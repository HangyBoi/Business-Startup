using UnityEngine;
using System.Collections;

public class AttackStateEnemy : State
{
    [SerializeField] private int damageAmount = 1;
    // You can add a knockbackForce if you want stronger push
    [SerializeField] private float knockbackForce = 3f;

    private PlayerHealth playerHealth;
    private TargetDetector targetDetector;
    private Transform playerTransform;

    public override void OnEnterState()
    {
        Debug.Log("Enemy is Attacking");
        targetDetector = GetComponent<TargetDetector>();
        if (targetDetector != null)
        {
            playerTransform = targetDetector.GetTarget().transform;
            playerHealth = playerTransform.GetComponent<PlayerHealth>();
        }

        // Start a simple attack coroutine
        StartCoroutine(Attack());
    }

    public override void Handle() { }
    public override void OnExitState() { }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);

        if (playerHealth != null)
        {
            // Calculate direction and apply knockbackForce
            Vector3 knockbackDir = (playerTransform.position - transform.position).normalized;
            Vector3 knockback = knockbackDir * knockbackForce; // Combine here

            playerHealth.TakeDamage(damageAmount, knockback);
        }

        if (SM) SM.TransitToState(GetComponent<ChasingStateEnemy>());
    }
}