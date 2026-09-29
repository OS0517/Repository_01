using NUnit.Framework.Constraints;
using System.Collections;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerController : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;
    public int charge = 3;

    public float speed = 5.0f;
    public float sprintspeed = 10.0f;
    public float jumpHeight = 3.0f;
    public float jumpDetectDistance = 1.0f;
    public float attackTime = 5.0f;
    public float attackCooldownTime = 1.0f;
    public float fusionDmgInterval = 0.5f;

    public bool isAttacking = false;
    public bool canAttack = false;
    public bool fusionDmg = false;

    Ray2D jumpRay;
    Vector2 moveinput = Vector2.zero;

    GameObject currentWeaponObj;
    Transform weaponSlot;
    Transform spawnPoint;
    PlayerInput input;
    Rigidbody2D rb;

    public Vector2 moveInput = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        jumpRay = new Ray2D();
        weaponSlot = transform.GetChild(0);
        spawnPoint = transform.GetChild(1);

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            
        }

        jumpRay.origin = transform.position;
        jumpRay.direction = transform.up;

        rb.linearVelocityX = moveInput.x * speed;
    }
    public void Move(InputAction.CallbackContext context)
    {
        moveInput.x = context.ReadValue<Vector2>().x;
    }
    public void Sprint(InputAction.CallbackContext context)
    {
        moveInput.x = context.ReadValue<Vector2>().x;
    }
    public void Jump()
    {
        if (Physics2D.Raycast(jumpRay.origin, jumpRay.direction, jumpDetectDistance))
            rb.AddForceY(jumpHeight, ForceMode2D.Impulse);
    }
    public void Dash()
    {
        
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
    }
    IEnumerator fusionDmgCooldown()
    {
        fusionDmg = true;

        yield return new WaitForSeconds(fusionDmgInterval);

        health--;
        fusionDmg = false;
    }

}
