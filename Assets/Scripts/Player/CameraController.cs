using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] float sensitivity = 0.1f;
    [SerializeField] Transform playerBody;

    InputAction lookAction;
    float xRotation;

    void Awake()
    {
        lookAction = InputSystem.actions.FindAction("Player/Look");
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 look = lookAction.ReadValue<Vector2>() * sensitivity;

        xRotation = Mathf.Clamp(xRotation - look.y, -80f, 80f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * look.x);
    }
}
