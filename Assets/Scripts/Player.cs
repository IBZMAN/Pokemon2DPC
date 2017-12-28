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

    private bool IsPressingSprint
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button4);
        }
    }

    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = startingSprite;

        /*if (GameManager.doorID != 0)
        {
            foreach (var door in GetComponents<Door>())
            {
                if (door.doorID == GameManager.doorID)
                {
                    Vector2 offsetPosition = new Vector2(door.transform.position.x, door.transform.position.y);
                    offsetPosition.y += 0.9f;
                    transform.position = offsetPosition;
                }
            }
        }*/

        //if (lastPos != null)
        //{
        //    Vector2 offsetPosition = new Vector2(GameManager.playerPosition.x, GameManager.playerPosition.y -= 0.08f);
        //    transform.position = offsetPosition;
        //}
    }

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }

    void Update ()
    {
        GetInput();     
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

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "HealthCentreTrigger")
        {
            //playerPosition = transform.position;
            // lastPos.y += 2;
            doorController = collision.GetComponent<Door>();

            GameManager.doorID = doorController.doorID;

            SceneManager.LoadScene("InsideHealthCentre");
        }

        if (collision.gameObject.name == "ExitHealthCentre")
        {
            SceneManager.LoadScene("Fuccsville 1");

            //transform.position = GameManager.playerPosition;

            //transform.position = lastPos;
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 outsideHealthCentreDoor = new Vector2(6, -1);
        Vector2 outsidePokeMartDoor = new Vector2(10.5f, -1);
        Vector2 InsideHealthCentre = new Vector2(-143.32f, -2.6f);
        Vector2 InsidePokeMart = new Vector2(-143.32f, -2.6f);

        if (collision.gameObject.tag == "EnterHealthCentre")
        {
            Player Player = collision.gameObject.GetComponent<Player>();
            Player.transform.position = InsideHealthCentre;
        }

        if (collision.gameObject.tag == "EnterPokeMart")
        {
            Player Player = collision.gameObject.GetComponent<Player>();
            Player.transform.position = InsidePokeMart;
        }

        if (collision.gameObject.tag == "exitHealthCentre")
        {
             Player Player = collision.gameObject.GetComponent<Player>();
             Player.transform.position = outsideHealthCentreDoor;
        }

        if (collision.gameObject.tag == "exitPokeMart")
        {
            Player Player = collision.gameObject.GetComponent<Player>();
            Player.transform.position = outsidePokeMartDoor;
        }
    }
}
