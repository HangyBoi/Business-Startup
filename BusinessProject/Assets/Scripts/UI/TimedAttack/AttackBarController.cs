using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AttackBarController : MonoBehaviour
{
    [SerializeField] private RectTransform arrowRect;
    [SerializeField] private float arrowSpeed = 1f;

        // -------------- Zones --------------
    [Header("Red Zones Setup")]
    [SerializeField] private List<RedZoneDefinition> redZones;  // define widths
    [SerializeField] private RectTransform zoneContainer;       // a panel to hold all zone images
    [SerializeField] private Image zonePrefab;                  // prefab or template for a red zone
    [SerializeField, Range(0f, 0.2f)]
    private float spacing = 0.01f;  // how much empty space (fraction of bar width) to leave between zones

    // Internally track the normalized start/end for each zone
    private List<(float start, float end)> zoneRanges = new List<(float, float)>();

    private float arrowPos = -0.5f;  // Range [0..1]
    private int direction = 1;    // 1 = moving right, -1 = moving left
    private bool isActive;

    void Start()
    {
        GenerateZones();
        gameObject.SetActive(false); // attackBar typically hidden at start
    }

    void Update()
    {
        arrowPos += direction * arrowSpeed * Time.deltaTime;

        // If we go beyond 0 or 1, flip direction
        if (arrowPos > 1f) { arrowPos = 1f; direction = -1; }
        else if (arrowPos < 0f) { arrowPos = 0f; direction = 1; }

        // Update the arrowRect’s anchoredPosition accordingly
        UpdateArrowPosition();
    }

    public void StartArrowMovement()
    {
        // Reset arrow data
        arrowPos = 0f;
        direction = 1;
        isActive = true;
    }

    public bool WasArrowInRedZone()
    {
        // isActive = false; // optionally freeze arrow after checking
        // Check if arrowPos was inside any red zone
        foreach (var (start, end) in zoneRanges)
        {
            if (arrowPos >= start && arrowPos <= end)
            {
                return true;
            }
        }
        return false;
    }

    // ----------------------------------------------------
    // 1) Dynamically create the zone UI images + store ranges
    // ----------------------------------------------------
    private void GenerateZones()
    {
        zoneRanges.Clear();

        // Clear existing children if you are regenerating
        foreach (Transform child in zoneContainer)
        {
            Destroy(child.gameObject);
        }

        // The sum of all zone widths plus spacing
        // We'll simply place them left-to-right in normalized space.
        float currentStart = 0.0f;

        for (int i = 0; i < redZones.Count; i++)
        {
            // The zone's width
            float w = redZones[i].relativeWidth;

            // Create a new zone Image
            Image zone = Instantiate(zonePrefab, zoneContainer);
            zone.gameObject.SetActive(true);

            // Set zone's anchor to stretch left-right within zoneContainer
            RectTransform zoneRect = zone.rectTransform;
            zoneRect.anchorMin = new Vector2(0f, 0f);
            zoneRect.anchorMax = new Vector2(0f, 1f);
            zoneRect.pivot = new Vector2(0f, 0.5f);

            // Position in normalized space (0..1 -> local width)
            float barWidth = zoneContainer.rect.width;
            float pixelStart = currentStart * barWidth;
            float pixelWidth = w * barWidth;

            zoneRect.anchoredPosition = new Vector2(pixelStart, 0);
            zoneRect.sizeDelta = new Vector2(pixelWidth, 0);

            // Remember the (start, end) in normalized space:
            float zoneStart = currentStart;
            float zoneEnd = currentStart + w;
            zoneRanges.Add((zoneStart, zoneEnd));

            // Update currentStart for next zone
            currentStart += w + spacing;
        }
    }

    // ----------------------------------------------------
    // 2) Move the arrow across the bar
    // ----------------------------------------------------
    private void UpdateArrowPosition()
    {
        // total bar width
        float barWidth = ((RectTransform)transform).rect.width;

        float leftEdge = -barWidth * 0.5f;
        float rightEdge = barWidth * 0.5f;

        // arrowPos is [0..1], so we linearly map that to [leftEdge..rightEdge]
        float newX = Mathf.Lerp(leftEdge, rightEdge, arrowPos);
        arrowRect.anchoredPosition = new Vector2(newX, arrowRect.anchoredPosition.y);
    }

}
