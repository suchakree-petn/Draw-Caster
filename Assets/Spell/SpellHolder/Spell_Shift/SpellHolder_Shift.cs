using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

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
                SoundSource.Instance.PlaySfxCastSpell();
                DisableInput();
                ReceiveDrawInput();
                PayManaCost();
                OnFinishCast?.Invoke(transform.root.gameObject);
            }else{
                SoundSource.Instance.PlaySfxCastSpellFail();
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
        ManaNullify.Instance.gameObject.SetActive(false);
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
    [SerializeField] private float ShowScoreTime;
    void ShowDrawScore(float score, GameObject player)
    {
        // GameObject.Find("Cast level").GetComponent<TextMeshProUGUI>().text = "Cast Level: " + spell.CalThreshold(score);
        // GameObject.Find("Draw score").GetComponent<TextMeshProUGUI>().text =(score * 100).ToString("F2") + "%";
        float castLevel = spell.CalThreshold(score);
        if(castLevel == 1){
            GameObject.Find("Cast level").GetComponent<TextMeshProUGUI>().text = "I";
            SoundSource.Instance.PlaySfxCast1();
        }else if(castLevel == 2){
            GameObject.Find("Cast level").GetComponent<TextMeshProUGUI>().text = "II";
            SoundSource.Instance.PlaySfxCast2();
        }else if(castLevel == 3){
            GameObject.Find("Cast level").GetComponent<TextMeshProUGUI>().text = "III";
            SoundSource.Instance.PlaySfxCast3();
        }
        StartCoroutine(DelayShowDrawScore(ShowScoreTime));
        drawInput.gameObject.SetActive(false);
    }

    IEnumerator DelayShowDrawScore(float delayTime){
        yield return new WaitForSeconds(delayTime);
        GameObject.Find("Draw score").GetComponent<TextMeshProUGUI>().text = "";
        GameObject.Find("Cast level").GetComponent<TextMeshProUGUI>().text = "";
    }




}