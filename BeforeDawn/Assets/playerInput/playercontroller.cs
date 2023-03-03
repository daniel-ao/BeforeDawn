using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroller : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator animator;
    private CharacterController cc;
    private bool isMovementPressed;
    private Vector2 currentMovementInput;

    private Vector3 currentMovement;

    public float roatation = 1.0f;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        playerInput.CharacterControls.Move.started += onMovement;
        playerInput.CharacterControls.Move.canceled += onMovement;
        playerInput.CharacterControls.Move.performed += onMovement;
    }

    void onMovement(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleRotation()
    {
        Vector3 position;

        position.x = currentMovement.x;
        position.y = 0.0f;
        position.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;
        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(position);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, roatation);

        }
    }
    void animation()
    {
        bool isRunning = animator.GetBool("isRunning");
        if (isMovementPressed)
        {
            animator.SetBool("isRunning", true);
        }
        else if (!isMovementPressed)
        {
            animator.SetBool("isRunning", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();
        animation();
        cc.Move(currentMovement * Time.deltaTime);
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void Ondisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
