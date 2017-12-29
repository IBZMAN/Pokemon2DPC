using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{ 
    //[SerializeField]
    //public Sprite startingSprite;

    //[SerializeField]
    //public Sprite northSprite;

    //[SerializeField]
    //public Sprite eastSprite;

    //[SerializeField]
    //public Sprite southSprite;

    //[SerializeField]
    //public Sprite westSprite;

    [SerializeField]
    public Vector2 position;

    Animator myAnimator;

    // Add mouse wheel to zoom out?

    private bool IsPressingSprint
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button4);
        }
    }

    void Start ()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        //gameObject.GetComponent<SpriteRenderer>().sprite = startingSprite;
    }

    void Update ()
    {
        HandleIdleAnimations();
        HandleWalkingAnimations();

    }

    void LateUpdate()
    {
        position = transform.position;
    }

    private int GetIndex(List<SpawnPoint> spawnPoints, string name)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i].spawnName == name)
            {
                return i;
            }
        }
        return -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "OutsideHealthCentre")
        {
            int index = GetIndex(FindObjectOfType<GameManager>().spawnPoints, "InsideHealthCentre");
            transform.position = FindObjectOfType<GameManager>().spawnPoints[index].position;
        }

        if (collision.gameObject.name == "OutsidePokeMart")
        {
            int index = GetIndex(FindObjectOfType<GameManager>().spawnPoints, "InsidePokeMart");
            transform.position = FindObjectOfType<GameManager>().spawnPoints[index].position;
        }

        if (collision.gameObject.name == "InsideHealthCentre")
        {
            int index = GetIndex(FindObjectOfType<GameManager>().spawnPoints, "OutsideHealthCentre");
            transform.position = FindObjectOfType<GameManager>().spawnPoints[index].position;
        }

        if (collision.gameObject.name == "InsidePokeMart")
        {
            int index = GetIndex(FindObjectOfType<GameManager>().spawnPoints, "OutsidePokeMart");
            transform.position = FindObjectOfType<GameManager>().spawnPoints[index].position;
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dialogueTrigger"))
        {
            collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    private void HandleIdleAnimations()
    {
<<<<<<< HEAD
        if (Input.GetKeyUp(KeyCode.W))
=======
        if (Input.GetAxisRaw("Vertical") > 0)
>>>>>>> 1be261149554ba9fdd46550c554e3e23e872b8e4
        {
            myAnimator.SetInteger("State", 1);         
        }

<<<<<<< HEAD
        // if (Input.GetKeyUp(KeyCode.W))
        //{
        //  myAnim.SetInteger("State", -1);
        //}

        if (Input.GetKeyUp(KeyCode.A))
=======
        if (Input.GetAxisRaw("Horizontal") < 0)
>>>>>>> 1be261149554ba9fdd46550c554e3e23e872b8e4
        {
            myAnimator.SetInteger("State", -2);
        }

<<<<<<< HEAD
        // if (Input.GetKeyUp(KeyCode.A))
        // {
        //myAnim.SetInteger("State", 2);
        //}da

        if (Input.GetKeyUp(KeyCode.S))
=======
        if (Input.GetAxisRaw("Vertical") < 0)
>>>>>>> 1be261149554ba9fdd46550c554e3e23e872b8e4
        {
            myAnimator.SetInteger("State", -1);
        }
        
<<<<<<< HEAD
        if (Input.GetKeyUp(KeyCode.D))
=======
        if (Input.GetAxisRaw("Horizontal") > 0)
>>>>>>> 1be261149554ba9fdd46550c554e3e23e872b8e4
         {
           myAnimator.SetInteger("State", 2);
        }
    }

    private void HandleWalkingAnimations()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        

    }
}
