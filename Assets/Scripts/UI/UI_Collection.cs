using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Collection : UI
{
    [SerializeField] private GameObject slotPrefab;

    [SerializeField] private Button openButton;
    
    [SerializeField] private RectTransform infoPanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI maxHPText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private TextMeshProUGUI attackRangeText;
    [SerializeField] private TextMeshProUGUI moveSpeedText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI itemText;

    [SerializeField] private RectTransform collectionPanel;
    [SerializeField] private Transform content;

    private string curSelectedId;

    protected override void Awake()
    {
        base.Awake();

        openButton.onClick.AddListener(() =>
        {
            if (collectionPanel.gameObject.activeSelf)
            {
                Time.timeScale = 1f;
                collectionPanel.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                infoPanel.gameObject.SetActive(false);
                collectionPanel.gameObject.SetActive(true);
            }
        });

        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (MonsterData monster in DataManager.Instance.monsterDict.Values)
        {
            GameObject slotGO = Instantiate(slotPrefab, content);
            var slot = slotGO.GetComponent<CollectionSlot>();
            slot.SetData(monster.MonsterID);
            slot.slotButton.onClick.AddListener(() => ShowInfo(monster.MonsterID));
        }

        infoPanel.gameObject.SetActive(false);
        collectionPanel.gameObject.SetActive(false);
    }

    void ShowInfo(string id)
    {
        if (infoPanel.gameObject.activeSelf && curSelectedId == id)
        {
            infoPanel.gameObject.SetActive(false);
            return;
        }

        curSelectedId = id;
        MonsterData monster = DataManager.Instance.monsterDict[curSelectedId];

        nameText.text = monster.Name;
        descText.text = monster.Description;
        maxHPText.text = monster.MaxHP.ToString();
        attackText.text = monster.Attack.ToString();
        attackSpeedText.text = monster.AttackSpeed.ToString("F2");
        attackRangeText.text = monster.AttackRange.ToString();
        moveSpeedText.text = monster.MoveSpeed.ToString("F2");
        expText.text = $"{monster.MinExp} ~ {monster.MaxExp}";
        itemText.text = string.Join(", ", Array.ConvertAll(monster.DropItem, itemId => DataManager.Instance.itemDict[itemId].Name));

        infoPanel.gameObject.SetActive(true);
    }
}
