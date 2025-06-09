//캐릭터, 아이템 등의 초기값 로드 용도
using System;
using System.Collections.Generic;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

#region ItemData
[Serializable]
public class ItemData
{
    public int ItemID;
    public string Name;
    public string Description;
    public int UnlockLev;
    public int MaxHP;
    public float MaxHPMul;
    public int MaxMP;
    public float MaxMPMul;
    public int MaxAtk;
    public float MaxAtkMul;
    public int MaxDef;
    public float MaxDefMul;
    public int Status;
}

[Serializable]
public class ItemDataLoader : ILoader<int, ItemData>
{
    public List<ItemData> Item = new List<ItemData>();

    public Dictionary<int, ItemData> MakeDict()
    {
        Dictionary<int, ItemData> dict = new Dictionary<int, ItemData>();
        foreach (ItemData item in Item)
        {
            dict.Add(item.ItemID, item);
        }

        return dict;
    }
}
#endregion

#region MonsterData
[Serializable]
public class MonsterData
{
    public string MonsterID;
    public string Name;
    public string Description;
    public int Attack;
    public float AttackMul;
    public int MaxHP;
    public float MaxHPMul;
    public int AttackRange;
    public float AttackRangeMul;
    public float AttackSpeed;
    public float MoveSpeed;
    public int MinExp;
    public int MaxExp;
    public int[] DropItem;
}

[Serializable]
public class MonsterDataLoader : ILoader<string, MonsterData>
{
    public List<MonsterData> Monster = new List<MonsterData>();

    public Dictionary<string, MonsterData> MakeDict()
    {
        Dictionary<string, MonsterData> dict = new Dictionary<string, MonsterData>();
        foreach (MonsterData monster in Monster)
        {
            dict.Add(monster.MonsterID, monster);
        }

        return dict;
    }
}
#endregion