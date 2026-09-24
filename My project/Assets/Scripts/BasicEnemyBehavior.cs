using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UnifiedRayTracing;

public class BasicEnemyBehavior : MonoBehaviour
{

    public PlayerController player;
    public Rigidbody2D rb;

    public bool isFollowing = false;

    public float speed = 4.0f;
    public float health = 5.0f;
    public float maxHealth = 3.0f;

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
            if (player.transform.position.x > transform.position.x)
            {
                rb.linearVelocityX = speed;
            }

            if (player.transform.position.x < transform.position.x)
            {
                rb.linearVelocityX = -speed;
            }

            if (player.transform.position.y > transform.position.y)
            {
                rb.linearVelocityY = speed;
            }

            if (player.transform.position.y < transform.position.y)
            {
                rb.linearVelocityY = -speed;
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
}
