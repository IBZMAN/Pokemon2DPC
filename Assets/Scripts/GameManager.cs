using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject thePlayer;

    public static Vector2 playerPosition;

    public static int doorID;

	// Use this for initialization
	void Start () {
        thePlayer = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(doorID);
	}

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void LateUpdate()
    {
        playerPosition = thePlayer.transform.position;
    }
}
