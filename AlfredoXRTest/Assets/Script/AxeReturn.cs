using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeReturn : MonoBehaviour
{
    [SerializeField] private float maxDistance; 
    private Vector3 originalPosition;
    private void Start()
    {
        originalPosition = transform.position;
    }
    private void Update()
    {
        CheckIfNeedToReturn();
    }
    private void CheckIfNeedToReturn()
    {
        if (Vector3.Distance(transform.position, originalPosition) >= maxDistance)
        {
            ReturnObject();
        }
    }
    public void ReturnObject()
    {
        transform.position = originalPosition;
    }
}
