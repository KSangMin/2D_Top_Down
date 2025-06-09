using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public Dictionary<int, ItemData> itemDict = new();
    public Dictionary<string, MonsterData> monsterDict = new();

    protected override void Awake()
    {
        base.Awake();

        itemDict = LoadJson<ItemDataLoader, int, ItemData>("Item").MakeDict();
        monsterDict = LoadJson<MonsterDataLoader, string, MonsterData>("Monster").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string fileName = default) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>(string.IsNullOrEmpty(fileName) ? $"Data/{typeof(Value)}" : $"Data/{fileName}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
