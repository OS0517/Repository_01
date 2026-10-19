using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using UnityEngine.UI;

public class ProjectileBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController player;

    public Vector2 projectilePosition;
    public Vector2 playerPosition;

    public float projectileSpeed = 1.0f;
    public float projectileDistance = 25.0f;
    public float projectileAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        projectilePosition = Vector2.zero;
        playerPosition = Vector2.zero;
        projectileAngle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Getting the angle between the player and projectiles position
        projectilePosition = new Vector2(rb.transform.position.x, rb.transform.position.y);
        playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
        projectileAngle = Vector2.Angle(projectilePosition, playerPosition);
        
        //currently working out the speed of the projectile
        rb.linearVelocityX = projectileSpeed * (Mathf.Sin(projectileAngle * Mathf.Rad2Deg));
        rb.linearVelocityY = projectileSpeed * (Mathf.Cos(projectileAngle * Mathf.Rad2Deg));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.tag == "Player")
        {
            player.health--;
            Destroy(rb.gameObject);
        }
    }
}
