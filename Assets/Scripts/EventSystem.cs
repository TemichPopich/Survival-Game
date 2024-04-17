using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem
{
    public static event Action<int, string> Collect;


    public static void OnCollect(int _countOfResources, string _itemName)
    {
        Collect?.Invoke(_countOfResources, _itemName);
    }
}
