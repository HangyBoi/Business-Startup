using UnityEngine;

public class SpriteAnimDirController : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private Transform mainTransform;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Range(0f, 180f)][SerializeField] private float backAngle = 45f;
    private float sideAngle;

    private Vector2 animationDirection;

    private void Start()
    {
        sideAngle = 180 - backAngle;
        mainCam = Camera.main;
        if (mainCam == null)
        {
            Debug.LogWarning("No main Camera found!");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on SpriteAnimDirController.");
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer component not found on SpriteAnimDirController.");
        }
    }

    private void LateUpdate()
    {
        if (mainCam && animator != null && mainTransform != null)
        {
            Vector3 camForwardVector = new Vector3(mainCam.transform.forward.x, 0f, mainCam.transform.forward.z).normalized;
            Debug.DrawRay(mainCam.transform.position, camForwardVector * 5f, Color.magenta);
            float signedAngle = Vector3.SignedAngle(mainTransform.forward, camForwardVector, Vector3.up);

            float angle = Mathf.Abs(signedAngle);

            animationDirection = Vector2.zero;

            if (angle < backAngle)
            {
                animationDirection = new Vector2(0, -1f);
            }
            else if (angle < sideAngle)
            {
                animationDirection = signedAngle < 0 ? new Vector2(1f, 0f) : new Vector2(-1f, 0f);
            }
            else
            {
                animationDirection = new Vector2(0f, 1f);
            }

            animator.SetFloat("MoveX", animationDirection.x);
            animator.SetFloat("MoveY", animationDirection.y);

            Debug.Log($"MoveX: {animationDirection.x}, MoveY: {animationDirection.y}");
        }
    }
}
