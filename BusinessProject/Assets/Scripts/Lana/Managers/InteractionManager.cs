using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager interactionManager { get; private set; }

    // Events to notify other systems about interactable detection
    public delegate void InteractableDetected(Interactable interactable);
    public event InteractableDetected OnInteractableDetected;
    public event InteractableDetected OnInteractableExited;

    private Interactable currentInteractable;

    private void Awake()
    {
        //Singleton
        if (interactionManager == null) interactionManager = this;
        else Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            OnInteractableDetected?.Invoke(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable == currentInteractable)
        {
            OnInteractableExited?.Invoke(interactable);
            currentInteractable = null;
        }
    }

    private void Update()
    {
        // Handle interaction input (e.g., pressing 'E')
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.OnInteract();
            OnInteractableExited?.Invoke(currentInteractable);
            currentInteractable = null;
        }
    }
}