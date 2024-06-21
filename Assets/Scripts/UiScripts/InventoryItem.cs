using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public TMPro.TMP_Text amountText;

    public void HandleUpdatedValue(int amount)
    {
        amountText.text = amount.ToString();
    }

    public void InitValue(int value)
    {
        amountText.text = value.ToString();
    }
}
