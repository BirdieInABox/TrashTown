//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Coding = 0,
    Coding2 = 1,
    Land = 2,
    Water = 3,
    MainMenu = 4
}

public static class SceneLoader
{
    public static void SwitchScene()
    {
        if (string.Equals(SceneManager.GetActiveScene().name, ((Scenes)1).ToString()))
        {
            SceneLoaderData.sceneToLoad = ((Scenes)0).ToString();
            //SceneManager.LoadScene(((Scenes)0).ToString());
        }
        else if (string.Equals(SceneManager.GetActiveScene().name, ((Scenes)0).ToString()))
        {
            SceneLoaderData.sceneToLoad = ((Scenes)1).ToString();
            // SceneManager.LoadScene(((Scenes)1).ToString());
        }
        SceneManager.LoadScene("Loading Screen");
    }


}
