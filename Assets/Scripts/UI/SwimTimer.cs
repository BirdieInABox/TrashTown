//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimTimer : MonoBehaviour
{
    //The time left in the current bottle
    private float timeLeft;

    //The max time of the current bottle
    private float timeCapacity;

    [SerializeField]
    private Upgrades upgrades;
    private Slider swimSlider;

    // Start is called before the first frame update
    void Awake()
    {
        timeCapacity = upgrades.GetCapacity();
        timeLeft = timeCapacity;
        swimSlider = GetComponent<Slider>();
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
        //tell scene manager to change back to land scene
        SceneLoader.SwitchScene();
    }
}
