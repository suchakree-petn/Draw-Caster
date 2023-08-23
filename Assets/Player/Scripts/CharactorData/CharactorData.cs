using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharactorData : ScriptableObject
{
   public string Name;
   public float MovementSpeed;
   public GameObject CharactorPrefab;   
}
