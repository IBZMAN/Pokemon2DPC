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

    [SerializeField]
    private float runSpeed;

    Animator myAnimator;

    // Add mouse wheel to zoom out?

    private bool IsPressingSprint
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button4);
        }
    }

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
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
        GetInput();

        if (IsMoving) 
        {
            AnimateMovement(direction);
        }
        else
        {
            myAnimator.SetLayerWeight(2, 0);
            myAnimator.SetLayerWeight(1, 0);
        }

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

    public void AnimateMovement(Vector2 direction)
    {
        myAnimator.SetLayerWeight(1, 1);

        if (IsPressingSprint)
        {
            myAnimator.SetLayerWeight(2, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(2, 0);
        }

        myAnimator.SetFloat("x", direction.x);
        myAnimator.SetFloat("y", direction.y);

    }

    void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            direction += Vector2.up;
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            direction += Vector2.down;
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction += Vector2.right;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction += Vector2.left;
        }
    }
}
