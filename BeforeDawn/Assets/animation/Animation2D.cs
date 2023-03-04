using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation2D : MonoBehaviour
{
    private Animator bot;
    public float acceleration;
    public float decceleration;

    private float X = 0.0f;

    private float Y = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        bot = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool foward = Input.GetKey("z");
        bool left = Input.GetKey("q");
        bool right  = Input.GetKey("d");
        if (foward && Y < 1.0f)
        {
            Y += Time.deltaTime * acceleration;
        }

        if (left && X > -0.5f)
        {
            X -= Time.deltaTime * acceleration;
        }

        if (right && X < 0.5f)
        {
            X += Time.deltaTime * acceleration;
        }

        if (!foward && Y > 0.0f)
        {
            Y -= Time.deltaTime * decceleration;
        }
        if (!left && X < 0.0f)
        {
            X += Time.deltaTime * decceleration;
        }

        if (!right && X > 0.0f)
        {
            X -= Time.deltaTime * decceleration;
        }
        bot.SetFloat("X", X);
        bot.SetFloat("Y", Y);
    }
}
