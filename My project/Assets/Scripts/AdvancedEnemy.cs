using UnityEngine;

public class AdvancedEnemy : BasicEnemyBehavior
{

    public Rigidbody2D rb;
    public PlayerController player;
	public GameObject enemyProjectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        canAttack = true;
        isAttacking = false;

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
