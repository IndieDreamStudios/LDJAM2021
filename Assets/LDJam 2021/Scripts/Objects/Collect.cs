using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour, IInteractable
{
    public CollectableTypes collectType;

    public void Interact()
    {
        Debug.LogWarning("Collected");

        // Add Key to collected items
        Manager.Instance.Player.inventory.AddCollectable(collectType);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact();
        }
    }
}
