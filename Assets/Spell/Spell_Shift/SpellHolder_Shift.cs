using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SpellHolder_Shift : MonoBehaviour
{
    public PlayerAction _playerAction;

    [SerializeField] private Spell spell;
    [Header("Draw Input")]
    public DrawInput_Shift drawInput;
    public delegate void InputCompare(float score);
    public static InputCompare OnFinishDraw;



    public void Cast(InputAction.CallbackContext context)
    {
        if (CheckMana(spell.spellObj) && spell._isReadyToCast)
        {
            ReceiveDrawInput();
        }
    }
    public bool CheckMana(SpellObj spellObj)
    {
        PlayerData _playerData = transform.root.GetComponent<PlayerManager>().playerData;
        if (_playerData.currentMana >= spellObj._manaCost)
        {
            return true;
        }
        return false;
    }
    public void ReceiveDrawInput()
    {
        drawInput.UI_image = spell.spellObj.UI_image;
        drawInput.templateUI.sprite = spell.spellObj.UI_image;

        drawInput.gameObject.SetActive(true);
        drawInput.inputPos.Clear();
        drawInput.templatePos = BlackShadePositions.FindBlackShadePositions(spell.spellObj._templateImage);
    }
    private void OnEnable()
    {

        _playerAction = PlayerInputSystem.Instance.playerAction;

        _playerAction.Player.Spell_Shift.Enable();
        _playerAction.Player.Spell_Shift.canceled += Cast;
        OnFinishDraw += spell.CastSpell;
        OnFinishDraw += ShowDrawScore;

    }
    private void OnDisable()
    {
        _playerAction.Player.Spell_Shift.Disable();
        _playerAction.Player.Spell_Shift.canceled -= Cast;
        OnFinishDraw -= spell.CastSpell;
        OnFinishDraw -= ShowDrawScore;
    }
    void ShowDrawScore(float score)
    {
        GameObject.Find("Draw score").GetComponent<TextMeshProUGUI>().text = "Draw Score: " + (score * 100).ToString("F2");
        GameObject.Find("Cast level").GetComponent<TextMeshProUGUI>().text = "Cast Level: " + spell.CalThreshold(score);
        drawInput.gameObject.SetActive(false);

    }




}