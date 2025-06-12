using System.Linq;
using UnityEngine;

public class Item : Poolable
{
    [SerializeField] private int id = 0;

    private void Awake()
    {
        PickRandomId();
    }

    private void OnEnable()
    {
        PickRandomId();
    }

    void PickRandomId()
    {
        int randIndex = Random.Range(0, DataManager.Instance.itemDict.Count);
        int itemID = DataManager.Instance.itemDict.Keys.ToList()[randIndex];
        id = itemID;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCondition>().AddItemStat(id);
            UIManager.Instance.ShowUI<UI_ItemResult>().Init(id);
            PoolManager.Instance.Release(this);
        }
    }
}
