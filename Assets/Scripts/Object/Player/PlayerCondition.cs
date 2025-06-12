using DG.Tweening;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    private Player player;
    private PlayerController controller;

    private Stat HP;
    private Stat Attack;

    [SerializeField] private FloatEventChannel OnHPChanged;
    [SerializeField] private FloatEventChannel OnAttackChanged;

    private void Awake()
    {
        player = GetComponent<Player>();
        controller = GetComponent<PlayerController>();

        HP = new Stat(100, 100, 0);
        Attack = new Stat(20, 999, 0);

        HP.OnStatPercentageChanged += (percentage) => OnHPChanged.RaiseEvent(percentage);
        Attack.OnStatChanged += () => OnAttackChanged.RaiseEvent(Attack.CurValue);
    }

    private void Start()
    {
        OnHPChanged.RaiseEvent(HP.GetPercentage());
        OnAttackChanged.RaiseEvent(Attack.CurValue);
    }

    public void TakeDamage(float damage)
    {
        if(player.isDead) return;

        HP.Subtract(damage);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(controller.spriteRenderer.DOColor(Color.red, 0.1f));
        sequence.Append(controller.spriteRenderer.DOColor(Color.white, 0.1f));
        sequence.Play();

        if (HP.CurValue <= HP.minValue) Dead();

        Debug.Log(damage);
    }

    public void Dead()
    {
        player.isDead = true;
        controller.Dead();
    }
}
