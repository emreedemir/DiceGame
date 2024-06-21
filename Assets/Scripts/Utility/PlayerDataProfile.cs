using System;
using UnityEngine;

public static class PlayerDataProfile 
{
    public static Action<int> OnHamburgerAmountChanged;

    public static Action<int> OnPumpkinAmountChanged;

    public static Action<int> OnPizzaAmountChanged;

    public static Action<int> OnHambuerAmountChangedValue;

    public static Action<int> OnPumpkinAmountChangedValue;

    public static Action<int> OnPizzaAmountChangedValue;

    public static int HambergerAmount
    {
        get
        {
            return PlayerPrefs.GetInt(ConstantValues.HamburgerKey,0);
        }
        set
        {
            int oldValue = HambergerAmount;

            PlayerPrefs.SetInt(ConstantValues.HamburgerKey,value);

            OnHamburgerAmountChanged?.Invoke(value);

            OnHambuerAmountChangedValue?.Invoke(value-oldValue);
        }
    }

    public static int PupmkinAmount
    {
        get
        {
            return PlayerPrefs.GetInt(ConstantValues.PumpkinAmount, 0);
        }
        set
        {
            int oldValue = PupmkinAmount;

            PlayerPrefs.SetInt(ConstantValues.PumpkinAmount, value);

            OnPumpkinAmountChanged?.Invoke(value);

            OnPumpkinAmountChangedValue?.Invoke(value-oldValue);
        }
    }

    public static int PizzaAmount
    {
        get
        {
            return PlayerPrefs.GetInt(ConstantValues.PizzaKey, 0);
        }
        set
        {
            int oldValue = PizzaAmount;

            PlayerPrefs.SetInt(ConstantValues.PizzaKey, value);

            OnPizzaAmountChanged?.Invoke(value);

            OnPizzaAmountChangedValue?.Invoke(value - oldValue);
        }
    }

    public static bool IsDiceChangeTutorialCompleted
    {
        get
        {
            return PlayerPrefs.GetInt(ConstantValues.IsDiceTutorialCompletedKey,0)==0?false:true;
        }
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt(ConstantValues.IsDiceTutorialCompletedKey, 1);
            }
            else
            {
                PlayerPrefs.SetInt(ConstantValues.IsDiceTutorialCompletedKey, 0);
            }
        }
    }
}

public class ConstantValues
{
    public const string HamburgerKey = "HamburgerAmount";

    public const string PumpkinAmount = "PumpkinAmount";

    public const string PizzaKey= "PizzaAmount";

    public const string IsDiceTutorialCompletedKey = "IsDiceTutorialCompletedKey";
}
