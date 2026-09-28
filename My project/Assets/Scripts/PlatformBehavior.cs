using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{

    public float time = 10.0f;

    public bool rising = false;

    public Rigidbody2D rb;
    public SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("gravityPlateRising");
        rising = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rising)
        {
            if (rising == true)
            {
                rb.gravityScale = -0.02f;
            }
        }
        else
            rb.gravityScale = 0.02f;
    }

    IEnumerator gravityPlateRising()
    {
        rising = true;
        yield return new WaitForSeconds(time);
        StartCoroutine("gravityPlateFalling");
    }
    IEnumerator gravityPlateFalling()
    {
        rising = false;
        yield return new WaitForSeconds(time);
        StartCoroutine("gravityPlateRising");
    }
}
