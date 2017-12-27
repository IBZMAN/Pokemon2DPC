using UnityEngine;
using UnityEngine.SceneManagement;

public class PokeMartController : MonoBehaviour
{
    [SerializeField]
    private string NextSceneToLoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(NextSceneToLoad);
        }
    }
}
