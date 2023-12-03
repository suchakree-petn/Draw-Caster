using System.Collections;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellSlotUI : MonoBehaviour, IPointerUpHandler
{
    private SpellData SpellData;
    // {
    //     get
    //     {
    //         if (spellData.Obj_Name == null)
    //         {
    //             return GetComponent<UISlotData>().spellData;
    //         }
    //         return spellData;
    //     }
    //     set
    //     {
    //         spellData = GetComponent<UISlotData>().spellData;
    //     }
    // }
    public void OnPointerUp(PointerEventData eventData)
    {
        OnClicked();
    }

    private void OnClicked()
    {
        SpellSelectUI_Vertical.OnSpellSlotClick?.Invoke(GetComponent<UISlotData>().spellData, gameObject);
    }

    private void OnEnable()
    {
        SpellSelect.OnUpdateSpellSelectSlotSuccess += OnClicked;
    }

    private void OnDisable()
    {
        SpellSelect.OnUpdateSpellSelectSlotSuccess -= OnClicked;
    }


}
