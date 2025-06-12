using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemResult : UI
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI attackText;

    [SerializeField] private Button closeButton;

    protected override void Awake()
    {
        base.Awake();

        closeButton.onClick.AddListener(()=>
        {
            Time.timeScale = 1f;
            Hide();
        });
    }

    public void Init(int id)
    {
        Time.timeScale = 0f;

        ItemData data = DataManager.Instance.itemDict[id];

        nameText.text = data.Name;
        hpText.text = $"{(int)data.MaxHP}";
        attackText.text = $"{(int)data.MaxAtk}";
    }
}
