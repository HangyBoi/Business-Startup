using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI; // Add this namespace for UnityEngine.UI.Image

public class Bestiary : MonoBehaviour
{
    [SerializeField] private GameObject bestiaryImage;
    [SerializeField] private List<GameObject> objectsToHide;

    [SerializeField] private Sprite[] pages; // Change to Sprite if using UnityEngine.UI.Image
    private int currentPage = 0; // To keep track of the current page

    void Start()
    {
        if (pages.Length != 0)
        {
            Image imageComponent = bestiaryImage.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = pages[0];
            }
            else
            {
                Debug.LogError("No Image component found on bestiaryImage GameObject.");
            }
        }
        
        if(objectsToHide.Count!=0)
            foreach (GameObject obj in objectsToHide)
            {
                obj.SetActive(false);
            }
    }

    public void ManageBestiary()
    {
        bool isActive = bestiaryImage.activeInHierarchy;
        if(objectsToHide.Count!=0)
            foreach (GameObject obj in objectsToHide)
            {
                obj.SetActive(!isActive);
            }
    }

    // Example methods for navigating pages
    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            UpdatePage();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    private void UpdatePage()
    {
        Image imageComponent = bestiaryImage.GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.sprite = pages[currentPage];
        }
    }
}