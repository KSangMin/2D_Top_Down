using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private PlayerCondition condition;
    private PlayerInput input;

    [HideInInspector] public SpriteRenderer spriteRenderer;
    private Animator animator;

    private InputAction moveAction;

    private Vector2 moveInput;
    private float moveSpeed = 5f;

    [SerializeField] private GameObject ProjectilePrefab;

    [SerializeField] private float attackRange = 8;
    [SerializeField] private float attackCooldown = 1f;
    private float attackTimer = 0;

    private void Awake()
    {
        player = GetComponent<Player>();
        condition = GetComponent<PlayerCondition>();
        input = GetComponent<PlayerInput>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        moveAction = input.actions["Move"];

        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(player.isDead) return;

        if (moveInput != Vector2.zero)
        {
            transform.Translate(moveInput * moveSpeed * Time.fixedDeltaTime);
            animator.SetBool("isMoving", true);

            if(moveInput.x > 0) spriteRenderer.flipX = false;
            else if(moveInput.x < 0) spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("isMoving", false);
            Attack();
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed) moveInput = context.ReadValue<Vector2>().normalized;
        if(context.canceled) moveInput = Vector2.zero;
    }

    void Attack()
    {
        Transform target = FindEnemy();
        if (target == null) return;

        if (attackTimer < attackCooldown) return;
        attackTimer = 0;

        Projectile projectile = PoolManager.Instance.Get(ProjectilePrefab).GetComponent<Projectile>();
        projectile.transform.position = transform.position;
        projectile.Init(target, condition.GetAttack());
    }

    Transform FindEnemy()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Monster"));
        Transform target = null;
        float minDist = attackRange;
        foreach (var col in cols)
        {
            float dist = (col.transform.position - transform.position).magnitude;
            if (dist < minDist)
            {
                minDist = dist;
                target = col.transform;
            }
        }

        return target;
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
