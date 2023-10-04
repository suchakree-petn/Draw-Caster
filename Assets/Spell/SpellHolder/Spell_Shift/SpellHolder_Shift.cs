using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SpellHolder_Shift : MonoBehaviour
{
    public static SpellHolder_Shift Instance;
    public PlayerAction _playerAction;
    public bool _isReadyToCast;
    public int castLevel;

    public Spell spell;
    public float cooldown;

    [Header("Draw Input")]
    public DrawInput_Shift drawInput;
    public delegate void InputCompare(float score, GameObject player);
    public static InputCompare OnFinishDraw;
    public delegate void CastBehaviour(GameObject player);
    public static CastBehaviour OnFinishCast;
    private void Awake() {
        Instance = this;
    }


    public void Cast(InputAction.CallbackContext context)
    {
        if (spell != null)
        {
            if (CheckMana(spell) && _isReadyToCast)
            {
                DisableInput();
                ReceiveDrawInput();
                PayManaCost();
                OnFinishCast?.Invoke(transform.root.gameObject);
            }
        }
        else
        {
            Debug.Log("No spell equip on this slot " + name[name.Length - 1]);
        }
    }
    private void DisableInput()
    {
        _playerAction.Player.PressAttack.Disable();
        _playerAction.Player.HoldAttack.Disable();
        _playerAction.Player.Spell_Q.Disable();
        _playerAction.Player.Spell_E.Disable();
        _playerAction.Player.Spell_R.Disable();
        _playerAction.Player.Spell_Shift.Disable();
        _playerAction.Player.Interact.Disable();
        _playerAction.Player.LeftClick.Disable();
        _playerAction.Player.ManaNullify.Disable();

    }
    public bool CheckMana(Spell spell)
    {
        PlayerManager _playerManager = transform.root.GetComponent<PlayerManager>();
        if (_playerManager.currentMana >= spell._manaCost)
        {
            return true;
        }
        return false;
    }
    public void PayManaCost(){
        PlayerManager _playerManager = transform.root.GetComponent<PlayerManager>();
        _playerManager.currentMana -= spell._manaCost;
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
        _playerAction.Player.Spell_Shift.Enable();
        _playerAction.Player.Spell_Shift.canceled += Cast;
        if (spell != null)
        {
            OnFinishDraw += spell.CastSpell;
            OnFinishDraw += ShowDrawScore;
            OnFinishCast += spell.BeginCooldown;
            OnFinishCast += (player) => StartCooldown();
            GameController.WhileInGame += UpdateCooldown;
        }
    }
    private void OnDisable()
    {
        if (_playerAction != null)
        {
            _playerAction.Player.Spell_Shift.Disable();
            _playerAction.Player.Spell_Shift.canceled -= Cast;
        }
        if (spell != null)
        {
            OnFinishDraw -= spell.CastSpell;
            OnFinishDraw -= ShowDrawScore;
            OnFinishCast -= spell.BeginCooldown;
            GameController.WhileInGame -= UpdateCooldown;
        }
    }
    void StartCooldown()
    {
        _isReadyToCast = false;
        cooldown = spell._cooldown;
    }
    void UpdateCooldown()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 0;
            _isReadyToCast = true;
        }
    }
    void ShowDrawScore(float score, GameObject player)
    {
        GameObject.Find("Draw score").GetComponent<TextMeshProUGUI>().text = "Draw Score: " + (score * 100).ToString("F2");
        GameObject.Find("Cast level").GetComponent<TextMeshProUGUI>().text = "Cast Level: " + spell.CalThreshold(score);
        drawInput.gameObject.SetActive(false);
    }




}