using UnityEngine;

public class BlackShadePositions : MonoBehaviour
{
    public static BlackShadePositions Instance;

    public Texture2D sourceTexture;
    public Color targetColor; // The color you want to consider as "black"
    public float colorTolerance = 0.1f; // Tolerance value for color comparison


    private void Awake()
    {
        Instance = this;
    }
    public Vector2[] FindBlackShadePositions()
    {
        Color[] pixels = sourceTexture.GetPixels();
        int width = sourceTexture.width;
        int height = sourceTexture.height;
        int count = 0;

        // Count the number of pixels within the desired color tolerance range
        for (int i = 0; i < pixels.Length; i++)
        {
            if (ColorWithinTolerance(pixels[i], targetColor, colorTolerance))
            {
                count++;
            }
        }

        // Store positions of pixels within the desired color tolerance range
        Vector2[] positions = new Vector2[count];
        int index = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int pixelIndex = y * width + x;
                if (ColorWithinTolerance(pixels[pixelIndex], targetColor, colorTolerance))
                {
                    positions[index] = new Vector2(x, y);
                    index++;
                }
            }
        }

        return positions;
    }

    private bool ColorWithinTolerance(Color colorA, Color colorB, float tolerance)
    {
        float colorDifference = Mathf.Abs(colorA.r - colorB.r) + Mathf.Abs(colorA.g - colorB.g) + Mathf.Abs(colorA.b - colorB.b);
        return colorDifference <= tolerance;
    }
}
