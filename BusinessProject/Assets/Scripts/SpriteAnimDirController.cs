using UnityEngine;

public class SpriteAnimDirController : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private Transform mainTransform;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    [Range(0f, 180f)] [SerializeField] private float backAngle = 45f;
    private float sideAngle;

    private void Start()
    {
        sideAngle = 180 - backAngle;
        if(Camera.main) mainCam = Camera.main; else Debug.LogWarning("No main Camera!");
        // if(GetComponentInParent<Transform>()) mainTransform = GetComponentInParent<Transform>(); else Debug.LogWarning("no transform found in sprite dir controller");
        if(GetComponent<Animator>()) animator = GetComponent<Animator>(); else Debug.LogWarning("no animator found in sprite dir controller");
        if(GetComponent<SpriteRenderer>()) spriteRenderer = GetComponent<SpriteRenderer>(); else Debug.LogWarning("no spriteRenderer found in sprite dir controller");
    }
    private void LateUpdate()
    {
        if (mainCam)
        {
            Vector3 camForwardVector = new Vector3(mainCam.transform.forward.x, 0f, mainCam.transform.forward.z);
            Debug.DrawRay(mainCam.transform.position, camForwardVector * 5f, Color.magenta);
            float signedAngle = Vector3.SignedAngle(mainTransform.forward, camForwardVector, Vector3.up);

            Vector2 animationDirection = new Vector2(0, -1f);
            
            float angle = Mathf.Abs(signedAngle);

            if (angle < backAngle) animationDirection = new Vector2(0, -1f);
            else if (angle < sideAngle)
            {
                if (signedAngle < 0) animationDirection = new Vector2(1f, 0f);
                else animationDirection = new Vector2(-1f, 0f);
            }
            else animationDirection = new Vector2(0f, 1f);
            
            Debug.Log(angle);
            
            animator.SetFloat("MoveX", animationDirection.x);
            animator.SetFloat("MoveY", animationDirection.y);
        }
        else Debug.LogWarning("No main Camera!");
    }
}
