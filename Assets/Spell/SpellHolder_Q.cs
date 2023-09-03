using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellHolder_Q : MonoBehaviour
{
    [SerializeField] private Spell spell_Q;
    [SerializeField] private PlayerAction _playerAction;
    [SerializeField] private DrawInput_Q _mousePosList;
    public delegate void FinishDraw();
    public static FinishDraw finishDraw;
    private void Start()
    {
        _playerAction = PlayerInputSystem.Instance.playerAction;
        _playerAction.Player.Spell_Q.Enable();
        _playerAction.Player.Spell_Q.canceled += ReceiveDrawInput;
        finishDraw = Cast_Q;
    }
    public void ReceiveDrawInput(InputAction.CallbackContext context)
    {
        if (CheckMana(spell_Q) && spell_Q._isReadyToCast)
        {
            _mousePosList.gameObject.SetActive(true);
            _mousePosList.inputPos.Clear();
            _mousePosList.templatePos = BlackShadePositions.FindBlackShadePositions(spell_Q._templateImage);
        }
    }
    public void Cast_Q()
    {
        int castLevel = CalThreshold(_mousePosList.score);
        Debug.Log("score " + _mousePosList.score);
        Debug.Log("castLecel " + castLevel);
        _mousePosList.gameObject.SetActive(false);
        float delay = spell_Q._delayTime;
        int amount = spell_Q.GetAmount(castLevel);

        GameObject[] enemyList = GameController.Instance.GetAllEnemyInScene();
        StartCoroutine(RepeatCast(castLevel, delay, amount, enemyList));

        spell_Q._isReadyToCast = false;
        StartCoroutine(Cooldown(spell_Q));

    }

    private bool CheckMana(Spell spell)
    {
        PlayerData _playerData = transform.root.GetComponent<PlayerManager>().playerData;
        if (_playerData.currentMana >= spell._manaCost)
        {
            return true;
        }
        return false;
    }

    private int CalThreshold(float score)//
    {
        float low = spell_Q._lowThreshold;
        float mid = spell_Q._midThreshold;

        int castLevel = 1;
        if (score >= 0 && score <= low)
        {
            castLevel = 1;
        }
        else if (score > low && score <= mid)
        {
            castLevel = 2;
        }
        else if (score > mid && score <= 1)
        {
            castLevel = 3;
        }
        else
        {
            Debug.Log("CastThershold ERROR");
        }
        return castLevel;
    }

    IEnumerator RepeatCast(int castLevel, float delay, int amount, GameObject[] enemyList)
    {
        if (amount == 0)
        {
            yield break;
        }
        GameObject[] _enemyList = enemyList;
        GameObject enemyTarget = null;
        int _amount = amount;
        if (GameController.Instance.GetAllEnemyInScene().Length == 0)
        {
            enemyTarget = RandomTransform();
        }
        else if (enemyList.Length == 0)
        {
            _enemyList = GameController.Instance.GetAllEnemyInScene();
            enemyTarget = _enemyList[Random.Range(0, _enemyList.Length)];
        }
        else
        {
            enemyTarget = _enemyList[Random.Range(0, _enemyList.Length)];
        }

        switch (castLevel)
        {
            case 1:
                spell_Q.Cast1(transform.root.gameObject, enemyTarget);
                break;
            case 2:
                spell_Q.Cast2(transform.root.gameObject, enemyTarget);
                break;
            case 3:
                spell_Q.Cast3(transform.root.gameObject, enemyTarget);
                break;
            default:
                Debug.Log("ERROR!! Cast Level More Than 3");
                break;
        }

        _amount--;
        List<GameObject> temp = _enemyList.ToList();
        temp.Remove(enemyTarget);
        _enemyList = temp.ToArray();
        spell_Q.BeginCooldown(transform.root.gameObject);
        yield return new WaitForSeconds(delay);
        StartCoroutine(RepeatCast(castLevel, delay, _amount, _enemyList));
    }
    IEnumerator Cooldown(Spell spell)
    {
        yield return new WaitForSeconds(spell._cooldown);
        spell._isReadyToCast = true;
    }

    private GameObject RandomTransform()
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, Mathf.PI * 2);

        // Calculate a random position within the spawn radius
        Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f) * 5f;

        GameObject obj = new GameObject();
        obj.AddComponent<AttackHit>();
        obj.GetComponent<AttackHit>().selfDestructTime = 0.1f;
        GameObject randomTransform = Instantiate(obj, spawnPosition, Quaternion.identity);

        return randomTransform;
    }

    private void OnEnable()
    {
        

    }
    private void OnDisable()
    {
        _playerAction.Player.Spell_Q.Disable();
        _playerAction.Player.Spell_Q.canceled -= ReceiveDrawInput;
        finishDraw -= Cast_Q;
    }


}