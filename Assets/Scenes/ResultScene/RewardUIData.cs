using System;
using System.Collections;
using System.Collections.Generic;
using DrawCaster.DataPersistence;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DrawCaster.ResultManager
{
    public class RewardUIData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Reward data")]
        public SpellData spell;
        public double gold;

        [Header("Ref Component")]
        [SerializeField] private Transform info_prf;
        [SerializeField] private Transform info_child_prf;
        private Transform info_transform;


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (info_prf == null)
            {
                Debug.Log("Info prefab is null");
                return;
            }



            if (spell.Name != "")
            {
                info_transform = Instantiate(info_prf, eventData.position, Quaternion.identity, transform);
                ShowSpellInfo();
            }
            else
            {
                Debug.LogWarning("No data to show");
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (info_transform == null) return;
            Destroy(info_transform.gameObject);
        }
        private void ShowSpellInfo()
        {
            if (info_transform == null) return;

            GameObject name = Instantiate(info_child_prf.gameObject, info_transform);
            name.GetComponent<TextMeshProUGUI>().text = "Spell: " + spell.Name;

            GameObject desc = Instantiate(info_child_prf.gameObject, info_transform);
            desc.GetComponent<TextMeshProUGUI>().text = "Info: " + spell.Desc;
        }
        private void ShowGoldInfo()
        {
            if (info_transform == null) return;

            GameObject gold = Instantiate(info_child_prf.gameObject, info_transform);
            gold.GetComponent<TextMeshProUGUI>().text = "Gold: " + this.gold;
        }

    }

}

