using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowSpellSelect : MonoBehaviour
{
    [SerializeField] private List<Spell> listSpell;
    [SerializeField] private GameObject spellSelectPf;
    [SerializeField] private GameObject spellSelectSlot;
    private void Start() {
        Generate();
    }
    void Generate(){
        foreach(Spell spell in listSpell){
            GameObject spellInfo = Instantiate(spellSelectPf, spellSelectSlot.transform);
            spellInfo.transform.GetChild(0).GetComponent<Image>().sprite = spell._icon;
        }
        
    }
}
