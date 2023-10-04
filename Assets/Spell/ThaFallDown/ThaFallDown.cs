using DG.Tweening;
using DrawCaster.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new ThaFallDown", menuName = "Spell/ThaFallDown")]
public class ThaFallDown : Spell
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;
    public float _damageSpellLevelMultiplier1;
    public float _damageSpellLevelMultiplier2;
    public float _damageSpellLevelMultiplier3;

    [Header("Aerial Arcane Setting")]
    public float _delayTime;
    public int _amountLevel1;
    [SerializeField] private float knockback1;
    public int _amountLevel2;
    [SerializeField] private float knockback2;
    public int _amountLevel3;
    [SerializeField] private float knockback3;
    public float iFrameTime;
    [SerializeField] private GameObject ThaFallDownPrefab;

    public override void Cast1(GameObject player, GameObject target)
    {
        Debug.Log("Cast1");
        if (player == null) { return; }
        Revolve(player);
        MonoBehaviour monoBehaviour = player.GetComponent<MonoBehaviour>();
        if (monoBehaviour != null) { monoBehaviour.StartCoroutine(FollowPlayer(player)); }
        else { Debug.LogError("Player GameObject does not have a MonoBehaviour component!"); }



    }
    public override void Cast2(GameObject player, GameObject target)
    {
        Debug.Log("Cast2");

        if (player == null) { return; }
        Revolve(player);
        MonoBehaviour monoBehaviour = player.GetComponent<MonoBehaviour>();
        if (monoBehaviour != null) { monoBehaviour.StartCoroutine(FollowPlayer(player)); }
        else { Debug.LogError("Player GameObject does not have a MonoBehaviour component!"); }

    }
    public override void Cast3(GameObject player, GameObject target)
    {
        Debug.Log("Cast3");

        if (player == null) { return; }
        Revolve(player);
        MonoBehaviour monoBehaviour = player.GetComponent<MonoBehaviour>();
        if (monoBehaviour != null) { monoBehaviour.StartCoroutine(FollowPlayer(player)); }
        else { Debug.LogError("Player GameObject does not have a MonoBehaviour component!"); }


    }

    public float radius = 2f;  // Distance from the player
    public float duration = 5f;  // Duration of one revolution
    public float followInterval = 0.02f;  // Interval at which to update the parent object's position

    public static int numAll = 3;

    private GameObject[] parentObjectALl = new GameObject[numAll];
    private GameObject[] revolvingObjectAll = new GameObject[numAll];
    private Tween[] rotateTweenAll = new Tween[numAll];
    private float[] currentDurationAll = new float[numAll];
    private float[] timeToDestroyAll = { 3f, 3.5f, 4f };
    private float[] playerSpeedAll = { 10f, 11f, 11.5f };

    public override void CastSpell(float score, GameObject player)
    {
        //int castLevel = CalThreshold(currentScore);
        // CastByLevel(castLevel, player, null);
        Debug.Log("Test the spell.");
        int castLevel = CalThreshold(score);
        numAll = castLevel;
        Debug.Log(numAll);
        CastByLevel(castLevel, player, null);
    }
    void Revolve(GameObject player)
    {
        for (int i = 0; i < numAll; i++)
        {
            parentObjectALl[i] = new GameObject("RevolveParent" + (i + 1));
            parentObjectALl[i].transform.position = player.transform.position;
            // Instantiate ThaFallDownPrefab as a child of the parent object
            revolvingObjectAll[i] = DrawCasterUtil.AddAttackHitTo(
                Instantiate(ThaFallDownPrefab, parentObjectALl[i].transform.position + new Vector3(radius, 0, 0), Quaternion.identity, parentObjectALl[i].transform),
                _elementalType,
                player,
                _baseSkillDamageMultiplier,
                duration,
                targetLayer,
                knockback1,
                iFrameTime
                );

            rotateTweenAll[i] = parentObjectALl[i].transform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);  // Infinite loops

            currentDurationAll[i] = duration;  // Initialize currentDuration1 with the initial duration value
            RotateParentObject();  // Start the initial rotation
                                   // Destroy(revolvingObjectAll[i], timeToDestroyAll[i]);
            rotateTweenAll[i].OnComplete(() =>
            {
                // Code to execute when the rotation is complete (won't be called due to infinite loops)
            });
        }
    }


    void RotateParentObject()
    {
        for (int i = 0; i < numAll; i++)
        {
            if (parentObjectALl[i] != null)
            {
                // Rotate the parent object around its Z-axis
                parentObjectALl[i].transform.DORotate(new Vector3(0, 0, 360), currentDurationAll[i], RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Restart);  // Infinite loops
            }
        }
    }
    public IEnumerator FollowPlayer(GameObject player)
    {

        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody == null)
        {
            Debug.LogError("Player GameObject does not have a Rigidbody component!");
            yield break;
        }
        for (int i = 0; i < numAll; i++)
        {
            if (parentObjectALl[i] == null) { yield break; }
        }
        float previousSpeed = 0f;  // Store the previous speed to detect significant changes
        while (true)
        {
            Transform childTransform = DrawCasterUtil.GetMidTransformOf(player.transform);
            if (childTransform == null) { yield break; }

            Vector3 centerPosition = childTransform.position;
            for (int i = 0; i < numAll; i++)
            {
                if (parentObjectALl[i] == null)
                {
                    yield break;
                }
                parentObjectALl[i].transform.position = centerPosition;
            }
            float playerSpeed = 10f;
            if (Mathf.Abs(playerSpeed - previousSpeed) > 0.1f)  // Change the rotation speed only if the speed has changed significantly
            {
                previousSpeed = playerSpeed;
                for (int i = 0; i < numAll; i++) { currentDurationAll[i] = Mathf.Max(0.1f, duration / Mathf.Max(1f, playerSpeedAll[i])); }
                RotateParentObject();  // Restart the rotation with the new duration
            }
            yield return new WaitForSeconds(followInterval);
        }
    }


    public int GetAmount(int castLevel)
    {
        if (castLevel == 1)
        {
            return _amountLevel1;
        }
        else if (castLevel == 2)
        {
            return _amountLevel2;
        }
        return _amountLevel3;
    }
    public override void BeginCooldown(GameObject player)
    {

    }

}
