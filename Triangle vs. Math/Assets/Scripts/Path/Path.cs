using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform leftEdge;
    public Transform rightEdge;

    public Path pathOnLeft;
    public Path pathOnRight;

    public bool invertedAxis;

    private void Start()
    {
        if(invertedAxis)
        {
            Transform temp = leftEdge;
            leftEdge = rightEdge;
            rightEdge = temp;
        }
    }

}
