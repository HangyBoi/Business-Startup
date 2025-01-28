using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    private PlayerMovement player;

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerMovement>();
        player.isActive = false;
    }

    public void CloseMap()
    {
        Debug.Log("close map");
        player.isActive = true;
        Destroy(gameObject);
    }
    
    public void GoToLevel()
    {
        SceneManager.LoadScene("LevelScene");
        CloseMap();
    }
}