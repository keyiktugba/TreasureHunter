using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerinventory : MonoBehaviour, IInventory
{
    public int Money { get => money; set => money= value; }

    public int money = 0;
}
