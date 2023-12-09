using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using DrawCaster.DataPersistence;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error while trying to load data from file" + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error while trying to save data to file" + fullPath + "\n" + e);
        }
    }

    public string SaveImageToFile(Texture2D texture, string folderName, string fileName)
    {
        // Ensure the folder exists
        string fullPath = Path.Combine(dataDirPath, "/" + folderName + "/" + fileName);
        Debug.Log((fullPath));
        if (!Directory.Exists(Path.Combine(dataDirPath, "/" + folderName)))
        {
            Directory.CreateDirectory(Path.Combine(dataDirPath, "/" + folderName));

        }
        Texture2D uncompressedTexture = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        uncompressedTexture.SetPixels(texture.GetPixels());
        uncompressedTexture.Apply();
        // Convert texture to bytes and save as PNG file
        byte[] bytes = uncompressedTexture.EncodeToPNG();
        string filePath = fullPath + ".png";
        File.WriteAllBytes(filePath, bytes);

        return filePath;
    }

    public Sprite LoadSpriteFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            // Read the file as bytes
            byte[] fileData = File.ReadAllBytes(filePath);

            // Create a new Texture2D and load the image from bytes
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData); // Automatically resizes the texture

            return ConvertTextureToSprite(texture);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return null;
        }
    }

    private Sprite ConvertTextureToSprite(Texture2D texture)
    {
        if (texture != null)
        {
            // Create a new Sprite using the loaded texture
            Sprite sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f) // Set pivot to the center, adjust as needed
            );

            return sprite;
        }
        else
        {
            Debug.LogError("Texture is null.");
            return null;
        }
    }
}
