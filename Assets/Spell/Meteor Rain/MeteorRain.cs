using DG.Tweening;
using DrawCaster.Util;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new Meteor Rain", menuName = "Spell/Meteor Rain")]
public class MeteorRain : Spell
{
    [Header("Damage Multiplier")]
    public float _baseSkillDamageMultiplier;
    public float _damageSpellLevelMultiplier1;
    public float _damageSpellLevelMultiplier2;
    public float _damageSpellLevelMultiplier3;
    [Header("Spell Setting")]
    public float selectedPositionDuration;
    public float knockbackGaugeDeal;
    public float iFrame;
    public int meteorAmount1;
    public int meteorAmount2;
    public int meteorAmount3;
    public float meteorMoveSpeed;
    public float randomPositionRadius;
    public float selfDestructTime => meteorMoveSpeed;

    [Header("Prefab")]
    public GameObject _meteorPrefab;
    public GameObject _selectPositionPrefab;
    public GameObject DurationBarPrefab;
    public AnimationClip boomEffectClip;
    [SerializeField] private Vector2 selectedPosition;
    [SerializeField] private bool isSelected;

    public override void CastSpell(float score, GameObject player)
    {
        base.CastSpell(score, player);
        Sequence sequenceCast = DOTween.Sequence();

        int castLevel = CalThreshold(score);

        isSelected = false;
        GameObject selectedPos = Instantiate(_selectPositionPrefab, DrawCasterUtil.GetCurrentMousePosition(), Quaternion.identity);
        selectedPos.GetComponent<SpriteRenderer>().DOFade(1f, selectedPositionDuration - 0.2f);
        selectedPos.transform.DOScale(4, selectedPositionDuration - 0.2f)
        .OnUpdate(() =>
        {
            if (!isSelected)
            {
                selectedPos.transform.position = DrawCasterUtil.GetCurrentMousePosition();
            }
        })
        .SetUpdate(true)
        .OnComplete(() =>
        {
            Destroy(selectedPos);
        });

        sequenceCast.AppendCallback(() =>
        {
            PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
            playerAction.Player.LeftClick.Enable();
            playerAction.Player.LeftClick.canceled += context =>
            {
                playerAction.Player.LeftClick.Disable();
                SelectPosition();
            };
            GameObject durationBar = Instantiate(DurationBarPrefab, GameObject.Find("PlayerUI").transform);
            Slider slider = durationBar.GetComponent<Slider>();
            slider.value = 1;
            slider.DOValue(0, selectedPositionDuration)
            .SetUpdate(true)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Destroy(slider.gameObject);
            });
        });
        sequenceCast.AppendInterval(selectedPositionDuration);
        sequenceCast.AppendCallback(() =>
          {
              if (!isSelected)
              {
                  selectedPosition = DrawCasterUtil.GetCurrentMousePosition();
              }
              GameObject targetPos = new GameObject();
              targetPos.transform.position = selectedPosition;
              CastByLevel(castLevel, player, targetPos);
              Destroy(targetPos);
          });
    }

    public override void Cast1(GameObject player, GameObject target)
    {
        if (player == null) { return; }
        float cameraOrthoSize = Camera.main.orthographicSize;
        GameObject[] meteors = new GameObject[meteorAmount1];
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < meteorAmount1; i++)
        {
            GameObject meteor = SpawnMeteor(player, _damageSpellLevelMultiplier1);
            meteors[i] = meteor;
        }

        for (int i = 0; i < meteors.Length; i++)
        {
            int index = i;
            int randomSide = (Random.Range(0, 2) == 0) ? -1 : 1;
            sequence.AppendCallback(() =>
            {
                meteors[index].SetActive(true);
                meteors[index].transform.position = player.transform.position + new Vector3(randomSide * cameraOrthoSize, cameraOrthoSize + 3, 0);
                meteors[index].transform.right = (Vector3)selectedPosition - meteors[index].transform.position;
            });
            sequence.Append(meteors[index].transform.DOMove(DrawCasterUtil.RandomPosition(selectedPosition, randomPositionRadius), meteorMoveSpeed, false).OnComplete(() => BoomEffect(meteors[index])));
        }
    }
    public override void Cast2(GameObject player, GameObject target)
    {
        if (player == null) { return; }
        float cameraOrthoSize = Camera.main.orthographicSize;
        GameObject[] meteors = new GameObject[meteorAmount2];
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < meteorAmount2; i++)
        {
            GameObject meteor = SpawnMeteor(player, _damageSpellLevelMultiplier2);
            meteors[i] = meteor;
        }

        for (int i = 0; i < meteors.Length - 1; i++)
        {
            int ind = i;
            int randomSide = (Random.Range(0, 2) == 0) ? -1 : 1;
            sequence.AppendCallback(() =>
            {
                meteors[ind].SetActive(true);
                meteors[ind].transform.position = player.transform.position + new Vector3(randomSide * cameraOrthoSize, cameraOrthoSize + 3, 0);
                meteors[ind].transform.right = (Vector3)selectedPosition - meteors[ind].transform.position;
            });
            sequence.Append(meteors[ind].transform.DOMove(DrawCasterUtil.RandomPosition(selectedPosition, randomPositionRadius), meteorMoveSpeed, false).OnComplete(() => BoomEffect(meteors[ind])));
        }
        sequence.OnComplete(() =>
        {
            int index = meteors.Length - 1;
            meteors[index].transform.localScale *= 2;
            meteors[index].SetActive(true);
            meteors[index].transform.position = player.transform.position + new Vector3(cameraOrthoSize, cameraOrthoSize + 3, 0);
            // meteors[index].GetComponent<AttackHit>().selfDestructTime = 0.8f;
            meteors[index].GetComponent<AttackHit>().elementalDamage = Elemental.DamageCalculation(
                            _elementalType,
                            player,
                            _baseSkillDamageMultiplier * _damageSpellLevelMultiplier2 * 2,
                            targetLayer,
                            knockbackGaugeDeal * 2
                            );
            meteors[index].transform.right = (Vector3)selectedPosition - meteors[index].transform.position;
            meteors[index].transform.DOMove(selectedPosition, 0.8f, false).OnComplete(() => BoomEffect(meteors[index]));
        });
    }
    public override void Cast3(GameObject player, GameObject target)
    {
        if (player == null) { return; }
        float cameraOrthoSize = Camera.main.orthographicSize;
        GameObject[] meteors = new GameObject[meteorAmount3];
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < meteorAmount3; i++)
        {
            GameObject meteor = SpawnMeteor(player, _damageSpellLevelMultiplier3);
            meteors[i] = meteor;
        }

        for (int i = 0; i < meteors.Length - 1; i++)
        {
            int ind = i;
            int randomSide = (Random.Range(0, 2) == 0) ? -1 : 1;
            sequence.AppendCallback(() =>
            {
                meteors[ind].SetActive(true);
                meteors[ind].transform.position = player.transform.position + new Vector3(randomSide * cameraOrthoSize, cameraOrthoSize + 3, 0);
                meteors[ind].transform.right = (Vector3)selectedPosition - meteors[ind].transform.position;
            });
            sequence.Append(meteors[ind].transform.DOMove(DrawCasterUtil.RandomPosition(selectedPosition, randomPositionRadius), meteorMoveSpeed, false).OnComplete(() => BoomEffect(meteors[ind])));
        }
        sequence.OnComplete(() =>
        {
            int index = meteors.Length - 1;
            meteors[index].transform.localScale *= 3;
            meteors[index].SetActive(true);
            meteors[index].transform.position = player.transform.position + new Vector3(cameraOrthoSize, cameraOrthoSize + 3, 0);
            // meteors[index].GetComponent<AttackHit>().selfDestructTime = 0.8f;
            meteors[index].GetComponent<AttackHit>().elementalDamage = Elemental.DamageCalculation(
                            _elementalType,
                            player,
                            _baseSkillDamageMultiplier * _damageSpellLevelMultiplier3 * 2.5f,
                            targetLayer,
                            knockbackGaugeDeal * 2.5f
                            );
            meteors[index].transform.right = (Vector3)selectedPosition - meteors[index].transform.position;
            meteors[index].transform.DOMove(selectedPosition, 0.8f, false).OnComplete(() => BoomEffect(meteors[index]));
        });
    }
    public override void BeginCooldown(GameObject player)
    {

    }
    void BoomEffect(GameObject meteor)
    {
        Animator animator = meteor.transform.GetComponentInChildren<Animator>();
        animator.SetTrigger("Explosion");
        Destroy(meteor, boomEffectClip.length);
    }
    private GameObject SpawnMeteor(GameObject player, float multiplier)
    {
        GameObject meteor = DrawCasterUtil.AddAttackHitTo(
                        Instantiate(_meteorPrefab),
                        _elementalType,
                        player,
                        _baseSkillDamageMultiplier * multiplier,
                        99,
                        targetLayer,
                        knockbackGaugeDeal,
                        iFrame
                        );
        meteor.SetActive(false);
        return meteor;
    }

    private void SelectPosition()
    {
        isSelected = true;
        selectedPosition = DrawCasterUtil.GetCurrentMousePosition();
        PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.LeftClick.Disable();

    }
}
