using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using DG.Tweening;

public class SpellHolder_R : MonoBehaviour
{
    public PlayerAction _playerAction;
    public bool _isReadyToCast;
    public int castLevel;

    public Spell spell;
    [Header("Draw Input")]
    public DrawInput_R drawInput;
    public delegate void InputCompare(float score, GameObject player);
    public static InputCompare OnFinishDraw;
    public delegate void CastBehaviour(GameObject player);
    public static CastBehaviour OnFinishCast;



    public void Cast(InputAction.CallbackContext context)
    {
        if (spell != null)
        {
            if (CheckMana(spell) && _isReadyToCast)
            {
                ReceiveDrawInput();
                OnFinishCast?.Invoke(transform.root.gameObject);
            }
        }
        else
        {
            Debug.Log("No spell equip on this slot " + name[name.Length - 1]);
        }
    }
    public bool CheckMana(Spell spell)
    {
        PlayerData _playerData = transform.root.GetComponent<PlayerManager>().playerData;
        if (_playerData.currentMana >= spell._manaCost)
        {
            return true;
        }
        return false;
    }
    public void ReceiveDrawInput()
    {
        drawInput.UI_image = spell.UI_image;
        drawInput.templateUI.sprite = spell.UI_image;

        drawInput.gameObject.SetActive(true);
        drawInput.inputPos.Clear();
        drawInput.templatePos = BlackShadePositions.FindBlackShadePositions(spell._templateImage);
    }
    private void OnEnable()
    {
        _playerAction = PlayerInputSystem.Instance.playerAction;
        _playerAction.Player.Spell_R.Enable();
        _playerAction.Player.Spell_R.canceled += Cast;
        if (spell != null)
        {
            OnFinishDraw += spell.CastSpell;
            OnFinishDraw += ShowDrawScore;
            OnFinishCast += spell.BeginCooldown;
            OnFinishCast += CountCooldown;

        }
    }
    private void OnDisable()
    {
        if (_playerAction != null)
        {
            _playerAction.Player.Spell_R.Disable();
            _playerAction.Player.Spell_R.canceled -= Cast;
        }
        if (spell != null)
        {
            OnFinishDraw -= spell.CastSpell;
            OnFinishDraw -= ShowDrawScore;
            OnFinishCast -= spell.BeginCooldown;
            OnFinishCast -= CountCooldown;
        }
    }
    void CountCooldown(GameObject player)
    {
        var sequence = DOTween.Sequence();
        sequence.AppendCallback(() => _isReadyToCast = false).AppendInterval(spell._cooldown);
        sequence.AppendCallback(() => _isReadyToCast = true);

    }
    void ShowDrawScore(float score, GameObject player)
    {
        GameObject.Find("Draw score").GetComponent<TextMeshProUGUI>().text = "Draw Score: " + (score * 100).ToString("F2");
        GameObject.Find("Cast level").GetComponent<TextMeshProUGUI>().text = "Cast Level: " + spell.CalThreshold(score);
        drawInput.gameObject.SetActive(false);
    }




}