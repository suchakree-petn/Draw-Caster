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
        public double Gold;
        public List<SpellData> all_spells;
        public List<string> player_equiped_spells;
        public List<DimensionData> dimensionData;
        public float last_play_time;

        public GameData()
        {
            this.playerStat = new PlayerStat(0, 0, 0, 0, 0, 0);
            this.Gold = 0;
            this.all_spells = new();
            this.player_equiped_spells = new();
            this.last_play_time = 0;
            List<DimensionData> temp = new();
            for (int i = 0; i < 4; i++)
            {
                temp.Add(new("00" + (i + 1), -99));
            }
            dimensionData = temp;
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
            this._attackBase = 50;
            this._hpBase = 2000;
            this._manaBase = 300;
            this._moveSpeed = 5;
            this._defenseBase = 100;
        }
    }

    [System.Serializable]
    public struct SpellData
    {
        public string Obj_Name;
        public string Name;
        public string Desc;
        public string spritePath;
        public SpellData(Spell spell)
        {
            this.Obj_Name = spell.name;
            this.Name = spell._name;
            this.Desc = spell._description;
            this.spritePath = DataPersistenceManager.Instance.dataHandler.SaveImageToFile(spell._icon.texture, "Spell Icon", spell._name + "_icon");
        }

    }
    [System.Serializable]
    public struct DimensionData
    {
        public string dimension_id;
        public float best_play_time;
        public DimensionData(string dimension_id, float best_play_time)
        {
            this.dimension_id = dimension_id;
            this.best_play_time = best_play_time;
        }
    }

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
