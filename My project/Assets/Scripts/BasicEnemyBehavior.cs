using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UnifiedRayTracing;

public class BasicEnemyBehavior : MonoBehaviour
{

    public PlayerController player;
    public Rigidbody2D rb;

    public bool isFollowing = false;
    public bool canAttack = true;
    public bool isAttacking = false;

    public float speed = 4.0f;
    public float health = 5.0f;
    public float maxHealth = 5.0f;
    public float attackDurationTime = 1.0f;
    public float attackCooldownTime = 1.5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isFollowing)
        {
            if (player.transform.position.x > transform.position.x && !player.gravitySwitch)
            {
                rb.linearVelocityX = speed;
            }

            if (player.transform.position.x < transform.position.x && !player.gravitySwitch)
            {
                rb.linearVelocityX = -speed;
            }
        }

        else
            rb.linearVelocityX = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isFollowing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isFollowing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Player")
        {
            rb.linearVelocityX = 0;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (canAttack)
        {
            if (collision.gameObject.tag == "Player")
            {
            isAttacking = true;
            rb.linearVelocityX = 0;
            player.health--;
            canAttack = false;
            StartCoroutine("attackDuration");
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
       if (collision.gameObject.tag == "Player")
        {
            isFollowing = true;
        }
    }
    IEnumerator attackDuration()
    {
        yield return new WaitForSeconds(attackDurationTime);

        isAttacking = false;
        StartCoroutine("attackCooldown");
    }
    IEnumerator attackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTime);

        canAttack = true;
    }
}
