﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthCentreController : MonoBehaviour
{

    [SerializeField]
    private string LoadHealthInside;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(LoadHealthInside);
        }
    }
}
