using UnityEngine;

public class BoundaryHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BackpackItem"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Adjust velocity to prevent the item from escaping the backpack
                rb.velocity = Vector2.zero;
            }
        }
    }
        
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BackpackItem"))
        {
            // Move the item back inside the panel's boundaries
            other.transform.position = transform.position;
        }
    }
}
