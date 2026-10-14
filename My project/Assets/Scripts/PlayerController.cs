using NUnit.Framework.Constraints;
using System;
using System.Collections;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;
    public int charge = 3;

    public float speed = 5.0f;
	public float playerGravity = 3.0f;
    public float sprintspeed = 10.0f;
    public float jumpHeight = 5.0f;
    public float jumpDetectDistance = -1.0f;
    public float attackTime = 5.0f;
    public float attackCooldownTime = 1.0f;
    public float fusionDmgInterval = 0.5f;
    public float stamina = 50f;
    public float maxStamina = 50f;
    public float staminaRegen = 5.0f;
    public float sprint = 2.0f;
    public float sprintCooldown = 5.0f;
    public float sprintCost = 20f;
    public float dashStrength = 20.0f;
    public float dashTime = 3.0f;
    public float dashCost = 33.3f;
    public float dashCooldownTime = 2.0f;
    public float rotationSpeed = 0.1f;

    public bool isAttacking = false;
    public bool canAttack = false;
    public bool fusionDmg = false;
    public bool canSprint = true;
    public bool sprinting = false;
    public bool sprintLock = false;
    public bool toggleSprint = false;
    public bool isDashing = false;
    public bool canDash = true;
    public bool regenStamina = false;
	public bool canDash = true;
	public bool isDashing = false;
    public bool onGround = true;
    public bool gravitySwitch = false;
    public bool doubleJump = true;

    Ray2D jumpRay;
    Ray2D dashRay;
    Vector2 moveinput = Vector2.zero;

    GameObject currentWeaponObj;
    GameObject spawnPoint;
    Transform weaponSlot;
    PlayerInput input;
    Rigidbody2D rb;
    BasicEnemyBehavior enemy;
  

    public Vector2 moveInput = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        jumpRay = new Ray2D();
        dashRay = new Ray2D();
        weaponSlot = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            
        }

        jumpRay.origin = transform.position;
        if (!gravitySwitch)
            jumpRay.direction = -transform.up;
        else
            jumpRay.direction = transform.up;

        Vector2 tempMove = rb.linearVelocity;
            tempMove.x = moveInput.x * speed;

        if (sprinting)
        {
            if (stamina > 0)
            {
                tempMove.x *= sprint;

                stamina -= sprintCost * Time.deltaTime;

                StopCoroutine("sprintReset");
                regenStamina = false;

                if (stamina <=0)
                {
                    canSprint = false;
                    sprinting = false;
                    stamina = 0;
                }
            }
            if (moveInput.x < .75f && moveInput.x > -.75f)
            {
                canSprint = false;
                sprinting = false;
            }
        }

        if (!sprinting)
        {
          if  (!canSprint && !sprintLock)
            {
                StartCoroutine("sprintReset");
            }
          if (!canSprint && !regenStamina)
            {
                regenStamina = true;
            }
          if (regenStamina)
            {
                stamina += staminaRegen * Time.deltaTime;

                if (stamina >= maxStamina)
                {
                    stamina = maxStamina;
                    regenStamina = false;
                }
            }
        }
        rb.linearVelocityX = tempMove.x;

        if (gravitySwitch)
        {
            
            transform.rotation = new Quaternion(0, 0, 180f, 0);
            rb.gravityScale = -playerGravity;
        }
        else
        {

            transform.rotation = new Quaternion(0, 0, 0, 0);
            rb.gravityScale = playerGravity;
        }

    }
    
    
    public void Move(InputAction.CallbackContext context)
    {
        moveInput.x = context.ReadValue<Vector2>().x;
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        if (canSprint && (moveInput.x >= .75f || moveInput.x <= -.75f) && onGround)
        {
            if (!toggleSprint)
            {
                if (context.ReadValueAsButton())
                    sprinting = true;
                else
                    sprinting = false;

                if (!sprinting)
                    canSprint = false;
            }
            else
            {
                if (context.performed)
                    sprinting = !sprinting;
            }
        }
    }
    public void Jump()
    { 
        if (Physics2D.Raycast(jumpRay.origin, jumpRay.direction, jumpDetectDistance) && !gravitySwitch && onGround)
            rb.AddForceY(jumpHeight, ForceMode2D.Impulse);

        if (Physics2D.Raycast(jumpRay.origin, jumpRay.direction, jumpDetectDistance) && gravitySwitch && onGround)
            rb.AddForceY(-jumpHeight, ForceMode2D.Impulse);

    }
    public void Dash()
    {
        if (canDash)
		{
			if (rb.linearVelocityX > 0)
			{
				isDashing = true;
				canDash = false;
				float originalGravity = rb.gravityScale;
				rb.gravityScale = 0;
				rb.linearVelocityY = 0;
                rb.linearVelocityX = dashStrength * rb.linearVelocityX;
				
				new WaitForSeconds(dashTime);
				
				isDashing = false;
                rb.gravityScale = originalGravity;
				StartCoroutine("dashCooldown");
			}
		}
    }
    public void Attack()
    {
        if (currentWeaponObj != null && canAttack)
        {
            isAttacking = true;
            currentWeaponObj.transform.GetChild(0).gameObject.SetActive(true);
            canAttack = false;
            StartCoroutine("attackDuration");

        }
    }

    public void Drop()
    {

        {

        }
    }

    IEnumerator attackDuration()
    {

        yield return new WaitForSeconds(attackTime);

        isAttacking = false;
        currentWeaponObj.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine("attackCooldown");

    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime);

        canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            collision.gameObject.transform.SetPositionAndRotation(weaponSlot.position,
new Quaternion(0, 0, -90f, 90));

            collision.gameObject.transform.SetParent(weaponSlot);

            collision.rigidbody.bodyType = RigidbodyType2D.Kinematic;
            collision.rigidbody.simulated = false;

            collision.collider.enabled = false;

            currentWeaponObj = collision.gameObject;
            canAttack = true;
        }

        if (collision.gameObject.tag == "Hazard")
        {
            health--;
        }

        if (collision.gameObject.tag == "FusionHazard")
        {
            health--;
        }

        if (collision.gameObject.tag == "HealthItem")
        {
            health++;
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "reverseTerrain")
        {
            gravitySwitch = true;
			
        }

        if (collision.gameObject.name == "Terrain")
        {
            gravitySwitch = false;
			
        }
        if (collision.gameObject)
        {
            onGround = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FusionHazard")
        {
            if(!fusionDmg)
            {

                StartCoroutine("fusionDmgCooldown");
            }
        }
        if (collision.gameObject)
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FusionHazard")
        {
            if(fusionDmg)
            {
                StopCoroutine("fusionDmgCooldown");
                fusionDmg = false;
            }
        }
        if (collision.gameObject)
        {
            onGround = false;
        }
    }
    IEnumerator fusionDmgCooldown()
    {
        fusionDmg = true;

        yield return new WaitForSeconds(fusionDmgInterval);

        health--;
        fusionDmg = false;
    }
    IEnumerator sprintReset()
    {
        sprintLock = true;
        regenStamina = false;

        yield return new WaitForSeconds(sprintCooldown);

        canSprint = true;
        regenStamina = true;
        sprintLock = false;
    }
    IEnumerator dashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownTime);
        canDash = true;
    }
}
