using System.Collections;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UISlotData))]
public class SpellSlotUI : MonoBehaviour, IPointerClickHandler
{
    private UISlotData slotData;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked();
    }

    private void OnClicked()
    {
        if (slotData == null)
        {
            slotData = GetComponent<UISlotData>();
        }
        if (slotData.Is_HasData)
        {
            SpellSelectUI_Vertical.OnSpellSlotClick?.Invoke(slotData.spellData, gameObject);
        }
        else
        {
            Debug.Log("This slot doesn't has data");
        }
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }


}
