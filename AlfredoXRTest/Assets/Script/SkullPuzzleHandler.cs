using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkullPuzzleHandler : MonoBehaviour
{
    [SerializeField] private int skullsToDestroy;
    [SerializeField] private UnityEvent AllSkullsDestroyedEvent;
    [SerializeField] private AudioSource audioSource;

    private int skullsDestroyed = 0;
    private void CheckIfAllSkullsWhereDestroyed()
    {
        if(skullsDestroyed >= skullsToDestroy)
        {
            audioSource.Play();
            AllSkullsDestroyedEvent.Invoke();
        }
    }
    public void IncreaseSkullesDestroyed()
    {
        skullsDestroyed++;
        CheckIfAllSkullsWhereDestroyed();
    }
}
