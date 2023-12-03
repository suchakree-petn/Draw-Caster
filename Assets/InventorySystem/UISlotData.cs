
using DrawCaster.DataPersistence;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISlotData : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // public Item item;

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     UIInventory.OnSlotClick?.Invoke(this.item);
    // }
    public SpellData spellData;

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SpellSelectUI_Vertical.OnSlotClick?.Invoke(this.spellData);
        }
    }
}
