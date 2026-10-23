using JetBrains.Annotations;
using UnityEngine;

public class AttackZoneScript : MonoBehaviour
{
    public PlayerController player;
    public BasicEnemyBehavior enemy;

    public bool isAttacking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAttacking = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            enemy.isAttacking = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            enemy.isAttacking = true;
    }
}
