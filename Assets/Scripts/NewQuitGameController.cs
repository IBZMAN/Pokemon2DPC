using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewQuitGameController : MonoBehaviour {

    [SerializeField] private string NextLevelToLoad;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            SceneManager.LoadScene(NextLevelToLoad);
        }
        
    }
}
