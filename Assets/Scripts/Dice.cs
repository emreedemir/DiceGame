using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public bool isSleeping;

    public int diceCurrentValue;

    public Rigidbody diceRigidbody;

    public DiceThrow diceThrow;

    public LayerMask targetGroundLayer;
  
    public void ThrowDice(float diceThrowForce)
    {
        SetDiceThrowState(false);

        diceThrowForce /= 1000f;

        diceThrowForce = Mathf.Clamp(diceThrowForce,1.8f, 3.8f);

        diceRigidbody.isKinematic = false;

        int sendAxis = UnityEngine.Random.RandomRange(0, 2) == 0 ? -1 : 1;

        diceRigidbody.AddForce(Vector3.forward * UnityEngine.Random.RandomRange(1.3f, 1.5f) * diceThrowForce, ForceMode.Impulse);

        diceRigidbody.AddForce(Vector3.up * UnityEngine.Random.RandomRange(1f, 2f) * diceThrowForce, ForceMode.Impulse);

        diceRigidbody.AddTorque(Vector3.up * UnityEngine.Random.RandomRange(60, 120) * diceThrowForce * sendAxis, ForceMode.Impulse);

        diceRigidbody.AddTorque(Vector3.right * UnityEngine.Random.RandomRange(60, 120) * diceThrowForce * sendAxis, ForceMode.Impulse);
    }

    public void CheckValue()
    {
        Vector3 origin = transform.position;
   
        Vector3 forward = transform.forward;

        Vector3 backward = -transform.forward;

        Vector3 right = transform.right;

        Vector3 left =-transform.right;

        Vector3 up = transform.up;

        Vector3 down = -transform.up;

        RaycastHit hit;

        if (Physics.Raycast(origin, forward, out hit, Mathf.Infinity, targetGroundLayer))
        {
            diceCurrentValue = 3;
        }
        else if (Physics.Raycast(origin, backward, out hit, Mathf.Infinity, targetGroundLayer))
        {
            diceCurrentValue = 1;
        }
        else if (Physics.Raycast(origin, right, out hit, Mathf.Infinity, targetGroundLayer))
        {
            diceCurrentValue = 4;
        }
        else if (Physics.Raycast(origin, left, out hit, Mathf.Infinity, targetGroundLayer))
        {
            diceCurrentValue = 2;
        }
        else if (Physics.Raycast(origin, up, out hit, Mathf.Infinity, targetGroundLayer))
        {
            diceCurrentValue = 6;
        }
        else if (Physics.Raycast(origin, down, out hit, Mathf.Infinity, targetGroundLayer))
        {
            diceCurrentValue = 5;
        }
    }

    public void SetDiceThrowState(bool state)
    {
        diceThrow.isReady = state;
    }

    private void OnEnable()
    {
        diceThrow.OnThrowCompleted += ThrowDice;
    }

    private void OnDisable()
    {
        diceThrow.OnThrowCompleted -= ThrowDice;
    }

    private void Update()
    {
        isSleeping = diceRigidbody.IsSleeping();
    }
}
