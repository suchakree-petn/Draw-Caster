
using UnityEngine;

public abstract class Spell : ScriptableObject
{

    [Header("Infomation")]
    public string _name;
    public string _description;
    public Sprite _icon;

    [Header("Spell Setting")]
    public int _level;
    public SpellType _spellType;
    public ElementalType _elementalType;
    public float _cooldown;
    public float _manaCost;

    public LayerMask targetLayer;


    [Header("Draw Input")]
    public Texture2D _templateImage;
    public Sprite UI_image;
    public float _lowThreshold;
    public float _midThreshold;

    public virtual void CastSpell(float score, GameObject player)
    {
    }

    

    public abstract void Cast1(GameObject player, GameObject target);
    public abstract void Cast2(GameObject player, GameObject target);
    public abstract void Cast3(GameObject player, GameObject target);

    public void CastByLevel(int level, GameObject player, GameObject target)
    {
        Debug.Log("Cast by level");
        switch (level)
        {
            case 1:
                Cast1(player, target);
                break;
            case 2:
                Cast2(player, target);
                break;
            case 3:
                Cast3(player, target);
                break;
            default:
                Debug.LogWarning("Level Error");
                break;
        }
        EnableInput();
    }

    public abstract void BeginCooldown(GameObject player);
    public int CalThreshold(float score)
    {
        float low = _lowThreshold;
        float mid = _midThreshold;

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
    
    private void EnableInput()
    {
        PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.PressAttack.Enable();
        playerAction.Player.HoldAttack.Enable();
        playerAction.Player.Spell_Q.Enable();
        playerAction.Player.Spell_E.Enable();
        playerAction.Player.Spell_R.Enable();
        playerAction.Player.Spell_Shift.Enable();
        playerAction.Player.Interact.Enable();
        playerAction.Player.LeftClick.Enable();
        ManaNullify.Instance.gameObject.SetActive(true);
    }
}

public enum SpellType
{
    Default,
    QuickCast,
    Concentrate
}


