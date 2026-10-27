using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] float sprintMultiplier = 1.6f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] float jumpHeight = 1.2f;

    CharacterController controller;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction sprintAction;

    float verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        var player = InputSystem.actions.FindActionMap("Player");

        moveAction = player.FindAction("Move");
        jumpAction = player.FindAction("Jump");
        sprintAction = player.FindAction("Sprint");
    }

    private void Update()
    {
        bool grounded = controller.isGrounded;

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 horizontal = transform.right * input.x + transform.forward * input.y;

        float speed = moveSpeed;
        if (sprintAction != null && sprintAction.IsPressed())
            speed *= sprintMultiplier;

        horizontal *= speed;

        if (grounded && verticalVelocity < 0f)
            verticalVelocity = -2f;

        if (grounded && jumpAction.WasPressedThisFrame())
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = horizontal + Vector3.up * verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
}