using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float invulnerabilityDuration = 2f;

    private int currentHealth;
    private bool isInvulnerable;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private Rigidbody playerRigidbody;

    // Optional: reference to the SpriteRenderer or material for blinking effect
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkInterval = 0.2f;

    // We’ll rely on an external UI script to update hearts (HeartsUI or something similar).
    public System.Action<int, int> OnHealthChanged;
    // e.g. (currentHP, maxHP) => UpdateHeartsUI()

    private void Start()
    {
        // Find the HeartsUI in the scene
        HeartsUI heartsUI = FindObjectOfType<HeartsUI>();

        if (heartsUI != null)
        {
            // Whenever the player's health changes, update hearts
            OnHealthChanged += heartsUI.UpdateHearts;

            // Initialize the UI immediately
            heartsUI.UpdateHearts(currentHealth, maxHealth);
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        if (!playerRigidbody) playerRigidbody = GetComponent<Rigidbody>();
        if (!spriteRenderer) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// Receive damage from any source (enemy, environment).
    /// </summary>
    /// <param name="damage">Amount of HP lost.</param>
    public void TakeDamage(int damage, Vector3? knockbackDirection = null)
    {
        // If player is invulnerable, ignore.
        if (isInvulnerable) return;

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        // Trigger knockback if direction provided
        if (knockbackDirection.HasValue)
        {
            Knockback(knockbackDirection.Value);
        }

        // Immediately become invulnerable for a short duration
        StartCoroutine(InvulnerabilityRoutine());

        // Notify UI or other systems that HP changed
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        // Check if we died
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Knockback(Vector3 direction)
    {
        // Stop any existing knockback routine to avoid overlapping pushes
        StopAllCoroutines();

        // Start pushing the player smoothly over (e.g.) 0.25 seconds
        float knockbackDuration = 0.25f;
        StartCoroutine(KnockbackRoutine(direction, knockbackDuration));
    }

    private IEnumerator KnockbackRoutine(Vector3 direction, float duration)
    {
        // Ensure direction is normalized
        direction = direction.normalized;

        float elapsed = 0f;
        // We'll distribute our total knockbackForce across this duration.
        // The actual force per frame depends on your ForceMode and Time.fixedDeltaTime below.

        while (elapsed < duration)
        {
            // Make sure we do this in sync with physics
            yield return new WaitForFixedUpdate();

            // How much of the knockbackForce to apply *this* frame
            float fraction = Time.fixedDeltaTime / duration;
            Vector3 frameForce = direction * (knockbackForce * fraction);

            // Add a fraction of the total push
            if (playerRigidbody)
            {
                // You can experiment with ForceMode.Impulse vs. ForceMode.Force/Acceleration
                playerRigidbody.AddForce(frameForce, ForceMode.Impulse);
            }

            elapsed += Time.fixedDeltaTime;
        }
    }


    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;

        float timer = 0f;
        bool spriteVisible = true;

        // Basic blink logic
        while (timer < invulnerabilityDuration)
        {
            timer += blinkInterval;

            if (spriteRenderer)
            {
                spriteVisible = !spriteVisible;
                spriteRenderer.enabled = spriteVisible;
            }

            yield return new WaitForSeconds(blinkInterval);
        }

        // End invulnerability + ensure sprite is visible
        if (spriteRenderer) spriteRenderer.enabled = true;
        isInvulnerable = false;
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // For now, just log. You could do scene reload, open a Game Over UI, etc.
    }

}