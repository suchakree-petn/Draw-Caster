using DG.Tweening;
using DrawCaster.Util;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "new Aerial Arcane", menuName = "Spell/Aerial Arcane")]
public class AerialArcane: Spell
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;
    public float _damageSpellLevelMultiplier1;
    public float _damageSpellLevelMultiplier2;
    public float _damageSpellLevelMultiplier3;

    [Header("Lighning Storm Setting")]
    public float _delayTime;
    public int _amountLevel1;
    public int _amountLevel2;
    public int _amountLevel3;
    public float selfDestructTime;
    public float randomPositionRadius;
    [SerializeField] private GameObject AerialArcanePrefab;

    /*
    public float speed = 10000f;
    public override void CastSpell(float score, GameObject player)
    {
        int castLevel = CalThreshold(score);
        int amount = GetAmount(castLevel);

        // Specify the name of the child GameObject you are looking for
        string childName = "Transforms";
        
        //Get position to use spell
        Transform childTransform = player.transform.Find(childName);
        Vector3 childPosition = childTransform.position;

        // Instantiate the object at the childPosition
        GameObject spawnedObject = Instantiate(AerialArcanePrefab, childPosition, Quaternion.identity);

        // Add a Rigidbody2D component to the spawned object if it doesn't already have one
        Rigidbody2D rb2d = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = spawnedObject.AddComponent<Rigidbody2D>();
        }
        rb2d.isKinematic = false; // Make sure it's not kinematic if you want to use physics forces

        // Assuming you have mousePos and randomSpread variables defined somewhere
        Vector2 direction = childPosition;
        rb2d.AddForce((direction + new Vector2(100,0)).normalized * speed, ForceMode2D.Impulse);
        Destroy(spawnedObject, 0.5f);

        Debug.Log("Test spell");
    }
    */



    /*
    public float speed = 10000f;
    public override void CastSpell(float score, GameObject player)
    {
        int castLevel = CalThreshold(score);
        int amount = GetAmount(castLevel);

        // Specify the name of the child GameObject you are looking for
        string childName = "Transforms";

        //Get position to use spell
        Transform childTransform = player.transform.Find(childName);
        Vector3 childPosition = childTransform.position;

        // Instantiate the object at the childPosition
        GameObject spawnedObject = Instantiate(AerialArcanePrefab, childPosition, Quaternion.identity);

        // Add a Rigidbody2D component to the spawned object if it doesn't already have one
        Rigidbody2D rb2d = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = spawnedObject.AddComponent<Rigidbody2D>();
        }
        rb2d.isKinematic = false; // Make sure it's not kinematic if you want to use physics forces
        
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  // Set z to 0 to ignore the z-axis

        // Calculate the direction vector from the player to the mouse
        Vector2 direction = (mousePosition - childPosition).normalized;
        
        // Apply force in the direction of the mouse
        rb2d.AddForce(direction * speed, ForceMode2D.Impulse);
        
        Destroy(spawnedObject, 0.5f);

        Debug.Log("Test spell");
    }
    */


    public override void Cast1(GameObject player, GameObject target)
    {
        Debug.Log("Cast1");
        if (player == null) { return; }
        player.GetComponent<PlayerInput>().AddMouseListener(HandleMouseClick1); //mouse click to skill


    }
    public override void Cast2(GameObject player, GameObject target)
    {
        Debug.Log("Cast2");

        if (player == null) { return; }
        player.GetComponent<PlayerInput>().AddMouseListener(HandleMouseClick2); //mouse click to skill

    }
    public override void Cast3(GameObject player, GameObject target)
    {
        Debug.Log("Cast3");

        if (player == null) { return; }

        player.GetComponent<PlayerInput>().AddMouseListener(HandleMouseClick3); //mouse click to skill

    }


    private float currentScore;
    
    public override void CastSpell(float score, GameObject player)
    {
        currentScore = score;  // Store the score value
        int castLevel = CalThreshold(currentScore);
        GameObject targetPos = new GameObject();
        CastByLevel(castLevel, player, targetPos);

    }

    /*
    public float speed = 10000f;
    

    public override void CastSpell(float score, GameObject player)
    {
        Sequence sequenceCast = DOTween.Sequence();

        int castLevel = CalThreshold(score);
            
    }
    */
    
     //Spell level 1
    private void HandleMouseClick1(GameObject player)
    {
        player.GetComponent<PlayerInput>().RemoveMouseListener(HandleMouseClick1);

        int castLevel = CalThreshold(currentScore);
        int amount = GetAmount(castLevel);

        // Specify the name of the child GameObject you are looking for
        string childName = "Transforms";

        //Get position to use spell
        Transform childTransform = player.transform.Find(childName);
        Vector3 childPosition = childTransform.position;

        // Instantiate the object at the childPosition
        GameObject spawnedObject = Instantiate(AerialArcanePrefab, childPosition, Quaternion.identity);

        // Add a Rigidbody2D component to the spawned object if it doesn't already have one
        Rigidbody2D rb2d = spawnedObject.GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = spawnedObject.AddComponent<Rigidbody2D>();
        }
        rb2d.isKinematic = false; // Make sure it's not kinematic if you want to use physics forces

        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  // Set z to 0 to ignore the z-axis

        // Calculate the direction vector from the player to the mouse
        Vector2 direction = (mousePosition - childPosition).normalized;

        // Calculate the angle in radians between the player and the mouse
        float angleRad = Mathf.Atan2(direction.y, direction.x);

        // Convert the angle to degrees
        float angleDeg = angleRad * Mathf.Rad2Deg;

        // Set the rotation of the spawned object to match the angle
        spawnedObject.transform.rotation = Quaternion.Euler(0, 0, angleDeg);

        // Apply force in the direction of the mouse
        rb2d.AddForce(direction * speed, ForceMode2D.Impulse);

        Destroy(spawnedObject, 0.5f);

        Debug.Log("Test spell");

    }

    
    //Spell level 2
    private void HandleMouseClick2(GameObject player)
    {
        player.GetComponent<PlayerInput>().RemoveMouseListener(HandleMouseClick2);

        int castLevel = CalThreshold(currentScore);
        int amount = GetAmount(castLevel);

        // Specify the name of the child GameObject you are looking for
        string childName = "Transforms";

        //Get position to use spell
        Transform childTransform = player.transform.Find(childName);
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
        LaunchSpell(childPosition, centerDirection);

        // Instantiate and launch spell in left direction
        LaunchSpell(childPosition, leftDirection);

        // Instantiate and launch spell in right direction
        LaunchSpell(childPosition, rightDirection);

    }

    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float x = vector.x * Mathf.Cos(rad) - vector.y * Mathf.Sin(rad);
        float y = vector.x * Mathf.Sin(rad) + vector.y * Mathf.Cos(rad);
        return new Vector2(x, y);
    }

    private void LaunchSpell(Vector3 spawnPosition, Vector2 direction)
    {
        GameObject spawnedObject = Instantiate(AerialArcanePrefab, spawnPosition, Quaternion.identity);

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

        Destroy(spawnedObject, 0.5f);
    }
    

    //Spell level 3
    public float speed = 10000f;
    private void HandleMouseClick3(GameObject player)
    {
        player.GetComponent<PlayerInput>().RemoveMouseListener(HandleMouseClick3);

        int castLevel = CalThreshold(currentScore);
        int amount = GetAmount(castLevel);
        Debug.Log(amount);

        // Specify the name of the child GameObject you are looking for
        string childName = "Transforms";

        // Get position to use spell
        Transform childTransform = player.transform.Find(childName);
        Vector3 childPosition = childTransform.position;

        // Directions relative to the player
        Vector2[] directions = {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

        foreach (Vector2 direction in directions)
        {
            // Instantiate the object at the childPosition
            GameObject spawnedObject = Instantiate(AerialArcanePrefab, childPosition, Quaternion.identity);

            // Add a Rigidbody2D component to the spawned object if it doesn't already have one
            Rigidbody2D rb2d = spawnedObject.GetComponent<Rigidbody2D>();
            if (rb2d == null)
            {
                rb2d = spawnedObject.AddComponent<Rigidbody2D>();
            }
            rb2d.isKinematic = false; // Make sure it's not kinematic if you want to use physics forces

            // Calculate the angle in radians between the player and the direction
            float angleRad = Mathf.Atan2(direction.y, direction.x);

            // Convert the angle to degrees
            float angleDeg = angleRad * Mathf.Rad2Deg;

            // Set the rotation of the spawned object to match the angle
            spawnedObject.transform.rotation = Quaternion.Euler(0, 0, angleDeg);

            // Apply force in the direction
            rb2d.AddForce(direction * speed, ForceMode2D.Impulse);

            Destroy(spawnedObject, 0.5f);
        }

        Debug.Log("Test spell");

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
