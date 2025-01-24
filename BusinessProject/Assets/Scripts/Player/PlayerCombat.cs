using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private AttackBarController attackBar;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private float weaponRange = 1.5f;  // bigger than enemy’s attack range
    [SerializeField] private LayerMask enemyLayer;

    [Header("Damage Values")]
    [SerializeField] private int basicDamage = 1;
    [SerializeField] private int perfectDamage = 3;

    [Header("Knockback Settings")]
    [SerializeField] private float basicKnockback = 4f;
    [SerializeField] private float perfectKnockback = 6f;

    private bool isAttacking;
    private float nextAttackAllowedTime;

    private void Update()
    {
        // If on cooldown, do nothing
        if (Time.time < nextAttackAllowedTime) return;

        // Mouse down => start the bar
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartAttack();
        }

        // Mouse up => finalize the bar
        if (Input.GetMouseButtonUp(0) && isAttacking)
        {
            EndAttack();
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        // Show the bar above the player
        attackBar.gameObject.SetActive(true);
        attackBar.StartArrowMovement(); // might be your own method to reset the arrow
    }

    private void EndAttack()
    {
        isAttacking = false;
        attackBar.gameObject.SetActive(false);

        // Check if arrow landed in a red zone
        bool success = attackBar.WasArrowInRedZone();

        int damageToDeal = success ? perfectDamage : basicDamage;
        float knockbackToApply = success ? perfectKnockback : basicKnockback;

        // Attempt to strike enemies in front of the weaponPivot
        AttemptDealDamage(damageToDeal, knockbackToApply);

        // Start cooldown
        nextAttackAllowedTime = Time.time + attackCooldown;
    }

    private void AttemptDealDamage(int damage, float knockbackForce)
    {
        // We can do a sphere or box overlap to detect enemies in range.
        // Alternatively use a weapon collider. Here, we do a simple sphere check:
        Collider[] hits = Physics.OverlapSphere(weaponPivot.position, weaponRange, enemyLayer);

        foreach (Collider c in hits)
        {
            // We might have an EnemyHealth script
            EnemyHealth eHealth = c.GetComponent<EnemyHealth>();
            if (eHealth != null)
            {
                // apply damage
                eHealth.TakeDamage(damage,
                    (c.transform.position - transform.position).normalized * knockbackForce);

                // e.g. we pass a combined "knockback vector" to the enemy.
                Debug.Log("Enemy got HIT");
            }
        }
    }
}
