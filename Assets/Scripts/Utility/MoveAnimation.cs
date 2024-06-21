using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class MoveAnimation : MonoBehaviour
{

    public Vector3 targetPosition;

    public Vector3 initialPosition;

    public float duration;

    private float elapsedTime;

    public Action OnTargetReached;

    public void Set(Vector3 _targetPosition,float duration)
    {
        this.targetPosition = _targetPosition;

        this.elapsedTime = 0;

        this.duration = 1f;

        this.initialPosition = transform.position;
    }

  
   
    IEnumerator MoveCoroutine()
    {

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime/duration);

            yield return null;

        }

        OnTargetReached?.Invoke();
    }

    private void OnEnable()
    {
        StartCoroutine(MoveCoroutine());
    }
}
