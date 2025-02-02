using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    Vector3[] targetPositions = {new Vector3(-9,0,-10),new Vector3(9,0,-10)};
    int currPosition;

    private void Start()
    {
        if(transform.position == targetPositions[0]) currPosition = 0;
        else currPosition = 1;
    }

    public void changeSide()
    {
        currPosition = (currPosition + 1) % 2;
        transform.LeanMove(targetPositions[currPosition], .5f).setEaseInCubic().setEaseOutCubic();
    }
}
