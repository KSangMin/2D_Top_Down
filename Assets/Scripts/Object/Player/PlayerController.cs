using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private PlayerInput input;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    private Animator animator;

    private InputAction moveAction;

    private Vector2 moveInput;
    private float moveSpeed = 5f;

    private void Awake()
    {
        player = GetComponent<Player>();
        input = GetComponent<PlayerInput>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        moveAction = input.actions["Move"];

        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
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
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed) moveInput = context.ReadValue<Vector2>().normalized;
        if(context.canceled) moveInput = Vector2.zero;
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
    }
}
