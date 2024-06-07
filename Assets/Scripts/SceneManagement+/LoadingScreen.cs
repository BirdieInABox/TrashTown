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
    public float screenDuration;

    void Start()
    {
        // Start the loading process in a coroutine to allow UI updates
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (screenDuration > 0)
        {
            screenDuration -= Time.deltaTime;
            if (screenDuration <= 0)
            {
                StartCoroutine(LoadSceneAsync());
            }
        }
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneLoaderData.sceneToLoad);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress); // Normalize progress

            // Update UI elements
            loadingSlider.value = progress;
            loadingText.text = $"Loading: {Mathf.Round(progress * 100)}%";
            yield return null; // Wait for the next frame
        }
    }
}
