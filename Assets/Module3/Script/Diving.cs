using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diving : MonoBehaviour
{
    public float moveSpeed;
    private float speedMod = 0;

    private Rigidbody2D myRigidBody;

    public GameObject bubbles;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        controllerManager();
    }

    void controllerManager()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            movePlayer(moveSpeed + speedMod, myRigidBody.velocity.y);
        }
        else if (horizontalInput < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            movePlayer(-(moveSpeed + speedMod), myRigidBody.velocity.y);
        }
        else if (verticalInput > 0f)
        {
            movePlayer(myRigidBody.velocity.x, moveSpeed);
        }
        else if (verticalInput < 0f)
        {
            movePlayer(myRigidBody.velocity.x, -moveSpeed);
        }
        else
        {
            myRigidBody.velocity = new Vector2(0f, 0f);
        }
    }

    void movePlayer(float xVelocity, float yVelocity)
    {
        myRigidBody.velocity = new Vector2(xVelocity, yVelocity);
    }
}
