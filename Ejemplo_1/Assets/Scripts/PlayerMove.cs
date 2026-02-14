using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public  NIS inputActions;

    private Vector2 moveInput;

    public float speed = 5f;

    private void Awake()
    {
        inputActions = new NIS();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.PlayerMove.Caminar.performed += OnMove;
        inputActions.PlayerMove.Caminar.canceled += OnMove;

        inputActions.PlayerMove.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        inputActions.PlayerMove.Caminar.performed -= OnMove;
        inputActions.PlayerMove.Caminar.canceled -= OnMove;

        inputActions.PlayerMove.Jump.performed -= OnJump;
        inputActions.Disable();
    }
    //NEW IMPUT SYSTEM

    private void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        moveInput.y = 1;
        Debug.Log("NEW IMPUT - Saltar");
    }

    private void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(movement * speed * Time.deltaTime);

        Vector3 Jump = new Vector3(0, moveInput.y, 0);
        transform.Translate(Jump * speed * Time.deltaTime);

    }




}

