using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mana Nullify Data")]
public class ManaNullifyData : ScriptableObject
{
    public float cooldown;
    public List<Sprite> sprites;
    public List<Texture2D> textures;

    public NullifyMark GetRandomMark()
    {
        int index = Random.Range(0, sprites.Count);
        return new NullifyMark(sprites[index], textures[index]);
    }
}
[System.Serializable]
public struct NullifyMark
{
    public Sprite sprite;
    public Texture2D texture;
    public NullifyMark(Sprite sprite, Texture2D texture)
    {
        this.sprite = sprite;
        this.texture = texture;
    }
}
