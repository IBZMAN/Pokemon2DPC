using UnityEngine;
using System.Collections;

public class EditorHitbox : MonoBehaviour 
{
	void Start () 
	{
	    if(Application.isPlaying == true)
        {
            this.gameObject.SetActive(false);
        }
	}
}
