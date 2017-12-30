using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour {

    public int sceneTriggerAmnt = 1;
    public float elapsedTime = 0;
    public float timeUntilTrigger = 7;
    public GameObject sceneTrigger;
    Vector2 sceneTriggerPosition = new Vector2(-76.8f, -30.38f);
    public int i = 0;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > timeUntilTrigger)
        {
            while (i < sceneTriggerAmnt)
            {
                GameObject newSceneTrigger = Instantiate(sceneTrigger);
                newSceneTrigger.transform.position = sceneTriggerPosition;
                i++;
            }
        }
    }
   
}
