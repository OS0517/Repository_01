using UnityEngine;

public class AdvancedEnemy : BasicEnemyBehavior
{

    Rigidbody2D rb;
    PlayerController player; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        canAttack = true;
        isAttacking = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (isFollowing)
        {
            if (player.rb.position.x < rb.position.x)
        }
                ]
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if collision
    }
}
