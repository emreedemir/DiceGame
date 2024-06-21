using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceThrow : MonoBehaviour
{
    public Action<float> OnThrowCompleted;

    public float throwSpeed;

    public bool isReady;

    public Vector3 currentSpeed;

    bool isDown;

    public bool locked;

    private void OnMouseDown()
    {
        if (isReady)
            isDown = true;
    }

    private void OnMouseDrag()
    {
        currentSpeed = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        if (locked)
            return;

        if (isReady==false)
            return;

        isDown = false;

        Vector3 mouseScrollDelta = (Input.mousePosition - currentSpeed);

        float moveSpeed = mouseScrollDelta.magnitude / Time.deltaTime;

        OnThrowCompleted?.Invoke(moveSpeed);
    }

    private void Update()
    {
        if (locked)
            return;

        if (isDown)
        {
            transform.RotateAround(Vector3.up, 5 * Time.deltaTime);

            transform.RotateAround(Vector3.right,  5 * Time.deltaTime);
        }
    }
}
