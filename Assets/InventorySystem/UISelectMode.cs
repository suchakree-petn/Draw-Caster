using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectMode : MonoBehaviour
{
    public UIInventory InventoryGridUI;
    public UIInventory InventoryHorizontalUI;
    [SerializeField] private Transform parent;
    public void ShowGridUI(){
        ClearUIInventory();
        InventoryGridUI.ShowInventory(parent);
    }
    public void ShowHorizontalUI(){
        ClearUIInventory();
        InventoryHorizontalUI.ShowInventory(parent);
    }
    private void ClearUIInventory(){
        if(parent.childCount > 0){
            foreach(Transform child in parent){
                Destroy(child.gameObject);
            } 
        }
    }
}
