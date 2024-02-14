using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    [SerializeField] private int hitsToBreake;
    [SerializeField] private GameObject axeRef;
    [SerializeField] private AudioSource source;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == axeRef)
        {
            source.Play();
            RemoveHealth();
        }
        if(hitsToBreake <= 0)
        {
            DestroyThisBox();
        }
    }

    private void RemoveHealth()
    {
        hitsToBreake--;
    }
    private void DestroyThisBox()
    {
        Destroy(gameObject);
    }
}
