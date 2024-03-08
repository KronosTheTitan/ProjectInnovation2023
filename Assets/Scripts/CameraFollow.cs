using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 1;
    private void Update()
    {
        if(target == null)
            return;
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
    }
}
