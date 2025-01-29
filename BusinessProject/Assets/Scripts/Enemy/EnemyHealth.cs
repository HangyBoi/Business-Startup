using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    private int currentHealth;

    [Header("Death")]
    [SerializeField] private GameObject itemToSpawn;
    [SerializeField] private float destroyDelay = 0.5f;

    [Header("Knockback")]
    [SerializeField] private float knockbackDuration = 0.2f;

    // Addition for Sound
    public UnityEvent onTakeDamage;

    // Reference to the (kinematic) Rigidbody, just so you can detect or configure if needed
    private Rigidbody rb;
    private Coroutine knockbackRoutine;

    public event System.Action OnEnemyDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();

        // Make sure it's kinematic in code if you want to enforce it
        if (rb)
        {
            rb.isKinematic = true;
        }
    }

    public void TakeDamage(int damage, Vector3 knockbackVector)
    {
        // Subtract HP
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        //Sound
        if (onTakeDamage != null)
        {
            onTakeDamage.Invoke();
        }

        // Start knockback logic
        if (knockbackVector != Vector3.zero)
        {
            if (knockbackRoutine != null)
            {
                StopCoroutine(knockbackRoutine);
            }
            knockbackRoutine = StartCoroutine(DoKnockback(knockbackVector));
        }

        // Check for death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Since we're Kinematic, AddForce won't work. 
    /// Instead, manually move the enemy over knockbackDuration.
    /// </summary>
    private IEnumerator DoKnockback(Vector3 knockbackVector)
    {

        float elapsed = 0f;
        // We interpret knockbackVector as the total displacement over the entire knockbackDuration.
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + knockbackVector;

        // Simple linear interpolation from startPos to endPos
        while (elapsed < knockbackDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / knockbackDuration);

            // Lerp towards the end position
            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        // Optionally re-enable your AI or other movement
        // if (ai) ai.enabled = true;
    }

    private void Die()
    {
        Debug.Log($"{name} died!");
        Invoke("InvokeEnemyDeath", destroyDelay);
        //OnEnemyDeath?.Invoke();

        if (itemToSpawn)
        {
            Instantiate(itemToSpawn, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, destroyDelay);
    }

    void InvokeEnemyDeath()
    {
        OnEnemyDeath?.Invoke();
    }
}
