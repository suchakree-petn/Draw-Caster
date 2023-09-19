using DG.Tweening;
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
    public GameObject _meteorPrefab;
    public GameObject _selectPositionPrefab;
    [SerializeField] private Vector2 selectedPosition;
    [SerializeField] private bool isSelected = false;
    Sequence sequenceCast;

    public override void CastSpell(float score, GameObject player)
    {

        // Create a new sequence
        sequenceCast = DOTween.Sequence();

        int castLevel = CalThreshold(score);
        selectedPosition = RandomPositon(player.transform.position, 7);

        // Define a flag to track if the player has clicked within the time limit
        bool hasClicked = false;

        sequenceCast.AppendCallback(() =>
        {
            PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
            playerAction.Player.LeftClick.Enable();
            playerAction.Player.LeftClick.canceled += context =>
            {
                SelectPosition(castLevel, player);
                hasClicked = false; // Set the flag to true when the player clicks
            };
        }).AppendInterval(selectedPositionDuration)
          .AppendCallback(() =>
          {

              // Check if the player has not clicked within the time limit
              if (!hasClicked)
              {
                  // Force cast at the previously selected position
                  GameObject targetPos = new GameObject();
                  targetPos.transform.position = selectedPosition;
                  CastByLevel(castLevel, player, targetPos);
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
            GameObject meteor = Instantiate(_meteorPrefab);
            meteor.transform.GetComponentInChildren<AttackHit>().elementalDamage = Elemental.DamageCalculation(
                _elementalType,
                player.GetComponent<PlayerManager>().playerData,
                _baseSkillDamageMultiplier * _damageSpellLevelMultiplier1);
            meteor.transform.GetComponentInChildren<AttackHit>().selfDestructTime = 0.23f;
            meteor.SetActive(false);
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
            sequence.Append(meteors[index].transform.DOMove(RandomPositon(selectedPosition, 2), 0.2f, false));
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
            GameObject meteor = Instantiate(_meteorPrefab);
            meteor.transform.GetComponentInChildren<AttackHit>().elementalDamage = Elemental.DamageCalculation(
                _elementalType,
                player.GetComponent<PlayerManager>().playerData,
                _baseSkillDamageMultiplier * _damageSpellLevelMultiplier2);
            if (i != meteorAmount2 - 1)
            {
                meteor.transform.GetComponentInChildren<AttackHit>().selfDestructTime = 0.23f;
            }
            else
            {
                meteor.transform.GetComponentInChildren<AttackHit>().selfDestructTime = 1f;

            }
            meteor.SetActive(false);
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
            sequence.Append(meteors[index].transform.DOMove(RandomPositon(selectedPosition, 2), 0.2f, false));
        }
        sequence.AppendCallback(() =>
        {
            int index = meteors.Length - 1;
            meteors[index].transform.DOScale(meteors[index].transform.localScale.x * 3, 0);
            meteors[index].SetActive(true);
            meteors[index].transform.position = player.transform.position + new Vector3(cameraOrthoSize, cameraOrthoSize + 3, 0);
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
            GameObject meteor = Instantiate(_meteorPrefab);
            meteor.transform.GetComponentInChildren<AttackHit>().elementalDamage = Elemental.DamageCalculation(
                _elementalType,
                player.GetComponent<PlayerManager>().playerData,
                _baseSkillDamageMultiplier * _damageSpellLevelMultiplier3);
            if (i != meteorAmount3 - 1)
            {
                meteor.transform.GetComponentInChildren<AttackHit>().selfDestructTime = 0.23f;
            }
            else
            {
                meteor.transform.GetComponentInChildren<AttackHit>().selfDestructTime = 1f;

            }
            meteor.SetActive(false);
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
            sequence.Append(meteors[index].transform.DOMove(RandomPositon(selectedPosition, 2), 0.2f, false));
        }
        sequence.AppendCallback(() =>
        {
            int index = meteors.Length - 1;
            meteors[index].transform.DOScale(meteors[index].transform.localScale.x * 3, 0);
            meteors[index].SetActive(true);
            meteors[index].transform.position = player.transform.position + new Vector3(cameraOrthoSize, cameraOrthoSize + 3, 0);
            meteors[index].transform.DOMove(selectedPosition, 0.8f, false);
        });

    }
    public override void BeginCooldown(GameObject player)
    {

    }


    private void SelectPosition(int castLevel, GameObject player)
    {
        selectedPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        PlayerAction playerAction = PlayerInputSystem.Instance.playerAction;
        playerAction.Player.LeftClick.Disable();
        GameObject selectedPos = Instantiate(_selectPositionPrefab, (Vector3)selectedPosition, Quaternion.identity);
        selectedPos.GetComponent<SpriteRenderer>().DOFade(1f, selectedPositionDuration - 0.2f);
        selectedPos.transform.DOScale(4, selectedPositionDuration - 0.2f).OnComplete(() => Destroy(selectedPos));
        // GameObject targetPos = new GameObject();
        // targetPos.transform.position = selectedPosition;
        // CastByLevel(castLevel, player, targetPos);
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
