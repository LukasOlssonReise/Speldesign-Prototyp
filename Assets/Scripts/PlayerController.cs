using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float speed = 12f;
    CharacterController characterController;

    float gravity = -9.82f;
    float jumpForce = 5f;
    float yVelocity;

    InputActionMap player;
    InputAction moveButtons, jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = InputSystem.actions.FindActionMap("Player");
        moveButtons = player.FindAction("Move");
        jump = player.FindAction("Jump");

        player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterController)
        {
            characterController = gameObject.GetComponent<CharacterController>();
        }

        Vector2 move = moveButtons.ReadValue<Vector2>();

        Vector3 movement = new Vector3(move.x, 0, move.y);
        if (movement.magnitude > 1f)
        {
            movement = movement.normalized;
        }

        movement = transform.TransformDirection(movement);

        movement *= speed;

        if (characterController.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;

            if (jump.WasPressedThisFrame())
            {
                yVelocity = jumpForce;
            }
        }

        yVelocity += gravity * Time.deltaTime;
        movement.y = yVelocity;

        characterController.Move(movement * Time.deltaTime);
    }
}
