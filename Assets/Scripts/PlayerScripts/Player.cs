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

    private void HandleAnimations()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            myAnimator.SetInteger("State", 1);         
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            myAnimator.SetInteger("State", -2);
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            myAnimator.SetInteger("State", -1);
        }
        
        if (Input.GetAxisRaw("Horizontal") > 0)
         {
           myAnimator.SetInteger("State", 2);
        }
    }
}
