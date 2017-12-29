using UnityEngine;

public class PlayerMovement : Player
{
    [SerializeField]
    private float speed;

    private bool IsPressingSprint
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button4);
        }
    }

    void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (IsPressingSprint)
        {
            myRigidBody.velocity = direction.normalized * (speed + 3f);
        }
        else
        {
            myRigidBody.velocity = direction.normalized * speed;
        }
    }
}
