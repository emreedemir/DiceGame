using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float distance;

    public float heigth;

    public Transform targetTransform;

    private Vector3 target
    {
        get
        {
            return targetTransform.position;
        }
    }

    private void LateUpdate()
    {
        transform.position =Vector3.Lerp(transform.position, target -Vector3.forward * distance + Vector3.up * heigth, Time.deltaTime);
    }
}


