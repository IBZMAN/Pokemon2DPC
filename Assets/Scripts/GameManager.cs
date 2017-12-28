using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Player thePlayer;

    public static Vector2 playerPosition;

    public Vector2 direction;

    public static int doorID;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        GetDirection();
	}

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void LateUpdate()
    {
        playerPosition = thePlayer.transform.position;
    }

    private void GetDirection()
    {
        direction = Vector2.zero;

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            ChangeSprite(thePlayer.gameObject, thePlayer.northSprite);
            direction += Vector2.up;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            ChangeSprite(thePlayer.gameObject, thePlayer.westSprite);
            direction += Vector2.left;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            ChangeSprite(thePlayer.gameObject, thePlayer.southSprite);
            direction += Vector2.down;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            ChangeSprite(thePlayer.gameObject, thePlayer.eastSprite);
            direction += Vector2.right;
        }
    }

    private void ChangeSprite(GameObject target, Sprite newSprite)
    {
        target.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
