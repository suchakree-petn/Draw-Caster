using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DrawCaster.DataPersistence
{

    [System.Serializable]
    public class GameData
    {
        public PlayerStat playerStat;
        public int Gold;
        public List<SpellData> player_spells;

        public GameData()
        {
            this.playerStat = new PlayerStat(0, 0, 0, 0, 0, 0);
            this.Gold = 0;
            this.player_spells = new();
        }
    }



    [System.Serializable]
    public struct PlayerStat
    {
        [Header("Info")]
        public int _level;

        [Header("Movement")]
        public float _moveSpeed;

        [Header("Health Point")]
        public float _hpBase;

        [Header("Mana Point")]
        public float _manaBase;

        [Header("Attack Point")]
        public float _attackBase;

        [Header("Defense Point")]
        public float _defenseBase;
        public PlayerStat(int _level, float _moveSpeed, float _hpBase, float _manaBase, float _attackBase, float _defenseBase)
        {
            this._level = 1;
            this._moveSpeed = 5;
            this._hpBase = 2000;
            this._manaBase = 300;
            this._attackBase = 5;
            this._defenseBase = 100;
        }
    }

    [System.Serializable]
    public struct SpellData
    {
        public string Name;
        public string Desc;
        public string spritePath;
        public SpellData(Spell spell)
        {
            this.Name = spell._name;
            this.Desc = spell._description;
            this.spritePath = DataPersistenceManager.Instance.dataHandler.SaveImageToFile(spell._icon.texture, "Spell Icon", spell._name + "_icon");
        }

    }
    // [System.Serializable]
    // public class SpriteData
    // {
    //     public string name;
    //     public float pixelsPerUnit;
    //     public Vector2 pivot;
    //     public SerializableTexture texture; // You might need to convert the texture separately
    //                                         // Add other necessary properties

    //     public SpriteData(Sprite sprite)
    //     {
    //         name = sprite.name;
    //         pixelsPerUnit = sprite.pixelsPerUnit;
    //         pivot = sprite.pivot;
    //         // Convert the sprite's texture to a SerializableTexture (see below)
    //         texture = new SerializableTexture(sprite.texture);
    //     }
    // }

    // [System.Serializable]
    // public class SerializableTexture
    // {
    //     public byte[] data;
    //     public int width;
    //     public int height;

    //     public SerializableTexture(Texture2D texture)
    //     {
    //         data = texture.GetRawTextureData();
    //         width = texture.width;
    //         height = texture.height;
    //     }
    // }
}
