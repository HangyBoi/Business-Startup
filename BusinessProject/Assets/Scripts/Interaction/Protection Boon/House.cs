using UnityEngine;

public class House : MonoBehaviour
{
    // Assign these in the Inspector
    public Sprite normalSprite;
    public Sprite destroyedSprite;

    private SpriteRenderer spriteRenderer;
    private bool isUpgraded = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("House GameObject requires a SpriteRenderer component.");
        }

    }

    private void OnEnable()
    {
        // Subscribe to the BoonUpgraded event
        ProtectionBoon.BoonUpgraded += OnBoonUpgraded;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        ProtectionBoon.BoonUpgraded -= OnBoonUpgraded;
    }

    private void OnBoonUpgraded()
    {
        // Upgrade the house
        isUpgraded = true;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isUpgraded ? normalSprite : destroyedSprite;
        }
    }
}
