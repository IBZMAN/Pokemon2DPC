using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    protected Vector2 direction;

    protected Rigidbody2D myRigidBody;

    [SerializeField]
    private float speed;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

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
            myRigidBody.velocity = FindObjectOfType<GameManager>().direction.normalized * (speed + 3f);
        }
        else
        {
            myRigidBody.velocity = FindObjectOfType<GameManager>().direction.normalized * speed;
        }
    }
}
