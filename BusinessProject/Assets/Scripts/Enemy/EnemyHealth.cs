using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    private int currentHealth;

    [Header("Death")]
    [SerializeField] private GameObject itemToSpawn;   // e.g. coin or potion
    [SerializeField] private float destroyDelay = 0.5f;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Called by PlayerCombat or other damage sources
    /// The second parameter is a "knockback vector" we could use.
    /// Alternatively, pass in (Vector3 direction, float force).
    /// </summary>
    public void TakeDamage(int damage, Vector3 knockback = default)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        // optional knockback
        if (knockback != Vector3.zero)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb) rb.AddForce(knockback, ForceMode.Impulse);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Optionally trigger a death animation before destroying
        Debug.Log($"{name} died!");

        // spawn item
        if (itemToSpawn)
        {
            Instantiate(itemToSpawn, transform.position, Quaternion.identity);
        }

        // Wait a short delay to allow animation, then destroy
        Destroy(gameObject, destroyDelay);
    }
}
