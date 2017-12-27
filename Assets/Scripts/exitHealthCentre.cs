using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHealthCentre : MonoBehaviour {

    [SerializeField]
    private string LoadNextlevel;
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player Player = collision.gameObject.GetComponent<Player>();
            SceneManager.LoadScene(LoadNextlevel);
            Invoke("PlayerPositionAfterExit", 1f);
        }
        
    }
    void PlayerPositionAfterExit()
    {
        Player Player = gameObject.GetComponent<Player>();
        Player.transform.position = new Vector2(5.83f, -0.80f);
    }
}
