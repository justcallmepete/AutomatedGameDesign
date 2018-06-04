using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 direction;
    public float speed = 2f;
    public Camera camera;

	// Use this for initialization
	void Start ()
	{
	    camera = Camera.main;
	    rb = GetComponent<Rigidbody2D>();

        camera.transform.SetParent(this.transform);
	    camera.transform.position = new Vector3(0,0,0);
        camera.transform.position = new Vector3(transform.position.x,transform.position.y, -5);
	}
	
	// Update is called once per frame
	void Update () {
		InputHandler();
        Move();
	}

    void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void InputHandler()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
    }
}
