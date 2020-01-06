using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    private new Rigidbody2D rigidbody2D;

    private float xMaxVelocity = 10;
    private float xAcceleration = 100;

    private float friction = 10;
    private float gravity = -8;

    // Use this for initialization
    void Start () {
        this.rigidbody2D = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        float xVelocity = this.rigidbody2D.velocity.x;
        float yVelocity = this.rigidbody2D.velocity.y;


        if (Mathf.Abs(xVelocity) < xMaxVelocity)
        {
            //move horizontal
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

        //apply gravity
        yVelocity = yVelocity + this.gravity * Time.fixedDeltaTime;

        this.rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
    }
}
