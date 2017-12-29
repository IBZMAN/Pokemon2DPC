using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Player thePlayer;

    public static Vector2 playerPosition;

    public Vector2 direction;

    public static int doorID;

    public List<SpawnPoint> spawnPoints;

    //public readonly Vector2 outsideHealthCentreDoor = new Vector2(-2.51f, -1.7f);
    //public readonly Vector2 outsidePokeMartDoor = new Vector2(2.25f, -1.67f);
    //public readonly Vector2 insideHealthCentre = new Vector2(-143.32f, -2.6f);
    //public readonly Vector2 insidePokeMart = new Vector2(6.71f, 129.66f);

    void Start () {
        thePlayer = FindObjectOfType<Player>();

        FillSpawnPoints();
	}

    private void FillSpawnPoints()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("spawnPoint");

        for (int i = 0; i < temp.Length; i++)
        {
            spawnPoints.Add(new SpawnPoint());
            spawnPoints[i].spawnName = temp[i].name;
            spawnPoints[i].position = GetOffsetSpawnPos(temp[i]);
        }

        Debug.Log("Number of spawn points: " + spawnPoints.Count);
    }

    // Experimental - replace need for hard coded building coordinates
    private Vector2 GetOffsetSpawnPos(GameObject target)
    {
        if (target.name.Contains("Outside"))
        {
            Vector2 offsetPos = new Vector2(target.transform.position.x, target.transform.position.y);
            offsetPos.y -= 0.9f;
            return offsetPos;
        }
        else if(target.name.Contains("Inside"))
        {
            Vector2 offsetPos = new Vector2(target.transform.position.x, target.transform.position.y);
            offsetPos.y += 0.9f;
            return offsetPos;
        }

        return new Vector2();
    }

    void Update () {
        GetDirection();
	}

    private void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
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
            //ChangeSprite(thePlayer.gameObject, thePlayer.northSprite);
            direction += Vector2.up;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //ChangeSprite(thePlayer.gameObject, thePlayer.westSprite);
            direction += Vector2.left;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            //ChangeSprite(thePlayer.gameObject, thePlayer.southSprite);
            direction += Vector2.down;
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //ChangeSprite(thePlayer.gameObject, thePlayer.eastSprite);
            direction += Vector2.right;
        }
    }

    private void ChangeSprite(GameObject target, Sprite newSprite)
    {
        target.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
