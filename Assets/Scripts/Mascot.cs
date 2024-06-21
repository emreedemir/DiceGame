using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascot : MonoBehaviour
{
    public TileData currentTile;

    public Transform startPosition;

    public void InitTarget()
    {
        this.transform.position = startPosition.position;

        StopAllCoroutines();
    }

    public void InitTargetWithAnimation(Action OnMoveCompleted)
    {
       StartCoroutine( MoveCoroutine(startPosition.position, () =>
         {
             OnMoveCompleted?.Invoke();
         },4f));
    }

    public void MoveToTarget(TileData tileData,Action OnMoveCompleted)
    {
        currentTile = tileData;

        StartCoroutine(MoveCoroutine(currentTile.MapTileView.transform.position,OnMoveCompleted,1f));
    }

    IEnumerator MoveCoroutine(Vector3 targetPosition,Action OnCompleted,float waitTime)
    {
        float elapsedTime = 0;

        float duration = 1f;

        Vector3 initialPosition = transform.position;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);

            yield return null;

        }

        yield return new WaitForSeconds(waitTime);

        OnCompleted?.Invoke();
    }
}
