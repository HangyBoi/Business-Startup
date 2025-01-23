using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HeartsUI : MonoBehaviour
{
    [Header("Heart UI Setup")]
    [SerializeField] private Image heartPrefab;
    [SerializeField] private Transform heartsContainer;
    
    private List<Image> hearts = new List<Image>();

    /// <summary>
    /// Called when PlayerHealth.OnHealthChanged fires
    /// </summary>
    /// <param name="currentHP">The player's current HP</param>
    /// <param name="maxHP">The player's max HP</param>
    public void UpdateHearts(int currentHP, int maxHP)
    {
        // First, adjust the total number of heart UI objects
        if (hearts.Count < maxHP)
        {
            // create more hearts
            for (int i = hearts.Count; i < maxHP; i++)
            {
                Image newHeart = Instantiate(heartPrefab, heartsContainer);
                hearts.Add(newHeart);
            }
        }
        else if (hearts.Count > maxHP)
        {
            // remove extra hearts (unlikely, but in case maxHP changes)
            for (int i = hearts.Count - 1; i >= maxHP; i--)
            {
                Destroy(hearts[i].gameObject);
                hearts.RemoveAt(i);
            }
        }

        // Now update fill/active status based on currentHP
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentHP) hearts[i].color = Color.white; // full heart
            else hearts[i].color = new Color(1f, 1f, 1f, 0.3f); // "dimmed" or an empty heart sprite
        }
    }

    /// <summary>
    /// Show or hide the entire hearts container (e.g. outside of combat).
    /// </summary>
    public void SetHeartsVisible(bool visible)
    {
        if (heartsContainer) heartsContainer.gameObject.SetActive(visible);
    }
}
