using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
    void Dead();
}

public class Monster : Poolable, IDamageable
{
    [SerializeField] private string id;
    private MonsterData data;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    [SerializeField] private SpriteRenderer hpBar;

    private Stat HP;
    private float attackTimer;

    private float hpScaleX;
    private float hpScaleY;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();

        if(DataManager.Instance.monsterDict.Count == 0) DataManager.Instance.LoadAll();
        data = DataManager.Instance.monsterDict[id];

        HP = new Stat(
            data.MaxHP,
            data.MaxHP,
            0
        );

        hpScaleX = hpBar.transform.localScale.x;
        hpScaleY = hpBar.transform.localScale.y;

        HP.OnStatPercentageChanged += (percentage) =>
        {
            hpBar.transform.localScale = new Vector3(percentage * hpScaleX, hpScaleY, 1f);
        };
    }

    private void OnEnable()
    {
        HP.Reset();
        _animator.SetBool("isDead", false);
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(GameManager.Instance.player == null) return;

        Vector2 dir = GameManager.Instance.player.transform.position - transform.position;
        if (dir.magnitude <= data.AttackRange) Attack();
        else
        {
            Vector2 dirNorm = dir.normalized;
            if (dirNorm.x > 0) _spriteRenderer.flipX = false;
            else _spriteRenderer.flipX = true;

            transform.Translate(dirNorm * data.MoveSpeed * Time.fixedDeltaTime);
        }
    }

    public void Attack()
    {
        if(attackTimer >= data.AttackSpeed)
        {
            attackTimer = 0;

            GameManager.Instance.player.condition.TakeDamage(data.Attack);
        }
    }

    public void TakeDamage(float damage)
    {
        HP.Subtract(damage);
        if (HP.CurValue <= 0) Dead();

        _animator.SetTrigger("Hit");
    }

    public void Dead()
    {
        _animator.SetBool("isDead", true);

        PoolManager.Instance.Release(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, data.AttackRange);
    }
}
