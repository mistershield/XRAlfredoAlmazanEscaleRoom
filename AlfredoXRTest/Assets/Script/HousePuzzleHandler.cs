using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HousePuzzleHandler : MonoBehaviour
{
    [SerializeField] private GameObject missingHouse;
    [SerializeField] private GameObject tableHouseVisuals;
    [SerializeField] private AudioClip compleatedPuzzleSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UnityEvent foundHouseEvent;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject tmpGameObject = collision.gameObject;
        if (tmpGameObject == missingHouse)
        {
            PlaceHouse(tmpGameObject);
            DeactivateMisingHouseCode(tmpGameObject);
            audioSource.PlayOneShot(compleatedPuzzleSound);
            foundHouseEvent.Invoke();
        }
    }
    private void PlaceHouse(GameObject house)
    {
        house.transform.position = transform.position;
        house.transform.rotation = transform.rotation;
    }
    private void DeactivateMisingHouseCode(GameObject house)
    {
        house.gameObject.SetActive(false);
        tableHouseVisuals.SetActive(true);
    }
}
