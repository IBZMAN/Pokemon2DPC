using UnityEngine;

public class Player : Character
{
    Animator myAnimator;

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

    private bool IsPressingSprint
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button4);
        }
    }

<<<<<<< HEAD
    public Animation walkLeftAnim;

=======
=======
    public Sprite startingSprite;
    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;
<<<<<<< HEAD
   


=======
    public Animation walkLeftAnim;
>>>>>>> 8b1dd9bec34fcdf2fd4c5b234f653a249b62716f
>>>>>>> f029b5c5fd01f357d2d89472110ce765c48a3a02
>>>>>>> master
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        gameObject.GetComponent<SpriteRenderer>().sprite = startingSprite;      
    }

    void Update ()
    {
        GetInput();     
<<<<<<< HEAD
=======
=======
        GetInput();
<<<<<<< HEAD

=======
        GetAnimations();
        
>>>>>>> 8b1dd9bec34fcdf2fd4c5b234f653a249b62716f
>>>>>>> f029b5c5fd01f357d2d89472110ce765c48a3a02
>>>>>>> master
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
           


>>>>>>> 8b1dd9bec34fcdf2fd4c5b234f653a249b62716f
>>>>>>> f029b5c5fd01f357d2d89472110ce765c48a3a02
>>>>>>> master
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
<<<<<<< HEAD
=======
<<<<<<< HEAD

=======
    private void GetAnimations()
    {
>>>>>>> master

    private void ChangeSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    private void GetAnimations()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //animation.Play("WalkDown");
        }
    }
    
>>>>>>> f029b5c5fd01f357d2d89472110ce765c48a3a02
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
