using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougueManager : MonoBehaviour {

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}
	
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + name);
    }
}
