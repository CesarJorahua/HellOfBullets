using System;
using HellOfBullets.Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Transform mainCameraTransform;
    private InputSystemActions playerInputActions;
    private Vector2 movement;

    private void Awake()
    {
        mainCameraTransform = Camera.main.transform;
        if(!mainCameraTransform)
            Debug.LogError("There is no main camera on the scene");
        playerInputActions = new ();
        playerInputActions.Player.Move.Enable();
        playerInputActions.Player.Move.performed += OnMovePerfomed;
        playerInputActions.Player.Move.canceled += OnMoveStoped;
    }

    private void OnMoveStoped(InputAction.CallbackContext context)
    {
        movement = Vector2.zero;
    }

    private void OnMovePerfomed(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 displacement = movement * (_movementSpeed * Time.deltaTime);
        transform.position += displacement;
        mainCameraTransform.position += displacement;
    }

}
