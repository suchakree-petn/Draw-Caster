
using DrawCaster.DataPersistence;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlotData : MonoBehaviour, IPointerDownHandler
{
    // public Item item;

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     UIInventory.OnSlotClick?.Invoke(this.item);
    // }
    public SpellData spellData;

    public void OnPointerDown(PointerEventData eventData)
    {
        SpellSelectUI_Vertical.OnSlotClick?.Invoke(this.spellData);
    }
}
