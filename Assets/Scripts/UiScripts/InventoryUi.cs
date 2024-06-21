using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    public InventoryItem hamburgerInventoryItem;

    public InventoryItem pumpkinInventoryItem;

    public InventoryItem pizzaInventoryItem;

    private void OnEnable()
    {
        hamburgerInventoryItem.InitValue(PlayerDataProfile.HambergerAmount);

        pumpkinInventoryItem.InitValue(PlayerDataProfile.PupmkinAmount);

        pizzaInventoryItem.InitValue(PlayerDataProfile.PizzaAmount);

        PlayerDataProfile.OnHamburgerAmountChanged += hamburgerInventoryItem.HandleUpdatedValue;

        PlayerDataProfile.OnPumpkinAmountChanged += pumpkinInventoryItem.HandleUpdatedValue;

        PlayerDataProfile.OnPizzaAmountChanged += pizzaInventoryItem.HandleUpdatedValue;
    }
}
