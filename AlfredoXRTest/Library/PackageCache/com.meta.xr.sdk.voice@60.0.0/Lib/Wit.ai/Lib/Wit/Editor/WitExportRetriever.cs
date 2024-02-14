﻿/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Meta.WitAi;
using Meta.WitAi.Data.Info;
using Meta.WitAi.Requests;
namespace Lib.Wit.Runtime.Data.Info
{
    /// <summary>
    /// A class to synchronize multiple parallel retrievals of the same app's export.
    /// </summary>
    public abstract class WitExportRetriever
    {
        // tracks which callbacks have already been registered for a specific export retrieval
        // this is necessary as the equality checks on delegates don't work
        private static readonly Dictionary<string, List<MethodInfo>> CallbacksPerConfig =  new Dictionary<string, List<MethodInfo>>();

        //tracks the delegates to call for each config
        private static readonly Dictionary<string, List<VRequest.RequestCompleteDelegate<ZipArchive>>> PendingCallbacksPerConfig =  new Dictionary<string, List<VRequest.RequestCompleteDelegate<ZipArchive>>>();

        /// <summary>
        /// Retrieves the export for the requested configuration and calls the onComplete once retrieved.
        /// </summary>
        /// <param name="configuration">the config of the app export to be retrieved</param>
        /// <param name="onComplete">the function to call upon successful retrieval</param>
        public static void GetExport(IWitRequestConfiguration configuration, VRequest.RequestCompleteDelegate<ZipArchive> onComplete )
        {
            string appId = configuration.GetApplicationId();
            if (string.IsNullOrEmpty(appId)) return; //new config; haven't yet retrieved it.
            if (!CallbacksPerConfig.ContainsKey(appId))
            {
                CallbacksPerConfig[appId] = new List<MethodInfo>();
                PendingCallbacksPerConfig[appId] = new List<VRequest.RequestCompleteDelegate<ZipArchive>>();
            }

            if (CallbacksPerConfig[appId].Contains(onComplete.Method)) return;

            PendingCallbacksPerConfig[appId].Add(onComplete);
            CallbacksPerConfig[appId].Add(onComplete.Method);

            if (PendingCallbacksPerConfig[appId].Count == 1)
            {
                new WitInfoVRequest(configuration, true).RequestAppExportInfo(appId,
                    (exportInfo, exportInfoError) =>
                        OnExportInfoGetCompletion(appId, exportInfo, exportInfoError));
            }
        }
        // Callback following Wit request for app info
        private static void OnExportInfoGetCompletion(string appId, WitExportInfo exportInfo, string exportInfoError)
        {
            if (!string.IsNullOrEmpty(exportInfoError))
            {
                OnExportZipLoadCompletion(appId, null, exportInfoError);
                return;
            }
            new VRequest().RequestFile(new Uri(exportInfo.uri),
                (exportZipData, exportZipError) =>
                    OnExportZipLoadCompletion(appId, exportZipData, exportInfoError));
        }

        // Callback following zip file request
        private static void OnExportZipLoadCompletion(string appId, byte[] exportZipData, string exportZipError)
        {
            if (!string.IsNullOrEmpty(exportZipError))
            {
                OnExportZipDecodeCompletion(appId, null, exportZipError);
                return;
            }
            try
            {
                var zip = new ZipArchive(new MemoryStream(exportZipData));
                OnExportZipDecodeCompletion(appId, zip, null);
            }
            catch (Exception e)
            {
                OnExportZipDecodeCompletion(appId, null, e.ToString());
            }
        }

        // Callback following final export completion
        private static void OnExportZipDecodeCompletion(string appId, ZipArchive result, string error)
        {
            foreach (var pending in PendingCallbacksPerConfig[appId])
            {
                pending.Invoke(result, error);
            }
            PendingCallbacksPerConfig[appId].Clear();
            CallbacksPerConfig[appId].Clear();
        }
    }
}
