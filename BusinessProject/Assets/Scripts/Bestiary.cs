using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this namespace for UnityEngine.UI.Image

public class Bestiary : MonoBehaviour
{
    [SerializeField] private GameObject bestiaryImage;
    [SerializeField] private GameObject buttonNext;
    [SerializeField] private GameObject buttonPrevious;
    [SerializeField] private GameObject sprite1;
    [SerializeField] private GameObject sprite2;

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
        //Optionally disable UI elements initially
        bestiaryImage.SetActive(false);
        buttonNext.SetActive(false);
        buttonPrevious.SetActive(false);
        
        if(sprite1 && sprite2) sprite1.SetActive(false); sprite2.SetActive(false);
    }

    public void ManageBestiary()
    {
        bool isActive = bestiaryImage.activeInHierarchy;
        bestiaryImage.SetActive(!isActive);
        buttonNext.SetActive(!isActive);
        buttonPrevious.SetActive(!isActive);
        if (sprite1 && sprite2)
        {
            sprite1.SetActive(!isActive); 
            sprite2.SetActive(!isActive);
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