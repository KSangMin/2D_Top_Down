using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    private PlayerInput _input;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private InputAction moveAction;

    private Vector2 moveInput;
    private float moveSpeed = 5f;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();

        moveAction = _input.actions["Move"];

        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        GameManager.Instance.player = this;
    }

    private void FixedUpdate()
    {
        if(moveInput != Vector2.zero)
        {
            transform.Translate(moveInput * moveSpeed * Time.fixedDeltaTime);
            _animator.SetBool("isMoving", true);

            if(moveInput.x > 0) _spriteRenderer.flipX = false;
            else if(moveInput.x < 0) _spriteRenderer.flipX = true;
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed) moveInput = context.ReadValue<Vector2>().normalized;
        if(context.canceled) moveInput = Vector2.zero;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
    }

    public void Dead()
    {
        
    }
}
