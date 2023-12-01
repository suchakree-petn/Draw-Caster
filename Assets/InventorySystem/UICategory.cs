using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICategory : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private ItemType _categoryType;

    public void OnPointerDown(PointerEventData eventData)
    {
        UIInventory currentActiveInventory = InventorySystem.Instance.transform.GetComponentInChildren<UIInventory>();
        GameObject firstSlotItem = currentActiveInventory.GetItemListByType(_categoryType)[0];

        // UIInventory.OnCategoryClick?.Invoke(firstSlotItem.GetComponent<UISlotData>().item);
    }

}
