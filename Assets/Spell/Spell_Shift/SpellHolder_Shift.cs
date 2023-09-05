using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SpellHolder_Shift : MonoBehaviour
{
    [SerializeField] private Spell spell_Shift;
    [SerializeField] private bool _isReadyToCast;

    [SerializeField] private PlayerAction _playerAction;
    [SerializeField] private DrawInput_Shift drawInputShift;
    public delegate void FinishDraw();
    public static FinishDraw finishDraw;

    public void ReceiveDrawInput(InputAction.CallbackContext context)
    {
        if (CheckMana(spell_Shift) && _isReadyToCast)
        {
            drawInputShift.UI_image = spell_Shift.UI_image;
            drawInputShift.gameObject.SetActive(true);
            drawInputShift.inputPos.Clear();
            drawInputShift.templatePos = BlackShadePositions.FindBlackShadePositions(spell_Shift._templateImage);
        }
    }
    public void Cast_Shift()
    {
        int castLevel = CalThreshold(drawInputShift.score);
        Debug.Log("score " + drawInputShift.score);
        Debug.Log("castLecel " + castLevel);
        float delay = spell_Shift._delayTime;
        int amount = spell_Shift.GetAmount(castLevel);

        GameObject[] enemyList = GameController.Instance.GetAllEnemyInScene();
        StartCoroutine(RepeatCast(castLevel, delay, amount, enemyList));

        _isReadyToCast = false;
        StartCoroutine(Cooldown(spell_Shift));

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
        float low = spell_Shift._lowThreshold;
        float mid = spell_Shift._midThreshold;

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
                spell_Shift.Cast1(transform.root.gameObject, enemyTarget);
                break;
            case 2:
                spell_Shift.Cast2(transform.root.gameObject, enemyTarget);
                break;
            case 3:
                spell_Shift.Cast3(transform.root.gameObject, enemyTarget);
                break;
            default:
                Debug.Log("ERROR!! Cast Level More Than 3");
                break;
        }

        _amount--;
        List<GameObject> temp = _enemyList.ToList();
        temp.Remove(enemyTarget);
        _enemyList = temp.ToArray();
        spell_Shift.BeginCooldown(transform.root.gameObject);
        yield return new WaitForSeconds(delay);
        StartCoroutine(RepeatCast(castLevel, delay, _amount, _enemyList));
    }
    IEnumerator Cooldown(Spell spell)
    {
        yield return new WaitForSeconds(spell._cooldown);
        _isReadyToCast = true;
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

    void ShowDrawScore()
    {
        GameObject.Find("Draw score").GetComponent<TextMeshProUGUI>().text = "Draw Score: " + (drawInputShift.score * 100).ToString("F2");
        GameObject.Find("Cast level").GetComponent<TextMeshProUGUI>().text = "Cast Level: " + CalThreshold(drawInputShift.score);
        drawInputShift.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        if (spell_Shift != null)
        {
            _playerAction = PlayerInputSystem.Instance.playerAction;

            _playerAction.Player.Spell_Shift.Enable();
            _playerAction.Player.Spell_Shift.canceled += ReceiveDrawInput;
            finishDraw += Cast_Shift;
            finishDraw += ShowDrawScore;
        }
        else
        {
            gameObject.SetActive(false);
        }


    }
    private void OnDisable()
    {
        if (spell_Shift != null)
        {
            _playerAction.Player.Spell_Shift.Disable();
            _playerAction.Player.Spell_Shift.canceled -= ReceiveDrawInput;
            finishDraw -= Cast_Shift;
            finishDraw -= ShowDrawScore;

            //_playerAction = null; 
        }


    }


}