using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullTarget : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private SkullPuzzleHandler skullPuzzleHandler;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        skullPuzzleHandler = FindObjectOfType<SkullPuzzleHandler>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        skullPuzzleHandler.IncreaseSkullesDestroyed();
        MakeObjetInvisibleAndPassThrough();
        StartCoroutine(WaitDoDeactivateObject());
    }
    private void MakeObjetInvisibleAndPassThrough()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }
    private IEnumerator WaitDoDeactivateObject()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        gameObject.SetActive(false);
    }
}
