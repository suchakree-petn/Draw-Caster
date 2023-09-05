using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SpellHolder_E : MonoBehaviour
{
    [SerializeField] private Spell spell_E;
    [SerializeField] private bool _isReadyToCast;

    [SerializeField] private PlayerAction _playerAction;
    [SerializeField] private DrawInput_E drawInputE;
    public delegate void FinishDraw();
    public static FinishDraw finishDraw;

    public void ReceiveDrawInput(InputAction.CallbackContext context)
    {
        if (CheckMana(spell_E) && _isReadyToCast)
        {
            drawInputE.UI_image = spell_E.UI_image;
            drawInputE.gameObject.SetActive(true);
            drawInputE.inputPos.Clear();
            drawInputE.templatePos = BlackShadePositions.FindBlackShadePositions(spell_E._templateImage);
        }
    }
    public void Cast_E()
    {
        int castLevel = CalThreshold(drawInputE.score);
        Debug.Log("score " + drawInputE.score);
        Debug.Log("castLecel " + castLevel);
        float delay = spell_E._delayTime;
        int amount = spell_E.GetAmount(castLevel);

        GameObject[] enemyList = GameController.Instance.GetAllEnemyInScene();
        StartCoroutine(RepeatCast(castLevel, delay, amount, enemyList));

        _isReadyToCast = false;
        StartCoroutine(Cooldown(spell_E));

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
        float low = spell_E._lowThreshold;
        float mid = spell_E._midThreshold;

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
                spell_E.Cast1(transform.root.gameObject, enemyTarget);
                break;
            case 2:
                spell_E.Cast2(transform.root.gameObject, enemyTarget);
                break;
            case 3:
                spell_E.Cast3(transform.root.gameObject, enemyTarget);
                break;
            default:
                Debug.Log("ERROR!! Cast Level More Than 3");
                break;
        }

        _amount--;
        List<GameObject> temp = _enemyList.ToList();
        temp.Remove(enemyTarget);
        _enemyList = temp.ToArray();
        spell_E.BeginCooldown(transform.root.gameObject);
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
        GameObject.Find("Draw score").GetComponent<TextMeshProUGUI>().text = "Draw Score: " + (drawInputE.score * 100).ToString("F2");
        GameObject.Find("Cast level").GetComponent<TextMeshProUGUI>().text = "Cast Level: " + CalThreshold(drawInputE.score);
        drawInputE.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        _playerAction = PlayerInputSystem.Instance.playerAction;

        _playerAction.Player.Spell_E.Enable();
        _playerAction.Player.Spell_E.canceled += ReceiveDrawInput;
        finishDraw += Cast_E;
        finishDraw += ShowDrawScore;

    }
    private void OnDisable()
    {
        _playerAction.Player.Spell_E.Disable();
        _playerAction.Player.Spell_E.canceled -= ReceiveDrawInput;
        finishDraw -= Cast_E;
        finishDraw -= ShowDrawScore;

        //_playerAction = null;

    }


}