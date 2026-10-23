using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{

    public float time = 5.0f;
    public float riseOrFallSpeed = 0.5f;

    public bool rising = true;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Rigidbody2D gravityPlate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityPlate = GameObject.FindGameObjectWithTag("gravityPlate").GetComponent<Rigidbody2D>();
        StartCoroutine("gravityPlateRising");
        rising = true;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityY = riseOrFallSpeed;
        if (rising)
        {
            if (rising == true)
            {
                riseOrFallSpeed = -riseOrFallSpeed;
            }
        }
        else
            riseOrFallSpeed = +riseOrFallSpeed;
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
