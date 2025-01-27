using System;
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

    private void Start()
    {
        Interactable.InteractableDestroyed += TriggerInteractableExitEvent;
    }

    private void OnDisable()
    {
        Interactable.InteractableDestroyed -= TriggerInteractableExitEvent;
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            //Debug.Log($"Interactable '{other.name}' detected.");
            currentInteractable = interactable;
            OnInteractableDetected?.Invoke(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            TriggerInteractableExitEvent(interactable);
        }
    }

    private void Update()
    {
        // Handle interaction input (e.g., pressing 'F')
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.F))
        {
            currentInteractable.OnInteract();
        }
    }

    private void TriggerInteractableExitEvent(Interactable interactable)
    {
        if (interactable == currentInteractable)
        {
            OnInteractableExited?.Invoke(currentInteractable);
            currentInteractable = null;
        }
    }
}