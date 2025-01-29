using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void GoToLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}
