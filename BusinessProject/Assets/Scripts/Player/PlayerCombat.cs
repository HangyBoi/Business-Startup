using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private AttackBarController attackBar;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private float weaponRange = 1.5f;  // Bigger than enemy�s range
    [SerializeField] private LayerMask enemyLayer;

    [Header("Damage Values")]
    [SerializeField] private int basicDamage = 1;
    [SerializeField] private int perfectDamage = 3;

    [Header("Knockback Settings")]
    [SerializeField] private float basicKnockback = 4f;
    [SerializeField] private float perfectKnockback = 6f;

    [Header("Audio Settings")]
    [SerializeField] private Weapon_Swing weaponSwing; // Reference to Weapon_Swing

    private bool isAttacking;
    private float nextAttackAllowedTime;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>(); // Get the Animator component
        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on PlayerCombat.");
        }

        if (weaponSwing == null)
        {
            Debug.LogWarning("Weapon_Swing reference not set in PlayerCombat.");
        }
    }

    private void Update()
    {
        // If on cooldown, do nothing
        if (Time.time < nextAttackAllowedTime) return;

        // Mouse down => start the attack
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                StartAttack();
            }
            else
            {
                EndAttack();
            }
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        attackBar.gameObject.SetActive(true);
        attackBar.StartArrowMovement();
    }

    private void EndAttack()
    {
        attackBar.gameObject.SetActive(false);

        if (animator != null)
        {
            animator.SetBool("IsAttacking", true);
        }

        // Check if arrow landed in red zone
        bool success = attackBar.WasArrowInRedZone();

        int damageToDeal = success ? perfectDamage : basicDamage;
        float knockbackToApply = success ? perfectKnockback : basicKnockback;

        // Attempt to strike enemies in range
        AttemptDealDamage(damageToDeal, knockbackToApply);

        // Play swing sound based on success
        if (weaponSwing != null)
        {
            weaponSwing.PlaySwingSound(success);
        }

        // Start cooldown
        nextAttackAllowedTime = Time.time + attackCooldown;
        isAttacking = false;
    }

    // **This method will be called via an Animation Event from the SpriteAnimDirController at the end of the attack animation**
    public void OnAttackAnimationEnd()
    {
        if (animator != null)
        {
            animator.SetBool("IsAttacking", false);
        }

        isAttacking = false;
    }

    private void AttemptDealDamage(int damage, float knockbackForce)
    {
        Collider[] hits = Physics.OverlapSphere(weaponPivot.position, weaponRange, enemyLayer);

        foreach (Collider c in hits)
        {
            EnemyHealth eHealth = c.GetComponent<EnemyHealth>();
            if (eHealth != null)
            {
                // Direction from Player -> Enemy
                Vector3 knockbackDir = (c.transform.position - transform.position).normalized * knockbackForce;
                eHealth.TakeDamage(damage, knockbackDir);

                Debug.Log("Enemy got HIT");
            }
        }
    }
}
