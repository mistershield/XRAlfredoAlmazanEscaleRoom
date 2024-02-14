using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingButton : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private AudioSource source;

    private bool canMove;
    private float maxDistance;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { Debug.Log("sss");
            canMove = true;
        }
        if(canMove)
        {
            MoveButton();
            source.Play();
        }    
    }
    private void MoveButton()
    {
        maxDistance = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, maxDistance);
    }
    public void MakeTheButtonMove()
    {
        canMove = true;
    }
}
