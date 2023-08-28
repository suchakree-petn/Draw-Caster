using UnityEngine;
using System.Linq;


public class CosSim : MonoBehaviour
{
    [SerializeField] private Texture2D userInput;
    [SerializeField] private Texture2D template;
    public float[] vectorA;
    public float[] vectorB;

    public static CosSim Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        MousePosList.CalcCosSim += CosineSimilarity;
    }
    private void OnDisable()
    {
        MousePosList.CalcCosSim -= CosineSimilarity;

    }
    private void Start()
    {
        BlackShadePositions.Instance.sourceTexture = userInput;
        Vector2[] input = BlackShadePositions.Instance.FindBlackShadePositions();
    
        BlackShadePositions.Instance.sourceTexture = template;
        Vector2[] tp = BlackShadePositions.Instance.FindBlackShadePositions();

        CosineSimilarity(input, tp, 16);
    }
    // Assumes the images are grayscale and have the same dimensions
    private void CosineSimilarity(Vector2[] input, Vector2[] template, int sideLenght)
    {
        // Convert to float[]
        int index = 0;
        vectorA = new float[sideLenght * sideLenght];
        Debug.Log(sideLenght);
        for (int x = 0; x < sideLenght; x++)
        {
            for (int y = 0; y < sideLenght; y++)
            {
                if (input.ToList().Contains(new Vector2(x, y)))
                {
                    vectorA[index] = 1;
                    Debug.Log("Exist at: " + new Vector2(x, y));
                }
                else
                {
                    vectorA[index] = 0;
                }
                index++;
            }
        }
        index = 0;
        vectorB = new float[sideLenght * sideLenght];
        for (int x = 0; x < sideLenght; x++)
        {
            for (int y = 0; y < sideLenght; y++)
            {
                if (template.ToList().Contains(new Vector2(x, y)))
                {
                    vectorB[index] = 1;
                }
                else
                {
                    vectorB[index] = 0;
                }
                index++;
            }
        }

        float dotProduct = 0;
        float normA = 0;
        float normB = 0;

        for (int i = 0; i < vectorA.Length; i++)
        {
            dotProduct += vectorA[i] * vectorB[i];
            normA += vectorA[i] * vectorA[i];
            normB += vectorB[i] * vectorB[i];
        }

        // Avoid division by zero
        if (normA == 0 || normB == 0)
        {
            Debug.Log("Not Match");
            return;
        }


        Debug.Log("Cosine Similarity: " + dotProduct / (Mathf.Sqrt(normA) * Mathf.Sqrt(normB)));
    }


    private float[] FlattenImage(Texture2D image)
    {
        int width = image.width;
        int height = image.height;
        Color[] pixels = image.GetPixels();

        float[] flattened = new float[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                flattened[index] = pixels[index].grayscale;
            }
        }

        return flattened;
    }
    // public void ClearList()
    // {
    //     vectorA.ToList().Clear();
    //     vectorB.ToList().Clear();

    // }

}