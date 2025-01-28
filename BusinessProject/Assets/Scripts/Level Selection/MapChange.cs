using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour
{
    // A public variable to set the scene name in the Inspector
    [SerializeField] public string sceneToLoad;

    private void Update()
    {
        // Ensure this script is active and listens for the M key
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadSelectedScene();
        }
    }

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
}
