using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiceController : MonoBehaviour
{
    public List<Dice> dices;

    [Range(1,10)]
    public float diceThrowForce;

    public Action<int,int> DiceThrowCompleted;

    WaitForSeconds diceWaitTime;

    public GameObject[] diceInitialPositions;

    private static int _diceTarget1 = 1;

    public static Action<int> OnDice1ValueChanged;

    public static int diceTarget1
    {
        get
        {
            return _diceTarget1;
        }
        set
        {

            if (value > 6)
                _diceTarget1 = 1;
            else
                _diceTarget1 = value;

            OnDice1ValueChanged?.Invoke(_diceTarget1);
        }
    }

    private static int _diceTarget2 = 1;

    public static Action<int> OnDice2ValueChanged;

    public static int diceTarget2
    {
        get
        {
            return _diceTarget2;
        }
        set
        {


            if (value> 6)
                _diceTarget2 = 1;
            else
                _diceTarget2 = value;

            OnDice2ValueChanged?.Invoke(_diceTarget2);
        }

    }

    public static bool dicePlayable = false;

    private void Awake()
    {
        diceWaitTime = new WaitForSeconds(1f);   
    }

    public void StartToDiceThrow()
    {
        StartCoroutine(DiceCoroutine());
    }

    IEnumerator DiceCoroutine()
    {
        dicePlayable = true;

        SetAsKinematic(true);

        LocateDiceInitialPosition();

        SetStatusDiceThrow(true);

        yield return new WaitUntil(() =>IsAnyDiceThrowed());

        dicePlayable = false;

        yield return new WaitUntil(() => IsAllDiceThrowed());

        yield return diceWaitTime;

        yield return new WaitUntil(() => IsAllDiceSleeping());

        SetAsKinematic(true);

        CheckValues();

        int dice1Value = dices[0].diceCurrentValue;

        int dice2Value = dices[1].diceCurrentValue;

        DiceThrowCompleted?.Invoke(dice1Value,dice2Value);
    }

    private bool IsAllDiceSleeping()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            if (dices[i].isSleeping==false)
                return false;
        }

        return true;
    }

    private bool IsAllDiceThrowed()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            if (dices[i].diceThrow.isReady==true)
                return false;
        }

        return true;
    }

    public bool IsAnyDiceThrowed()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            if (dices[i].diceThrow.isReady == false)
                return true;
        }

        return false;
    }

    public int GetSumValueDices()
    {
        int sum = 0;

        for (int i = 0; i < dices.Count; i++)
        {
            sum += dices[i].diceCurrentValue;
        }

        return sum;
    }

    public void SetStatusDiceThrow(bool state)
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].SetDiceThrowState(state);
        }
    }

    public void SetAsKinematic(bool state)
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].diceRigidbody.isKinematic = state;
        }
    }

    public void CheckValues()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].CheckValue();
        }
    }

    public void LocateDiceInitialPosition()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].transform.position = diceInitialPositions[i].transform.position;

            dices[i].transform.rotation = diceInitialPositions[i].transform.rotation;
        }
    }
}
