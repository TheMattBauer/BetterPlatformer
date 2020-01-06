using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MeatJump : MonoBehaviour {

    private new Rigidbody2D rigidbody2D;
    private float xMaxVelocity = 10;
    private float xAcceleration = 100;

    private float friction = 10;
    private float gravity = 40;
    private float jumpVelocity = 20;

    public bool IsGrounded { get; set; }
    private bool isJumping = false;
    private bool lastIsJumping = false;

    //velocity clamp jump
    private float terminationVelocity;

    // Use this for initialization
    void Start()
    {
        this.rigidbody2D = this.GetComponent<Rigidbody2D>();

        float jumpHeight = Mathf.Pow(this.jumpVelocity, 2) / (2f * this.gravity);
        float timeToApex = Mathf.Sqrt((2f * jumpHeight) / this.gravity);
        float minimumJumpHeight = jumpHeight * .2f; // limits jump to 20% of max

        this.terminationVelocity = Mathf.Sqrt(Mathf.Pow(this.jumpVelocity, 2) + 2f * -this.gravity * (jumpHeight - minimumJumpHeight));
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsGrounded && Input.GetKeyDown("space"))
        {
            this.isJumping = true;
        }
        else if (Input.GetKeyUp("space"))
        {
            this.isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        float xVelocity = this.rigidbody2D.velocity.x;
        float yVelocity = this.rigidbody2D.velocity.y;

        //apply jump velocity once
        if (this.isJumping && !this.lastIsJumping)
        {
            yVelocity = jumpVelocity;
        }
        else if (!this.isJumping && this.lastIsJumping)
        {
            //if (yVelocity > 0)
            //{
            //    yVelocity = 0;
            //}

            if (yVelocity > this.terminationVelocity)
            {
                yVelocity = this.terminationVelocity;
            }
        }

        //apply gravity
        yVelocity = yVelocity + -this.gravity * Time.fixedDeltaTime;

        //move horizontal
        if (Mathf.Abs(xVelocity) < xMaxVelocity)
        {
            if (Input.GetKey("a"))
            {
                xVelocity = xVelocity + -xAcceleration * Time.fixedDeltaTime;
            }
            else if (Input.GetKey("d"))
            {
                xVelocity = xVelocity + xAcceleration * Time.fixedDeltaTime;
            }
        }

        //apply friction (dampening)
        if (Mathf.Abs(xVelocity) > 0)
        {
            float sign = Mathf.Sign(xVelocity);
            xVelocity = xVelocity / (1 + friction * Time.fixedDeltaTime);

            if (sign != Mathf.Sign(xVelocity))
            {
                xVelocity = 0;
            }
        }

        this.rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
        this.lastIsJumping = this.isJumping;
    }
}
