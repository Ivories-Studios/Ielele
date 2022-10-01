using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent<PlayerObject> onInteract;

    public void Interact(PlayerObject playerObject)
    {
        onInteract.Invoke(playerObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerObject>().surroundingInteractableObjects.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Player"))
        {
            collision?.GetComponentInParent<PlayerObject>().surroundingInteractableObjects.Remove(this);
        }
    }
}
