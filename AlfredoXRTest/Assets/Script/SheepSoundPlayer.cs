using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> sheepSounds = new List<AudioClip>();

    private int sheepSoundsSize;
    private int randomIndex;
    private void Start()
    {
        sheepSoundsSize = sheepSounds.Count;
    }
    private void OnCollisionEnter(Collision collision)
    {
        SetCurrentAudioClip();
        PlaySheepSound();
    }
    private void SetCurrentAudioClip()
    {
        randomIndex = Random.Range(0, sheepSoundsSize);
    }
    private void PlaySheepSound()
    {
        audioSource.PlayOneShot(sheepSounds[randomIndex]);
    }
}
