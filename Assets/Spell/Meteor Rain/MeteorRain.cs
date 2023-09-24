using DG.Tweening;
using DrawCaster.Util;
using UnityEngine;
using UnityEngine.InputSystem;


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
    public int meteorAmount1;
    public int meteorAmount2;
    public int meteorAmount3;
    public float meteorMoveSpeed;
    public float selfDestructTime => meteorMoveSpeed;

    [Header("Prefab")]
    public GameObject _meteorPrefab;
    public GameObject _selectPositionPrefab;
    [SerializeField] private Vector2 selectedPosition;
    Sequence sequenceCast;

    public override void CastSpell(float score, GameObject player)
    {

        sequenceCast = DOTween.Sequence();

        int castLevel = CalThreshold(score);
        selectedPosition = RandomPositon(player.transform.position, 7);

        bool hasClicked = false;

        sequenceCast.AppendCallback(() =>
        {
            PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
            playerAction.Player.LeftClick.Enable();
            playerAction.Player.LeftClick.canceled += context =>
            {
                SelectPosition();
                hasClicked = false;
            };
        }).AppendInterval(selectedPositionDuration)
          .AppendCallback(() =>
          {
              if (!hasClicked)
              {
                  GameObject targetPos = new GameObject();
                  targetPos.transform.position = selectedPosition;
                  CastByLevel(castLevel, player, targetPos);
                  Destroy(targetPos);
              }

          });
        sequenceCast.Play();
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
            });
            sequence.Append(meteors[index].transform.DOMove(RandomPositon(selectedPosition, 2), meteorMoveSpeed, false));
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
            int index = i;
            int randomSide = (Random.Range(0, 2) == 0) ? -1 : 1;
            sequence.AppendCallback(() =>
            {
                meteors[index].SetActive(true);
                meteors[index].transform.position = player.transform.position + new Vector3(randomSide * cameraOrthoSize, cameraOrthoSize + 3, 0);
            });
            sequence.Append(meteors[index].transform.DOMove(RandomPositon(selectedPosition, 2), meteorMoveSpeed, false));
        }
        sequence.AppendCallback(() =>
        {
            int index = meteors.Length - 1;
            meteors[index].transform.localScale = new Vector3(3, 3, 0);
            meteors[index].SetActive(true);
            meteors[index].transform.position = player.transform.position + new Vector3(cameraOrthoSize, cameraOrthoSize + 3, 0);
            meteors[index].GetComponent<AttackHit>().selfDestructTime = 0.8f;
            meteors[index].transform.DOMove(selectedPosition, 0.8f, false);
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
            int index = i;
            int randomSide = (Random.Range(0, 2) == 0) ? -1 : 1;
            sequence.AppendCallback(() =>
            {
                meteors[index].SetActive(true);
                meteors[index].transform.position = player.transform.position + new Vector3(randomSide * cameraOrthoSize, cameraOrthoSize + 3, 0);
            });
            sequence.Append(meteors[index].transform.DOMove(RandomPositon(selectedPosition, 2), meteorMoveSpeed, false));
        }
        sequence.AppendCallback(() =>
        {
            int index = meteors.Length - 1;
            meteors[index].transform.localScale = new Vector3(3, 3, 0);
            meteors[index].SetActive(true);
            meteors[index].transform.position = player.transform.position + new Vector3(cameraOrthoSize, cameraOrthoSize + 3, 0);
            meteors[index].GetComponent<AttackHit>().selfDestructTime = 0.8f;
            meteors[index].transform.DOMove(selectedPosition, 0.8f, false);
        });

    }
    public override void BeginCooldown(GameObject player)
    {

    }

    private GameObject SpawnMeteor(GameObject player, float multiplier)
    {
        GameObject meteor = DrawCasterUtil.AddAttackHitTo(
                        Instantiate(_meteorPrefab),
                        _elementalType,
                        player.GetComponent<CharactorManager<PlayerData>>().GetCharactorData(),
                        _baseSkillDamageMultiplier * multiplier,
                        selfDestructTime,
                        targetLayer
                        );
        meteor.SetActive(false);
        return meteor;
    }

    private void SelectPosition()
    {
        selectedPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.LeftClick.Disable();
        GameObject selectedPos = Instantiate(_selectPositionPrefab, (Vector3)selectedPosition, Quaternion.identity);
        selectedPos.GetComponent<SpriteRenderer>().DOFade(1f, selectedPositionDuration - 0.2f);
        selectedPos.transform.DOScale(4, selectedPositionDuration - 0.2f).OnComplete(() =>
        {
            Destroy(selectedPos);
        });
    }
    private Vector2 RandomPositon(Vector2 targetPosition, float radius)
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, Mathf.PI * 2);

        // Calculate a random position within the spawn radius
        Vector2 spawnPosition = targetPosition + new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * Random.Range(0, radius);
        return spawnPosition;
    }
}
