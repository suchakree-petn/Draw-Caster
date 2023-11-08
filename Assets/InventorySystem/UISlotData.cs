using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlotData : MonoBehaviour, IPointerDownHandler
{
    public Item item;

    public void OnPointerDown(PointerEventData eventData)
    {
        UIInventory.OnSlotClick?.Invoke(this.item);
    }
}
