using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource source;

    public void OpenDoor()
    {
        animator.Play("OpenDoor", 0);
        source.Play();
    }
    public void CloseDoor()
    {
        animator.Play("CloseDoor", 0);
        source.Play();
    }
}
