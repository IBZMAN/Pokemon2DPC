using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class exitHealthCentre : MonoBehaviour {

    [SerializeField] private string LoadNextlevel;

  
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
            SceneManager.LoadScene(LoadNextlevel);
            Invoke("PlayerPositionAfterExit", 1f);
            
         
        }
        
    }
    void PlayerPositionAfterExit()
    {
        PlayerController Player = gameObject.GetComponent<PlayerController>();
        Player.transform.position = new Vector2(5.83f, -0.80f);
    }
}
