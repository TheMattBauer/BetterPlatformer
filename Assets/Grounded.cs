using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Grounded : MonoBehaviour {

    public MeatJump player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            player.IsGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            player.IsGrounded = false;
        }
    }
}
