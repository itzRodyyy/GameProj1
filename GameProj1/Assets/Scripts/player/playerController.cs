using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour, iPickup
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

    [Header("*** Shoot ***")]
    [SerializeField] GameObject weaponModel;
    [SerializeField] weaponStats weapon;

    int playerJumpCount;

    Vector3 playerMoveDir;
    Vector3 playerVelocity;

    bool isSprinting;
    bool staminaIsUpdating;

    float attackTimer;
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
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * weapon.range, Color.red);
        Movement();
        attackTimer += Time.deltaTime;
        Shoot();
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

    void Shoot()
    {
        if (weapon.isMelee)
        {
            if (Input.GetButtonDown("Fire1") && attackTimer >= weapon.attackRate)
            {
                attackTimer = 0;
                CheckCollision();
            }
        }
        else
        {
            if (weapon.isAutomatic)
            {
                if (Input.GetButton("Fire1") && attackTimer >= weapon.attackRate && weapon.currentAmmo > 0)
                {
                    attackTimer = 0;
                    CheckCollision();
                    weapon.currentAmmo--;
                }
            } 
            else
            {
                if (Input.GetButtonDown("Fire1") && attackTimer >= weapon.attackRate && weapon.currentAmmo > 0)
                {
                    attackTimer = 0;
                    CheckCollision();
                    weapon.currentAmmo--;
                }
            }
        }
    }

    void CheckCollision()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, weapon.range, ~ignoreLayer))
        {
            Debug.Log(hit.collider.name);
            iDamage dmg = hit.collider.GetComponent<iDamage>();
            if (dmg != null)
            {
                dmg.takeDmg(weapon.damage);
            }
        }
    }

    public void GetWeapon(weaponStats _weapon)
    {
        weapon = _weapon;
        weaponModel.transform.localScale = weapon.model.transform.lossyScale;
        weaponModel.GetComponent<MeshFilter>().sharedMesh = weapon.model.GetComponent<MeshFilter>().sharedMesh;
        weaponModel.GetComponent<MeshRenderer>().sharedMaterial = weapon.model.GetComponent<MeshRenderer>().sharedMaterial;
    }
}
