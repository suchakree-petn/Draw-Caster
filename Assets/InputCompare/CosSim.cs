using UnityEngine;
using System.Linq;


public class CosSim : MonoBehaviour
{
    public static float CosineSimilarity(Vector2[] input, Vector2[] template, int sideLenght)
    {
        float[] vectorA;
        float[] vectorB;
        int index = 0;
        vectorA = new float[sideLenght * sideLenght];
        for (int x = 0; x < sideLenght; x++)
        {
            for (int y = 0; y < sideLenght; y++)
            {
                if (input.ToList().Contains(new Vector2(x, y)))
                {
                    vectorA[index] = 1;
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
            return 0;
        }

        float score = dotProduct / (Mathf.Sqrt(normA) * Mathf.Sqrt(normB));
        Debug.Log("Cosine Similarity: " + score);
        return score;
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

}