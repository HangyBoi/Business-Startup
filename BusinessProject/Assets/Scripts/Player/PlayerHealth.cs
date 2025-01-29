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
    [SerializeField] private float knockbackDuration = 0.25f; // how long the push lasts
    [SerializeField] private Rigidbody playerRigidbody;

    [Header("Blink Settings")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkInterval = 0.2f;

    // For UI updates
    public System.Action<int, int> OnHealthChanged;

    private Coroutine knockbackRoutine;
    private Coroutine invulRoutine;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (!playerRigidbody) playerRigidbody = GetComponent<Rigidbody>();
        if (!spriteRenderer) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        // Example: wire up hearts UI if you have it
        HeartsUI heartsUI = FindObjectOfType<HeartsUI>();
        if (heartsUI != null)
        {
            OnHealthChanged += heartsUI.UpdateHearts;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        //Play Damaged Sound
        Player_Damaged_Sound damagedSound = GetComponent<Player_Damaged_Sound>();
        if (damagedSound != null)
        {
            OnHealthChanged += damagedSound.PlayDamageSound;
        }

    }

    public void TakeDamage(int damage, Vector3? knockbackDirection = null)
    {
        if (isInvulnerable) return;

        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        if (knockbackDirection.HasValue)
        {
            // Apply the passed knockback force directly
            Vector3 finalForce = knockbackDirection.Value;

            if (knockbackRoutine != null) StopCoroutine(knockbackRoutine);
            knockbackRoutine = StartCoroutine(KnockbackRoutine(finalForce));
        }

        // Trigger invulnerability & blink
        if (invulRoutine != null) StopCoroutine(invulRoutine);
        invulRoutine = StartCoroutine(InvulnerabilityRoutine());

        // Notify UI
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        // Check for death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator KnockbackRoutine(Vector3 force)
    {
        // Temporarily disable your movement script
        PlayerMovement movementScript = GetComponent<PlayerMovement>();
        if (movementScript != null) movementScript.enabled = false;

        // Clear and add impulse
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.AddForce(force, ForceMode.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        // Re-enable movement
        if (movementScript != null) movementScript.enabled = true;
    }


    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;

        float timer = 0f;
        bool spriteVisible = true;

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
        // e.g. reload scene or show game over
    }
}