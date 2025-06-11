using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionSlot : MonoBehaviour
{
    [HideInInspector] public Button slotButton;

    [SerializeField] private TextMeshProUGUI nameText;

    private void Awake()
    {
        slotButton = GetComponent<Button>();
    }

    public void SetData(string monsterId)
    {
        nameText.text = DataManager.Instance.monsterDict[monsterId].Name;
    }
}
