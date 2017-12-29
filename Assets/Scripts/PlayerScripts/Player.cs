using System.Collections.Generic;
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

    Animator myAnimator;

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
        gameObject.GetComponent<SpriteRenderer>().sprite = startingSprite;
    }

    void Update ()
    {
        HandleAnimations();
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
        if (collision.gameObject.name == "EnterHealthCentre")
        {
            int index = GetIndex(FindObjectOfType<GameManager>().spawnPoints, "ExitHealthCentre");
            transform.position = FindObjectOfType<GameManager>().spawnPoints[index].position;
        }

        if (collision.gameObject.name == "EnterPokeMart")
        {
            transform.position = FindObjectOfType<GameManager>().insidePokeMart;
        }

        if (collision.gameObject.name == "ExitHealthCentre")
        {
            transform.position = FindObjectOfType<GameManager>().outsideHealthCentreDoor;
        }

        if (collision.gameObject.name == "ExitPokeMart")
        {
            transform.position = FindObjectOfType<GameManager>().outsidePokeMartDoor;
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dialogueTrigger"))
        {
            collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    private void HandleAnimations()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            myAnimator.SetInteger("State", 1);         
        }

        // if (Input.GetKeyUp(KeyCode.W))
        //{
        //  myAnim.SetInteger("State", -1);
        //}

        if (Input.GetKeyDown(KeyCode.A))
        {
            myAnimator.SetInteger("State", -2);
        }

        // if (Input.GetKeyUp(KeyCode.A))
        // {
        //myAnim.SetInteger("State", 2);
        //}da

        if (Input.GetKeyDown(KeyCode.S))
        {
            myAnimator.SetInteger("State", -1);
        }

        //if (Input.GetKeyDown(KeyCode.S))
        //{
            //myAnim.SetInteger("State", -3);
        //}
        
        if (Input.GetKeyDown(KeyCode.D))
         {
           myAnimator.SetInteger("State", 2);
        }

        //if (Input.GetKeyUp(KeyCode.D))
        //{
            //myAnim.SetInteger("State", -2);
        //}
    }
}
