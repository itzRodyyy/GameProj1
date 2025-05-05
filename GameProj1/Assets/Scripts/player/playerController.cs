using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("*** Components ***")]
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] CharacterController controller;
    [Header("*** Stats ***")]
    public float HP;
    public float MaxHP;
    public float SP;
    public float MaxSP;

    [Header("*** Movement ***")]
    [SerializeField] int playerSpeed;
    [SerializeField] int playerSprintMod;
    [SerializeField] int playerJumpPower;
    [SerializeField] int playerJumpMax;
    [SerializeField] int gravity;

    int playerJumpCount;

    Vector3 playerMoveDir;
    Vector3 playerVelocity;

    bool isSprinting;
    bool staminaIsUpdating;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaxHP = HP;
        MaxSP = SP;
        gamemanager.instance.updatePlayerUI();
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
        HandleStamina();
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
        if(Input.GetButtonDown("Sprint") && SP > 1)
        {
            isSprinting = true;
            playerSpeed *= playerSprintMod;
        }
        if (Input.GetButtonUp("Sprint") && isSprinting)
        {
            StopSprinting();
        }

    }
    void StopSprinting()
    {
        isSprinting = false;
        playerSpeed /= playerSprintMod;
    }

    void HandleStamina()
    {
        if (isSprinting && SP > 0)
        {
            SP -= 1 * Time.deltaTime;
            if (SP < 0)
                SP = 0;
            gamemanager.instance.updatePlayerUI();
        }
        else if (!isSprinting && SP < MaxSP)
        {
            SP += 1 * Time.deltaTime;
            if (SP > MaxSP)
                SP = MaxSP;
            gamemanager.instance.updatePlayerUI();
        }
        else if (isSprinting && SP <= 0)
        {
            StopSprinting();
        }
    }


}
