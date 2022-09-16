using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderContainer : MonoBehaviour
{
    private HashSet<Collider2D> colliders = new HashSet<Collider2D>();
    [SerializeField] private string TAG = "Enemy";

    public HashSet<Collider2D> GetColliders() { return colliders; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TAG))
        {
            colliders.Add(collision); //hashset automatically handles duplicates
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(TAG))
        {
            colliders.Remove(other);
        }
    }
}
