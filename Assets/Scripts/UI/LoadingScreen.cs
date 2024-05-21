using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingSlider; // Reference to the UI slider for progress
    public TMP_Text loadingText; // Reference to the UI text for progress percentage

    public string sceneToLoad; // Name of the scene to load

    void Start()
    {
        // Start the loading process in a coroutine to allow UI updates
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // Normalize progress

            // Update UI elements
            loadingSlider.value = progress;
            loadingText.text = $"Loading: {Mathf.Round(progress * 100)}%";

            yield return null; // Wait for the next frame
        }
    }
}
