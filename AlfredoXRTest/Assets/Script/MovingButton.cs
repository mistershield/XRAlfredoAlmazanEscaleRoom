using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingButton : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    [SerializeField] private AudioSource source;
    [SerializeField] List<Transform> targets = new List<Transform>();

    private int currentTargetIndex = 0;
    private float maxDistance;
    private bool canMove;

    void Update()
    {
        if(canMove)
        {
            MoveButton();
            CheckDistance();
            source.Play();
        }    
    }
    private void MoveButton()
    {
        maxDistance = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targets[currentTargetIndex].position, maxDistance);
    }
    private void CheckDistance()
    {
        if (Vector3.Distance(targets[currentTargetIndex].position, transform.position) <= minDistance)
        {
            canMove = false;
            if (currentTargetIndex >= targets.Count - 1) return;
            currentTargetIndex++;
        }
    }
    public void MakeTheButtonMove()
    {
        canMove = true;
    }
}
