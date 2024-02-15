using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private int minPuzzledSolveToOpenDoor;
    [SerializeField] private AudioSource puzzleAudioSource;
    [SerializeField] private AudioClip puzzleCompleated;
    [SerializeField] private AudioClip puzzleNotCompleated;
    [SerializeField] private UnityEvent openDoor;
    [SerializeField] private UnityEvent closeDoor;
    [SerializeField] private UnityEvent blockLights;

    public int puzzlesSolved;
    private bool isDoorClosed = true;
    private bool isPuzzleSolved;
    private void CheckIfITCanOpenTheDoor()
    {
        if (puzzlesSolved >= minPuzzledSolveToOpenDoor && isDoorClosed)
        {
            openDoor.Invoke();
            blockLights.Invoke();
            isDoorClosed = false;
            isPuzzleSolved = true;
        }
        else if(!isDoorClosed)
        {
            if(!isPuzzleSolved)
            {
                closeDoor.Invoke();
                isDoorClosed = true;
            }
        }
    }
    public void PuzzleSolved()
    {
        puzzlesSolved++;
        puzzleAudioSource.PlayOneShot(puzzleCompleated);
        CheckIfITCanOpenTheDoor();
    }
    public void PuzzleNotSolved()
    {
        puzzlesSolved--;
        puzzleAudioSource.PlayOneShot(puzzleNotCompleated);
        CheckIfITCanOpenTheDoor();
    }
}
