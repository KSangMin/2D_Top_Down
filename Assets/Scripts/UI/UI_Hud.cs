using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI
{
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI attackText;

    [SerializeField] private FloatEventChannel OnHpChanged;
    [SerializeField] private FloatEventChannel OnAttackChanged;

    protected override void Awake()
    {
        base.Awake();

        OnHpChanged.RegisterListener(SetHPBar);
        OnAttackChanged.RegisterListener(SetAttackText);
    }
     
    void SetHPBar(float percentage)
    {
        hpBar.fillAmount = percentage;
    }

    void SetAttackText(float attack)
    {
        attackText.text = $"°ø°Ý·Â: {((int)attack)}";
    }

    private void OnDestroy()
    {
        OnHpChanged.UnregisterListener(SetHPBar);
        OnAttackChanged.UnregisterListener(SetAttackText);
    }
}
