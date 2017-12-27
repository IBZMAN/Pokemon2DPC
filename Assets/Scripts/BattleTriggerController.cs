using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTriggerController : MonoBehaviour {

    
    public GameObject playerCamera;
    public GameObject battleCamera;
    [SerializeField] private string LoadNextLevel;
    
    void Start () {
        playerCamera.SetActive(true);
        battleCamera.SetActive(false);
    }
	
	
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerCamera.SetActive(false);
            battleCamera.SetActive(true);
            PlayerController Player = collision.gameObject.GetComponent<PlayerController>();
            transform.localScale = new Vector2(5, 5);
            SceneManager.LoadScene(LoadNextLevel);
            


    
        }
        
    }
}
