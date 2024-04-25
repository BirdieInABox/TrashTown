using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimTimer : MonoBehaviour
{
    //The time left in the current cycle
    public float timeLeft;

    //The duration of a cycle
    public float dayDuration;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            //The current rotation
            // currPercentile = maximum - (timeLeft * percentile);
            //Passage of time
            timeLeft -= Time.deltaTime;
            //Rotating the game object


            //Automatic events
        }
    }
}
