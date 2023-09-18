// using System.Collections;
// using System.Collections.Generic;
// using DG.Tweening;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public enum State
// {
//     Default,
//     SelectPositioning,
//     Cast
// }
// public class MeteorRain : Spell
// {
//     [SerializeField] private MeteorRainObj meteorRainObj;
//     [SerializeField] private State currentState;
//     [SerializeField] private Vector2 selectedPosition;
//     public delegate void MeteorRainBehaviour();
//     public static MeteorRainBehaviour OnSelectPosition;
//     public static MeteorRainBehaviour OnCast;
//     private void Awake()
//     {
//         spellObj = meteorRainObj;
//     }
//     private void Update()
//     {
//         switch (currentState)
//         {
//             case State.Default:
//                 DOTween.Kill(this);
//                 break;
//             case State.SelectPositioning:
//                 OnSelectPosition?.Invoke();
//                 break;
//             case State.Cast:
//                 OnCast?.Invoke();
//                 currentState = State.Default;
//                 break;
//         }
//     }
//     public override void CastSpell(float score)
//     {
//         castLevel = CalThreshold(score);
//         currentState = State.SelectPositioning;
//     }

//     public override void Cast1(GameObject player, GameObject target)
//     {
//         if (player == null) { return; }
//         float cameraOrthoSize = Camera.main.orthographicSize;
//         var sequence = DOTween.Sequence();
//         GameObject[] meteors = new GameObject[meteorRainObj.meteorAmount1];
//         for (int i = 0; i < meteorRainObj.meteorAmount1; i++)
//         {
//             GameObject meteor = Instantiate(meteorRainObj._meteorPrefab);
//             meteor.SetActive(false);
//             meteors[i] = meteor;
//         }

//         for (int i = 0; i < meteors.Length; i++)
//         {
//             int index = i;
//             int randomSide = (Random.Range(0, 2) == 0) ? -1 : 1;
//             sequence.AppendCallback(() =>
//             {
//                 meteors[index].SetActive(true);
//                 meteors[index].transform.position = transform.root.position + new Vector3(randomSide * cameraOrthoSize, cameraOrthoSize + 3, 0);
//             });
//             sequence.Append(meteors[index].transform.DOMove(RandomPositon(selectedPosition, 2), 0.2f, false));
//         }
//         sequence.Play();
//     }
//     public override void Cast2(GameObject player, GameObject target)
//     {
//         if (player == null) { return; }



//     }
//     public override void Cast3(GameObject player, GameObject target)
//     {
//         if (player == null) { return; }

//     }
//     public override void BeginCooldown(GameObject gameObject)
//     {
//         base.BeginCooldown(gameObject);
//     }


//     private void SelectPosition()
//     {
//         if (Input.GetMouseButtonUp(0))
//         {
//             selectedPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
//             currentState = State.Cast;
//         }
//     }
//     private void CastSpellByLevel()
//     {
//         switch (castLevel)
//         {
//             case 1:
//                 Cast1(transform.root.gameObject, null);
//                 break;
//             case 2:
//                 Cast2(transform.root.gameObject, null);
//                 break;
//             case 3:
//                 Cast3(transform.root.gameObject, null);
//                 break;
//             default:
//                 Debug.LogWarning("Cast level error");
//                 break;
//         }
//     }
//     private Vector2 RandomPositon(Vector2 targetPosition, float radius)
//     {
//         // Generate a random angle in radians
//         float randomAngle = Random.Range(0f, Mathf.PI * 2);

//         // Calculate a random position within the spawn radius
//         Vector2 spawnPosition = targetPosition + new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * Random.Range(0, radius);
//         return spawnPosition;
//     }
//     private void OnEnable()
//     {
//         currentState = State.Default;
//         OnSelectPosition += SelectPosition;
//         OnCast += CastSpellByLevel;
//     }
//     private void OnDisable()
//     {
//         OnSelectPosition -= SelectPosition;
//         OnCast -= CastSpellByLevel;
//     }
// }
