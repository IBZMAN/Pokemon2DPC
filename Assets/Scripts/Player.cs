using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{ 
    [SerializeField]
    public Sprite startingSprite;

    [SerializeField]
    public Sprite northSprite;

    [SerializeField]
    public Sprite eastSprite;

    [SerializeField]
    public Sprite southSprite;

    [SerializeField]
    public Sprite westSprite;

    [SerializeField]
    public Vector2 position;

    private Door doorController;

    void Start ()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = startingSprite;     
    }

    void LateUpdate()
    {
        position = transform.position;
    }

    private void FixedUpdate()
    {

    }

    //private void GetInput()
    //{
    //    direction = Vector2.zero;

    //    if (Input.GetAxisRaw("Vertical") > 0)
    //    {
    //        //myAnimator.SetFloat("velY", myRigidBody.velocity.y);
    //        ChangeSprite(northSprite);
    //        direction += Vector2.up;
    //    }
    //    if (Input.GetAxisRaw("Horizontal") < 0)
    //    {
    //        //myAnimator.SetFloat("velX", myRigidBody.velocity.x);
    //        ChangeSprite(westSprite);
    //        direction += Vector2.left;
    //    }
    //    if (Input.GetAxisRaw("Vertical") < 0)
    //    {
    //        //myAnimator.SetFloat("velY", myRigidBody.velocity.y);
    //        ChangeSprite(southSprite);
    //        direction += Vector2.down;
    //    }
    //    if (Input.GetAxisRaw("Horizontal") > 0)
    //    {
    //        //myAnimator.SetFloat("velX", myRigidBody.velocity.x);
    //        ChangeSprite(eastSprite);
    //        direction += Vector2.right;
    //    }
    //}

    //private void ChangeSprite(Sprite newSprite)
    //{
    //    gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    //}

    //private void Move()
    //{
    //    if (IsPressingSprint)
    //    {
    //        myRigidBody.velocity = FindObjectOfType<GameManager>().direction.normalized * (speed + 3f);
    //    }
    //    else
    //    {
    //        myRigidBody.velocity = FindObjectOfType<GameManager>().direction.normalized * speed;
    //    }   
    //}

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dialogueTrigger"))
        {
            collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }
}
