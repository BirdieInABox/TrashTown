using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Coding = 0,
    Coding2 = 1
}

public static class SceneLoader
{
    public static void SwitchScene()
    {
        if (string.Equals(SceneManager.GetActiveScene().name, ((Scenes)1).ToString()))
        {
            SceneManager.LoadScene(((Scenes)0).ToString());
        }
        else if (string.Equals(SceneManager.GetActiveScene().name, ((Scenes)0).ToString()))
        {
            SceneManager.LoadScene(((Scenes)1).ToString());
        }
    }

    public static void PassOut()
    {
        //don't save first, switch instantly
        SwitchScene();
    }
    public static void ExitScene()
    {
        //Save first, then switch
        SwitchScene();
    }
}
