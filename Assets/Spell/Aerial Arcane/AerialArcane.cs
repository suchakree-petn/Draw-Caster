using DG.Tweening;
using DrawCaster.Util;
using UnityEngine;

[CreateAssetMenu(fileName = "new Aerial Arcane", menuName = "Spell/Aerial Arcane")]
public class AerialArcane : Spell
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
    public float selfDestructTime;
    [SerializeField] private GameObject AerialArcanePrefab;
    [SerializeField] private float iFrame;

    public override void Cast1(GameObject player, GameObject target)
    {
        Debug.Log("Cast1");
        if (player == null) { return; }
        HandleMouseClick1(player); //mouse click to skill


    }
    public override void Cast2(GameObject player, GameObject target)
    {
        Debug.Log("Cast2");

        if (player == null) { return; }
        HandleMouseClick2(player); //mouse click to skill

    }
    public override void Cast3(GameObject player, GameObject target)
    {
        Debug.Log("Cast3");

        if (player == null) { return; }

        HandleMouseClick3(player); //mouse click to skill

    }


    private float currentScore;

    public override void CastSpell(float score, GameObject player)
    {
        base.CastSpell(score,player);
        currentScore = score;  // Store the score value
        int castLevel = CalThreshold(currentScore);
        CastByLevel(castLevel, player, null);

    }

    private void HandleMouseClick1(GameObject player)
    {
        int castLevel = CalThreshold(currentScore);
        int amount = GetAmount(castLevel);
        Sequence sequence = DOTween.Sequence();
        bool isClicked = false;
        PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.LeftClick.Enable();
        playerAction.Player.LeftClick.canceled += ctx => isClicked = true;
        sequence.AppendInterval(_delayTime).OnUpdate(() =>
        {
            if (isClicked)
            {
                playerAction.Player.LeftClick.Disable();
                Spawn1(player);
                sequence.Kill();
            }
        }).OnComplete(() =>
        {
            playerAction.Player.LeftClick.Disable();
            Spawn1(player);
        });
    }

    private void Spawn1(GameObject player)
    {
        Transform childTransform = DrawCasterUtil.GetMidTransformOf(player.transform);
        Vector3 childPosition = childTransform.position;
        GameObject spawnedObject = Instantiate(AerialArcanePrefab, childPosition, Quaternion.identity);
        spawnedObject = DrawCasterUtil.AddAttackHitTo(
            spawnedObject,
            _elementalType,
            player,
            _baseSkillDamageMultiplier * _damageSpellLevelMultiplier1,
            selfDestructTime,
            targetLayer,
            knockback1,
            iFrame
            );
        Rigidbody2D rb2d = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = spawnedObject.AddComponent<Rigidbody2D>();
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector2 direction = (mousePosition - childPosition).normalized;
        float angleRad = Mathf.Atan2(direction.y, direction.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;
        spawnedObject.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        rb2d.AddForce(direction * speed, ForceMode2D.Impulse);
    }


    //Spell level 2
    private void HandleMouseClick2(GameObject player)
    {

        int castLevel = CalThreshold(currentScore);
        int amount = GetAmount(castLevel);
        Sequence sequence = DOTween.Sequence();
        bool isClicked = false;
        PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.LeftClick.Enable();
        playerAction.Player.LeftClick.canceled += ctx => isClicked = true;
        sequence.AppendInterval(_delayTime).OnUpdate(() =>
        {
            if (isClicked)
            {
                playerAction.Player.LeftClick.Disable();
                Spawn2(player);
                sequence.Kill();
            }
        }).OnComplete(() =>
        {
            playerAction.Player.LeftClick.Disable();
            Spawn2(player);
        });

    }

    private void Spawn2(GameObject player)
    {
        //Get position to use spell
        Transform childTransform = DrawCasterUtil.GetMidTransformOf(player.transform);
        Vector3 childPosition = childTransform.position;

        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  // Set z to 0 to ignore the z-axis

        // Calculate the direction vector from the player to the mouse
        Vector2 direction = (mousePosition - childPosition).normalized;

        // Calculate the angle in radians between the player and the mouse
        float angleRad = Mathf.Atan2(direction.y, direction.x);

        // Convert the angle to degrees
        float angleDeg = angleRad * Mathf.Rad2Deg;

        // Calculate the direction vector from the player to the mouse
        Vector2 centerDirection = (mousePosition - childPosition).normalized;

        // Calculate left and right directions by rotating the center direction
        float angleOffset = 30f;  // Adjust this value to control the spread of the directions
        Vector2 leftDirection = RotateVector(centerDirection, angleOffset);
        Vector2 rightDirection = RotateVector(centerDirection, -angleOffset);

        // Instantiate and launch spell in center direction
        LaunchSpell(childPosition, centerDirection, player);

        // Instantiate and launch spell in left direction
        LaunchSpell(childPosition, leftDirection, player);

        // Instantiate and launch spell in right direction
        LaunchSpell(childPosition, rightDirection, player);
    }

    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float x = vector.x * Mathf.Cos(rad) - vector.y * Mathf.Sin(rad);
        float y = vector.x * Mathf.Sin(rad) + vector.y * Mathf.Cos(rad);
        return new Vector2(x, y);
    }

    private void LaunchSpell(Vector3 spawnPosition, Vector2 direction, GameObject player)
    {
        GameObject spawnedObject = Instantiate(AerialArcanePrefab, spawnPosition, Quaternion.identity);
        spawnedObject = DrawCasterUtil.AddAttackHitTo(
                    spawnedObject,
                    _elementalType,
                    player,
                    _baseSkillDamageMultiplier * _damageSpellLevelMultiplier2,
                    selfDestructTime,
                    targetLayer,
                    knockback2,
                    iFrame
                    );
        Rigidbody2D rb2d = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = spawnedObject.AddComponent<Rigidbody2D>();
        }
        rb2d.isKinematic = false;

        float angleRad = Mathf.Atan2(direction.y, direction.x);
        float angleDeg = angleRad * Mathf.Rad2Deg;
        spawnedObject.transform.rotation = Quaternion.Euler(0, 0, angleDeg);

        rb2d.AddForce(direction * speed, ForceMode2D.Impulse);

    }


    //Spell level 3
    public float speed = 10000f;
    private void HandleMouseClick3(GameObject player)
    {
        int castLevel = CalThreshold(currentScore);
        int amount = GetAmount(castLevel);

        Sequence sequence = DOTween.Sequence();
        bool isClicked = false;
        PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.LeftClick.Enable();
        playerAction.Player.LeftClick.canceled += ctx => isClicked = true;
        sequence.AppendInterval(_delayTime).OnUpdate(() =>
        {
            if (isClicked)
            {
                playerAction.Player.LeftClick.Disable();
                Spawn3(player);
                sequence.Kill();
            }
        }).OnComplete(() =>
        {
            playerAction.Player.LeftClick.Disable();
            Spawn3(player);
        });

    }

    private void Spawn3(GameObject player)
    {
        Transform childTransform = DrawCasterUtil.GetMidTransformOf(player.transform);
        Vector3 childPosition = childTransform.position;
        Vector2[] directions = {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
        };

        foreach (Vector2 direction in directions)
        {
            GameObject spawnedObject = Instantiate(AerialArcanePrefab, childPosition, Quaternion.identity);
            spawnedObject = DrawCasterUtil.AddAttackHitTo(
                                spawnedObject,
                                _elementalType,
                                player,
                                _baseSkillDamageMultiplier * _damageSpellLevelMultiplier3,
                                selfDestructTime,
                                targetLayer,
                                knockback3,
                                iFrame
                                );
            Rigidbody2D rb2d = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb2d == null)
            {
                rb2d = spawnedObject.AddComponent<Rigidbody2D>();
            }
            rb2d.isKinematic = false; // Make sure it's not kinematic if you want to use physics forces
            float angleRad = Mathf.Atan2(direction.y, direction.x);
            float angleDeg = angleRad * Mathf.Rad2Deg;
            spawnedObject.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
            rb2d.AddForce(direction * speed, ForceMode2D.Impulse);
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
