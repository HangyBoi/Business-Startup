using UnityEngine;
using System.Collections;

public class AttackStateEnemy : State
{
    [SerializeField] private int damageAmount = 1;

    private PlayerHealth playerHealth;
    private TargetDetector targetDetector;
    private Transform playerTransform;

    public override void OnEnterState()
    {
        Debug.Log("Enemy is Attacking");
        // Acquire references
        targetDetector = GetComponent<TargetDetector>();
        playerTransform = targetDetector.GetTarget().transform;
        playerHealth = playerTransform.GetComponent<PlayerHealth>();

        // Start a simple attack coroutine
        StartCoroutine(Attack());
    }

    public override void Handle() { }

    public override void OnExitState() { }

    private IEnumerator Attack()
    {
        // Attack animation could last ~1s
        yield return new WaitForSeconds(0.2f);

        // If the player is still within range (optional check)...
        if (playerHealth != null)
        {
            Vector3 knockbackDir = (playerTransform.position - transform.position).normalized;

            playerHealth.TakeDamage(damageAmount, knockbackDir);
        }

        // Return to chasing or another state
        if (SM) SM.TransitToState(GetComponent<ChasingStateEnemy>());
    }
}
