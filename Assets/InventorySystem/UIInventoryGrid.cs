using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIInventoryGrid : UIInventory
{

    [SerializeField] private GameObject UIInventoryHorizontalPrefab;


    public override void ShowInventory(Transform canvasTransform)
    {
        Instantiate(UIInventoryHorizontalPrefab, canvasTransform);
    }
}
