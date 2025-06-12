using System.Linq;
using UnityEngine;

public class Item : Poolable
{
    [SerializeField] private int id = 0;

    public void Init(int itemId)
    {
        id = itemId;
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
