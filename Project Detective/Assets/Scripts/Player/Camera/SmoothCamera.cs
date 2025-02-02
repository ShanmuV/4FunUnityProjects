using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private Vector3 velocity = Vector3.zero;
    // Update is called once per frame

    private void Start()
    {
        if(NewPlayerManager.instance != null)
        {
            target = NewPlayerManager.instance.transform;
        }
        else
        {
            Debug.LogError("No Player Movement Singleton found for camera");
        }
    }

    void Update()
    {
        Vector3 targetPosition =  target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
