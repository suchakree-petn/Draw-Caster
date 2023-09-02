using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class MousePosList : MonoBehaviour
{
    public static MousePosList Instance;
    public List<Vector2> pos;
    //public Vector2[] templatePos;
    [SerializeField] private Texture2D template;
    public int sideLenght;

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

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            pos.Clear();
        }
        if (Input.GetMouseButton(1))
        {
            down?.Invoke(BlackShadePositions.FindBlackShadePositions(template));
        }
        if (Input.GetMouseButtonUp(1))
        {
            up?.Invoke(BlackShadePositions.FindBlackShadePositions(template));
        }

    }
    void Draw(Vector2[] templatePos)
    {
        Vector3 mosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!pos.Contains(mosPos))
        {
            mosPos.z = 0;
            pos.Add(mosPos);

        }
    }
    void ResamplingMouseInputPos(Vector2[] templatePos)
    {
        mostLeft = float.PositiveInfinity;
        foreach (Vector2 p in pos)
        {
            if (p.x < mostLeft)
            {
                mostLeft = p.x;


                GameObject go = Instantiate(prefab, new Vector3(p.x, p.y, 0), Quaternion.identity);
                go.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
        mostButtom = float.PositiveInfinity;
        foreach (Vector2 p in pos)
        {
            if (p.y < mostButtom)
            {
                mostButtom = p.y;
                GameObject go = Instantiate(prefab, new Vector3(p.x, p.y, 0), Quaternion.identity);
                go.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
        mostAbove = float.NegativeInfinity;
        foreach (Vector2 p in pos)
        {
            if (p.y > mostAbove)
            {
                mostAbove = p.y;
                GameObject go = Instantiate(prefab, new Vector3(p.x, p.y, 0), Quaternion.identity);
                go.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }

        mostRight = float.NegativeInfinity;
        foreach (Vector2 p in pos)
        {
            if (p.x > mostRight)
            {
                mostRight = p.x;

                // Show raw draw input
                GameObject go = Instantiate(prefab, new Vector3(p.x, p.y, 0), Quaternion.identity);
                go.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
        pos = Resample(pos.ToArray(), templatePos.Length).ToList();

        for (int i = 0; i < pos.Count; i++)
        {
            if (mostLeft > 0)
            {

                pos[i] = new Vector2((pos[i].x - mostLeft), pos[i].y);
            }
            else if (mostLeft < 0)
            {
                pos[i] = new Vector2(pos[i].x - mostLeft, pos[i].y);
            }

            if (mostButtom > 0)
            {
                pos[i] = new Vector2(pos[i].x, pos[i].y - mostButtom);
            }
            else if (mostButtom < 0)
            {
                pos[i] = new Vector2(pos[i].x, pos[i].y - mostButtom);
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

        // Show template img
        // foreach (Vector2 pos in templatePos)
        // {
        //     GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        //     go.GetComponent<SpriteRenderer>().color = Color.red;
        // }

        // re-scale
        float TWidth = (TmostRight - TmostLeft);
        float width = (mostRight - mostLeft);
        float THeight = (TmostAbove - TmostButtom);
        float height = (mostAbove - mostButtom);
        float scale = 1;
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
            width += (height - width);
        }
        else if (width > height)
        {
            height += (width - height);
        }

        scale = TWidth / width;
        for (int i = 0; i < pos.Count; i++)
        {
            pos[i] *= scale;
            Vector2 roundPixel = new Vector2(Mathf.Round(pos[i].x), Mathf.Round(pos[i].y));
            pos[i] = roundPixel;

            // Show draw input resampled img
            //Instantiate(prefab, new Vector3(pos[i].x, pos[i].y, 0), Quaternion.identity);
        }
        pos = RemoveDuplicates(pos.ToArray()).ToList();
        pos = sortList(pos);


        newMostRight = float.NegativeInfinity;
        foreach (Vector2 p in templatePos)
        {
            if (p.x > newMostRight)
            {
                newMostRight = p.x;
            }
        }
        sideLenght = 16;
        CosSim.CosineSimilarity(pos.ToArray(),
                                templatePos,
                                sideLenght);
    }

    private Vector2[] Resample(Vector2[] originalArray, int newLength)
    {
        Vector2[] resampledArray = new Vector2[newLength];

        float scale = (float)(originalArray.Length - 1) / (newLength - 1);

        for (int i = 0; i < newLength; i++)
        {
            float origIndex = i * scale;
            int index0 = Mathf.FloorToInt(origIndex);
            int index1 = Mathf.CeilToInt(origIndex);

            Vector2 interpValue = Vector2.Lerp(originalArray[index0], originalArray[index1], origIndex - index0);

            resampledArray[i] = interpValue;
        }

        return resampledArray;
    }
    private Vector2[] RemoveDuplicates(Vector2[] array)
    {
        HashSet<Vector2> seenElements = new HashSet<Vector2>();
        List<Vector2> uniqueElements = new List<Vector2>();

        foreach (Vector2 element in array)
        {
            if (seenElements.Add(element))
            {
                // If the element is unique, add it to the list
                uniqueElements.Add(element);
            }
        }

        return uniqueElements.ToArray();
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
        up += ResamplingMouseInputPos;
        down += Draw;

    }
    private void OnDisable()
    {
        up -= ResamplingMouseInputPos;
        down -= Draw;


    }
}




