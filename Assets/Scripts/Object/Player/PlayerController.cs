using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;

    private InputAction moveAction;

    private Vector2 moveInput;
    private float moveSpeed = 5f;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();

        moveAction = _input.actions["Move"];

        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
    }

    private void FixedUpdate()
    {
        if(moveInput != Vector2.zero)
        {
            transform.Translate(moveInput * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed) moveInput = context.ReadValue<Vector2>().normalized;
        if(context.canceled) moveInput = Vector2.zero;
    }
}
