using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // A public variable to set the scene name in the Inspector
    [SerializeField] private string sceneToLoad;

    // Method to load the scene set in the Inspector
    public void LoadSelectedScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log($"Attempting to load scene: {sceneToLoad}");

            if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
                Debug.Log($"Successfully loaded scene: {sceneToLoad}");
            }
            else
            {
                Debug.LogError($"Scene {sceneToLoad} does not exist or is not added to the build settings.");
            }
        }
        else
        {
            Debug.LogError("No scene name set! Please assign a scene name in the Inspector.");
        }
    }

    // Optional: Method to load a scene by name (can also be used for direct calls)
    public void LoadSceneByName(string sceneName)
    {
        Debug.Log($"Attempting to load scene: {sceneName}");

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Successfully loaded scene: {sceneName}");
        }
        else
        {
            Debug.LogError($"Scene {sceneName} does not exist or is not added to the build settings.");
        }
    }
}
