using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UISlotData))]
public class PlayerSpellUI : MonoBehaviour, IPointerClickHandler
{
    private UISlotData slotData;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked(eventData);
    }

    private void OnClicked(PointerEventData eventData)
    {
        if (slotData == null)
        {
            slotData = GetComponent<UISlotData>();
        }
        if (slotData.Is_HasData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                SpellSelectUI_Vertical.OnSlotClick?.Invoke(slotData.spellData);
            }
        }
        else
        {
            Debug.Log("This slot doesn't has data");
        }
    }
}
