//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// An enum of all scenes
/// </summary>
public enum Scenes
{
    Land = 0,
    Water = 1,

    //2 empty indices for debugging scenes
    MainMenu = 4
}

public static class SceneLoader
{
    /// <summary>
    /// a static function, allows for calling from any script without reference.
    /// Checks the current of the two available gameplay scenes and prepares to load the other.
    /// </summary>
    public static void SwitchScene()
    {
        //If the current scene is at index 1 (Water)
        if (string.Equals(SceneManager.GetActiveScene().name, ((Scenes)1).ToString()))
        {
            //Load scene at index 0 (Land)
            SceneLoaderData.sceneToLoad = ((Scenes)0).ToString();
        }
        //If the current scene is at index 0 (Land)
        else if (string.Equals(SceneManager.GetActiveScene().name, ((Scenes)0).ToString()))
        {
            //Load scene at index 1 (Water)
            SceneLoaderData.sceneToLoad = ((Scenes)1).ToString();
        }
        //Load the Loading Screen
        SceneManager.LoadScene("Loading Screen");
    }
}
