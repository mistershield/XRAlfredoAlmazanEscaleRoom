using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullTarget : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private SkullPuzzleHandler skullPuzzleHandler;
    private void Start()
    {
        skullPuzzleHandler = FindObjectOfType<SkullPuzzleHandler>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        skullPuzzleHandler.IncreaseSkullesDestroyed();
        StartCoroutine(WaitDoDeactivateObject());
    }
    private IEnumerator WaitDoDeactivateObject()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        gameObject.SetActive(false);
    }
}
