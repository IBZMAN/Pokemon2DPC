using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{ 
    [SerializeField]
    private Sprite startingSprite;

    [SerializeField]
    private Sprite northSprite;

    [SerializeField]
    private Sprite eastSprite;

    [SerializeField]
    private Sprite southSprite;

    [SerializeField]
    private Sprite westSprite;

    public Vector2 lastPos;

    private Door doorController;

    Animator myAnim;

    private bool IsPressingSprint
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button4);
        }
    }

    void Start ()
    {
        myAnim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = startingSprite;
    }

    void Update ()
    {
        GetInput();
        PlayerAnimations();
	}

    void LateUpdate()
    {
        lastPos = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            //myAnimator.SetFloat("velY", myRigidBody.velocity.y);
            ChangeSprite(northSprite);
            direction += Vector2.up;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //myAnimator.SetFloat("velX", myRigidBody.velocity.x);
            ChangeSprite(westSprite);
            direction += Vector2.left;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            //myAnimator.SetFloat("velY", myRigidBody.velocity.y);
            ChangeSprite(southSprite);
            direction += Vector2.down;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //myAnimator.SetFloat("velX", myRigidBody.velocity.x);
            ChangeSprite(eastSprite);
            direction += Vector2.right;
        }
    }

    private void ChangeSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 outsideHealthCentreDoor = new Vector2(-2.51f, -1.7f);
        Vector2 outsidePokeMartDoor = new Vector2(2.25f, -1.67f);
        Vector2 InsideHealthCentre = new Vector2(-143.32f, -2.6f);
        Vector2 InsidePokeMart = new Vector2(6.71f, 129.66f);

        if (collision.gameObject.name == "EnterHealthCentre")
        {
            transform.position = InsideHealthCentre;
        }

        if (collision.gameObject.name == "EnterPokeMart")
        {
            transform.position = InsidePokeMart;
        }

        if (collision.gameObject.name == "ExitHealthCentre")
        {
             transform.position = outsideHealthCentreDoor;
        }

        if (collision.gameObject.name == "ExitPokeMart")
        {
            transform.position = outsidePokeMartDoor;
        }
    }

    private void PlayerAnimations()
    {
        if (Input.GetKey(KeyCode.D))
        {
            myAnim.SetInteger("State", 2);
        }
        if (Input.GetKey(KeyCode.W))
        {
            myAnim.SetInteger("State", 4);
        }
        if (Input.GetKey(KeyCode.A))
        {
            myAnim.SetInteger("State", 3);
        }
        if (Input.GetKey(KeyCode.S))
        {
            myAnim.SetInteger("State", 1);
        }
    }
}
