using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{

    public float gForce = 0.01f;
    public float time = 3.0f;

    public bool rising = false;

    public Rigidbody2D rb;
    public SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("gravityPlateRising");
        rising = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rising)
        {
            if (rising == true)
            {
                rb.linearVelocityY = -gForce;
            }
            if (rising == false)
            {
                rb.linearVelocityY = gForce;
            }
        }
    }
}
