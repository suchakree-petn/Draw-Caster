using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.InputSystem;

public class DrawInput_Q : MonoBehaviour
{
    public Sprite UI_image;
    public Image templateUI;
    public List<Vector2> inputPos = new List<Vector2>();
    public Vector2[] templatePos;
    public float score;
    public int sideLenght;
    PlayerAction _playerAction => PlayerInputSystem.Instance.playerAction;

    [Header("Matrix Dimension")]
    public float mostLeft;
    public float mostRight;
    public float mostAbove;
    public float mostButtom;

    [Header("New Matrix Dimension")]
    public float newMostRight;

    [Header("Draw List")]
    [SerializeField] private GameObject prefab;
    public delegate void MouseEvent(Vector2[] templatePos);
    public static MouseEvent up;
    public static MouseEvent down;
    public delegate void CalcCosSim(Vector2[] input, Vector2[] template, int sideLenght);
    public static CalcCosSim calcCosSim;

    private void Update()
    {
        if (_playerAction.Player.DrawInput.IsPressed())
        {
            Draw();
        }

    }
    void Draw()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        if (!inputPos.Contains(mousePos))
        {
            inputPos.Add(mousePos);
        }
    }
    void ResamplingMouseInputPos(InputAction.CallbackContext context)
    {
        mostLeft = float.PositiveInfinity;
        foreach (Vector2 p in inputPos)
        {
            if (p.x < mostLeft)
            {
                mostLeft = p.x;
            }
        }
        mostButtom = float.PositiveInfinity;
        foreach (Vector2 p in inputPos)
        {
            if (p.y < mostButtom)
            {
                mostButtom = p.y;
            }
        }
        mostAbove = float.NegativeInfinity;
        foreach (Vector2 p in inputPos)
        {
            if (p.y > mostAbove)
            {
                mostAbove = p.y;
            }
        }

        mostRight = float.NegativeInfinity;
        foreach (Vector2 p in inputPos)
        {
            if (p.x > mostRight)
            {
                mostRight = p.x;
            }
        }
        inputPos = Resample(inputPos.ToArray(), templatePos.Length);

        for (int i = 0; i < inputPos.Count; i++)
        {
            if (mostLeft > 0)
            {

                inputPos[i] = new Vector2(inputPos[i].x - mostLeft, inputPos[i].y);
            }
            else if (mostLeft < 0)
            {
                inputPos[i] = new Vector2(inputPos[i].x - mostLeft, inputPos[i].y);
            }

            if (mostButtom > 0)
            {
                inputPos[i] = new Vector2(inputPos[i].x, inputPos[i].y - mostButtom);
            }
            else if (mostButtom < 0)
            {
                inputPos[i] = new Vector2(inputPos[i].x, inputPos[i].y - mostButtom);
            }
        }

        float TmostLeft = float.PositiveInfinity;
        foreach (Vector2 p in templatePos)
        {
            if (p.x < TmostLeft)
            {
                TmostLeft = p.x;
            }
        }
        float TmostButtom = float.PositiveInfinity;
        foreach (Vector2 p in templatePos)
        {
            if (p.y < TmostButtom)
            {
                TmostButtom = p.y;

            }
        }
        float TmostAbove = float.NegativeInfinity;
        foreach (Vector2 p in templatePos)
        {
            if (p.y > TmostAbove)
            {
                TmostAbove = p.y;
            }
        }
        float TmostRight = float.NegativeInfinity;
        foreach (Vector2 p in templatePos)
        {
            if (p.x > TmostRight)
            {
                TmostRight = p.x;
            }
        }
        for (int i = 0; i < templatePos.Length; i++)
        {
            templatePos[i] = new Vector2(templatePos[i].x - TmostLeft, templatePos[i].y);
            templatePos[i] = new Vector2(templatePos[i].x, templatePos[i].y - TmostButtom);

        }
        float TWidth = TmostRight - TmostLeft;
        float width = mostRight - mostLeft;
        float THeight = TmostAbove - TmostButtom;
        float height = mostAbove - mostButtom;
        if (TWidth < THeight)
        {
            TWidth += THeight - TWidth;
        }
        else if (TWidth > THeight)
        {
            THeight += TWidth - THeight;
        }

        if (width < height)
        {
            width += height - width;
        }
        else if (width > height)
        {
            height += width - height;
        }

        float scale = TWidth / width;
        for (int i = 0; i < inputPos.Count; i++)
        {
            inputPos[i] *= scale;
            Vector2 roundPixel = new Vector2(Mathf.Round(inputPos[i].x), Mathf.Round(inputPos[i].y));
            inputPos[i] = roundPixel;
        }
        inputPos = RemoveDuplicates(inputPos);
        inputPos = sortList(inputPos);


        newMostRight = float.NegativeInfinity;
        foreach (Vector2 p in templatePos)
        {
            if (p.x > newMostRight)
            {
                newMostRight = p.x;
            }
        }
        sideLenght = 16;
        score = CosSim.CosineSimilarity(inputPos.ToArray(),
                                templatePos,
                                sideLenght);
        SpellHolder_Q.OnFinishDraw?.Invoke(score,transform.root.gameObject);
    }

    private List<Vector2> Resample(Vector2[] originalArray, int newLength)
    {
        Vector2[] resampledArray = new Vector2[newLength];

        float scale = (float)(originalArray.Length - 1) / (newLength - 1);

        for (int i = 0; i < newLength; i++)
        {
            float origIndex = i * scale;
            int index0 = Mathf.FloorToInt(origIndex);
            int index1 = Mathf.CeilToInt(origIndex);
            if (index0 < 0)
            {
                index0 = 0;
            }
            else if (index1 > originalArray.Length - 1)
            {
                index1 = originalArray.Length;
            }
            Vector2 interpValue = Vector2.Lerp(originalArray[index0], originalArray[index1], origIndex - index0);

            resampledArray[i] = interpValue;
        }

        return resampledArray.ToList();
    }
    private List<Vector2> RemoveDuplicates(List<Vector2> list)
    {
        HashSet<Vector2> seenElements = new HashSet<Vector2>();
        List<Vector2> uniqueElements = new List<Vector2>();

        foreach (Vector2 element in list)
        {
            if (seenElements.Add(element))
            {
                // If the element is unique, add it to the list
                uniqueElements.Add(element);
            }
        }

        return uniqueElements;
    }
    private List<Vector2> sortList(List<Vector2> list)
    {

        List<Vector2> sortList = list;
        sortList.Sort((v1, v2) =>
        {
            int yComparison = v1.y.CompareTo(v2.y);
            if (yComparison != 0)
                return yComparison;
            else
                return v1.x.CompareTo(v2.x);
        });

        return sortList;
    }
    private void OnEnable()
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = Time.timeScale * 0.01f;

        _playerAction.Player.DrawInput.Enable();
        _playerAction.Player.DrawInput.canceled += ResamplingMouseInputPos;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        _playerAction.Player.DrawInput.Disable();
        _playerAction.Player.DrawInput.canceled -= ResamplingMouseInputPos;
    }
}




