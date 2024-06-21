using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHand : MonoBehaviour
{
    public bool swipe;

    public Vector3 doMoveStartPosition;

    public Vector3 doMoveFinishPositio;

    private void OnEnable()
    {
        transform.localScale = Vector3.one;

        if (swipe)
            StartCoroutine(DoMoveAnimation());
        else
            StartCoroutine(DoScaleAnimation());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator DoMoveAnimation()
    {
        float duration = 1.5f;

        float elapsedTime = 0;

        while (true)
        {
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                transform.position = Vector3.Lerp(doMoveStartPosition, doMoveFinishPositio, elapsedTime / duration);

                yield return null;
            }

            transform.position = doMoveStartPosition;

            elapsedTime = 0;

            yield return null;

        }
    }

    IEnumerator DoScaleAnimation()
    {
        float duration = 0.7f;

        float elapsedTime = 0;

        while (true)
        {
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one*0.6f,elapsedTime / duration);

                yield return null;
            }

            elapsedTime = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                transform.localScale = Vector3.Lerp(Vector3.one*0.6f,Vector3.one, elapsedTime / duration);

                yield return null;
            }

            yield return null;
        }
       
    }
}
