using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerController player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player").
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
