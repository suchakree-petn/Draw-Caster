using UnityEngine;

public class BlackShadePositions
{
    public static Vector2[] FindBlackShadePositions(Texture2D sourceTexture)
    {
        Color[] pixels = sourceTexture.GetPixels();
        int width = sourceTexture.width;
        int height = sourceTexture.height;
        int count = 0;

        // Count the number of pixels within the desired color tolerance range
        for (int i = 0; i < pixels.Length; i++)
        {
            if (ColorWithinTolerance(pixels[i], Color.black, 0.1f))
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
                if (ColorWithinTolerance(pixels[pixelIndex], Color.black, 0.1f))
                {
                    positions[index] = new Vector2(x, y);
                    index++;
                }
            }
        }

        return positions;
    }

    private static bool ColorWithinTolerance(Color colorA, Color colorB, float tolerance)
    {
        float colorDifference = Mathf.Abs(colorA.r - colorB.r) + Mathf.Abs(colorA.g - colorB.g) + Mathf.Abs(colorA.b - colorB.b);
        return colorDifference <= tolerance;
    }
}
