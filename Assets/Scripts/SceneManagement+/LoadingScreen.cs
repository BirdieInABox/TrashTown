//Author: Kim Effie Proestler
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingSlider; // Reference to the UI slider for progress
    public TMP_Text loadingText; // Reference to the UI text for progress percentage
    public float screenDuration; //The time artificially added to the loading screen (in seconds)

    void Update()
    {
        //if the timer has not run out yet
        if (screenDuration > 0)
        {
            //reduce timer
            screenDuration -= Time.deltaTime;
            //if the timer is <= zero now
            if (screenDuration <= 0)
            {
                //Start loading the next scene
                StartCoroutine(LoadSceneAsync());
            }
        }
    }

    /// <summary>
    /// Loads a scene asynchronously and displays the progress as a loading bar and text
    /// </summary>
    private IEnumerator LoadSceneAsync()
    {
        //Load the scene asynchronously
        AsyncOperation asyncLoading = SceneManager.LoadSceneAsync(SceneLoaderData.sceneToLoad);

        //While the net scene is loading
        while (!asyncLoading.isDone)
        {
            //calculate progress and normalize it
            float progress = Mathf.Clamp01(asyncLoading.progress);

            // Update UI elements
            loadingSlider.value = progress;
            loadingText.text = $"Loading: {Mathf.Round(progress * 100)}%";
            yield return null; // Wait for the next frame
        }
    }
}
