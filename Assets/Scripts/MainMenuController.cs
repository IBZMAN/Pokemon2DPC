using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour {

    [SerializeField] private string NextMenuScene;
	

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(NextMenuScene);
        }

    }
}
