using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("*** Components ***")]
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] CharacterController controller;

    [Header("*** Movement ***")]
    [SerializeField] int playerSpeed;
    [SerializeField] int playerSprintMod;
    [SerializeField] int playerJumpPower;
    [SerializeField] int playerJumpMax;
    [SerializeField] int gravity;

    int playerJumpCount;

    Vector3 playerMoveDir;
    Vector3 playerVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    void Movement()
    {
        if (controller.isGrounded)
        {
            playerVelocity = Vector3.zero;
            playerJumpCount = 0;
        }

        playerMoveDir = (Input.GetAxis("Vertical") * transform.forward) + (Input.GetAxis("Horizontal") * transform.right);

        Jump();
        playerVelocity.y -= gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);

        Sprint();
        controller.Move(playerMoveDir * playerSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && playerJumpCount < playerJumpMax)
        {
            playerVelocity.y = playerJumpPower;
        }
    }

    void Sprint()
    {
        if(Input.GetButtonDown("Sprint"))
        {
            playerSpeed *= playerSprintMod;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            playerSpeed /= playerSprintMod;
        }
    }
}
