//Author: Kim Effie Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimTimer : MonoBehaviour
{
    //The time left in the current bottle
    public float timeLeft;

    //The max time of the current bottle
    public float timeCapacity;

    [SerializeField]
    private Slider swimSlider;

    

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeCapacity;
        swimSlider.maxValue = timeCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0f)
        {
            //Passage of time
            timeLeft -= Time.deltaTime;
            //Depleting the
            swimSlider.value = timeLeft;

            //Automatic event
        }
        else
        {
            PassOut();
        }
    }

    private void PassOut()
    {
        //Set flag to pass-out
        //tell scene manager to change back to land scene
    }
}
