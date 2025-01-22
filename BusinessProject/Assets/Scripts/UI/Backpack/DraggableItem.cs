using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform parentAfterDrag;
    private Rigidbody2D rb;

    // We'll store the previous frame's position during drag.
    private Vector3 lastMousePosition;

    // We'll store the velocity we calculate ourselves.
    private Vector2 calculatedVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Start as Kinematic so physics won't move it yet.
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        // Record initial mouse position for velocity calc
        lastMousePosition = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Calculate movement in screen space
        Vector3 currentMousePosition = Input.mousePosition;
        transform.position = currentMousePosition;

        // Compute velocity in screen space
        Vector3 delta = currentMousePosition - lastMousePosition;
        float dt = Time.deltaTime;
        if (dt > 0)
        {
            // Convert velocity to world space if needed
            // Right now, it’s basically “screen space units / sec”
            // If your Canvas is Screen Space - Overlay, you might 
            // want to do a proper conversion to world space coords.
            calculatedVelocity = delta / dt;
        }
        lastMousePosition = currentMousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        transform.SetParent(parentAfterDrag);

        // Now we let the Rigidbody2D handle the physics
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Convert the screen-space velocity to world-space if necessary
        // If your canvas is in Screen Space - Overlay, you need a conversion:
        Vector3 worldVelocity =
            Camera.main.ScreenToWorldPoint(Input.mousePosition + (Vector3)calculatedVelocity)
            - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rb.velocity = worldVelocity / Time.deltaTime; // approximate conversion

        // Optionally, add some rotational “spin” if desired:
        // rb.angularVelocity = Random.Range(-200f, 200f);
    }
}
