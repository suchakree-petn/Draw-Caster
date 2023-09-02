// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class SpellHolder_Q : MonoBehaviour
// {
//     [SerializeField] private Spell spell_Q;
//     [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();
//     [SerializeField] private PlayerAction _playerAction;

//     private void Awake()
//     {
//         _playerAction = PlayerInputSystem.Instance.playerAction();
//     }
//     public void Cast_Q(InputActions.CallbackContext context)
//     {
//         // if(context.canceled ==)
//         if(CheckMana(spell_Q) && spell_Q._isReadyToCast){

//         }
    
//         int castLevel = CalThreshold();
//         float delay = spell_Q.GetDelayTime();
//         int amount = spell_Q.GetAmount(castLevel);
//         StartCoroutine(RepeatCast(castLevel, delay, amount));

//     }
    
//     private bool CheckMana(Spell spell)
//     {
//         PlayerData _playerData = transform.root.GetComponent<PlayerManager>().playerData;
//         if (_playerData._mana >= spell._mana)
//         {
//             return true;
//         }
//         return false;
//     }
//     // private bool CheakCooldown(Spell spell){

//     // }

//     // private bool EnemyInRange(GameObject target)
//     // {
//     //     if (Vector2.Distance(transform.root.position, target.transform.position) < _spellList[0].GetRange())
//     //     {
//     //         return true;
//     //     }
//     //     else
//     //     {
//     //         return false;
//     //     }
//     // }

//     private int CalThreshold(float cossim)//
//     {
//         GameObject checkInput = Instantiate(spell_Q._checkInputPrefab,transform);
//         MousePosList calInput = checkInput.GetComponent<MousePosList>();
//         // calInput.
//         // float score = spell_Q._checkInputPrefab;
//         float low = spell_Q._lowThreshold;
//         float mid = spell_Q._midThreshold;

//         int castLevel;
//         if (score >= 0 && score <= low)
//         {
//             castLevel = 1;
//         }
//         else if (score > low && score <= mid)
//         {
//             castLevel = 2;
//         }
//         else if (score > mid && score <= 1)
//         {
//             castLevel = 3;
//         }
//         else
//         {
//             Debug.Log("CastThershold ERROR");
//         }
//         return castLevel;
//     }

//     IEnumerator RepeatCast(int castLevel, int delay, int amount)
//     {

//         for (int i = 0; i < amount.Count; i++)
//         {

//             yield return new WaitForSeconds(delay);
//         }
//     }


//     private void OnEnable()
//     {
//         _playerAction.Spell_Q.Enable();
//         _playerAction.Spell_Q.canceled += Cast_Q;
//     }
//     private void OnDisable()
//     {
//         _playerAction.Spell_Q.Disable();
//         _playerAction.Spell_Q.canceled -= Cast_Q;
//     }


// }

// // GameObject enemyTarget = _enemyList[i];
// //             if(EnemyInRange(enemyTarget)){
// //                 switch(castLevel){
// //                     case 1: _spellList[0].Cast1(transform.root.gameobject, enemyTarget);
// //                     amount--;
// //                     _enemyList.Remove(enemyTarget);
// //                     break;
// //                     case 2: _spellList[0].Cast2(transform.root.gameobject, enemyTarget);
// //                     amount--;
// //                     _enemyList.Remove(enemyTarget);
// //                     break;
// //                     case 3: _spellList[0].Cast3(transform.root.gameobject, enemyTarget);
// //                     amount--;
// //                     _enemyList.Remove(enemyTarget);
// //                     break;
// //                     default:
// //                         break;
// //                 }
// //                 if(_enemyList.Count == 0){
// //                     _enemyList = GameController.Instance.allEnemyInList;
// //                 }
// //             }
