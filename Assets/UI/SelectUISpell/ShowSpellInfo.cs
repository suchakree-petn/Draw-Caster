using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowSpellInfo : MonoBehaviour
{
    [SerializeField] private List<Spell> listSpell;
    [SerializeField] private GameObject spellInfoPf;
    [SerializeField] private GameObject spellSlot;
    private void Start() {
        Generate();
    }
    void Generate(){
        gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listSpell[0]._description;
        foreach(Spell spell in listSpell){
            GameObject spellInfo = Instantiate(spellInfoPf, spellSlot.transform);
            spellInfo.transform.GetChild(1).GetComponent<Image>().sprite = spell._icon;
            spellInfo.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = spell._name;
        }
        
    }
}
