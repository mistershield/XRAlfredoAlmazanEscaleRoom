using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLightHandler : MonoBehaviour
{
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;

    private bool blockLights = false;
    public void TurnOnOffLight1(bool state)
    {
        if(!blockLights)
        {
            light1.SetActive(state);
        }
    }
    public void TurnOnOffLight2(bool state)
    {
        if (!blockLights)
        {
            light2.SetActive(state);
        }
    }
    public void BlockLights()
    {
        blockLights = true;
        light1.SetActive(true);
        light2.SetActive(true);
    }
}
