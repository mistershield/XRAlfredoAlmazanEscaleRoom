using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    [SerializeField] private int hitsToBreake;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip breakeSound;
    [SerializeField] private GameObject axeRef;
    [SerializeField] private AudioSource source;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == axeRef)
        {
            HitBox();
        }
        if(hitsToBreake <= 0)
        {
            DestroyThisBox();
        }
    }

    private void HitBox()
    {
        hitsToBreake--;
        if (source.isPlaying) return;
        source.PlayOneShot(hitSound);
    }
    private void DestroyThisBox()
    {
        source.PlayOneShot(breakeSound);
        StartCoroutine(WaintToDestroyBox());
    }
    private void MakeObjectInvisibleAndPassThrough()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }
    private IEnumerator WaintToDestroyBox()
    {
        MakeObjectInvisibleAndPassThrough();
        yield return new WaitForSeconds(breakeSound.length);
        gameObject.SetActive(false);
    }
}
