using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerController player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb.linearVelocityX = 1;
        rb.linearVelocityY = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.tag == "Player")
        {
            player.health--;
            Destroy(gameObject);
        }
    }
}
