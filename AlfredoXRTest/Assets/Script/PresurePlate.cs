using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PresurePlate : MonoBehaviour
{
    [SerializeField] private UnityEvent activationEvent;
    [SerializeField] private UnityEvent deactivationEvent;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    private bool activated;
    private List<GameObject> collidedObjects = new List<GameObject>();
    private void OnCollisionEnter(Collision collision)
    {
        if (collidedObjects.Count <= 0)
        {
            animator.Play("ButtonDown", 0);
            audioSource.Play();
            activationEvent.Invoke();
        }
        collidedObjects.Add(collision.gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        collidedObjects.Remove(collision.gameObject);
        if(collidedObjects.Count <= 0)
        {
            animator.Play("ButtonUp", 0);
            audioSource.Play();
            deactivationEvent.Invoke();
        }
    }
}
