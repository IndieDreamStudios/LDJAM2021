using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Collider Inside the player / Attack range")]
    public ColliderContainer collider;

    public void SetColliderPosition(Vector3 position)
    {
        collider.transform.position = position;
    }

    public GameObject GetTarget()
    {
        GameObject result = null;
        float lastDistance = float.MaxValue;
        foreach (var obj in collider.GetColliders())
        {
            if ( Vector3.Distance(obj.ClosestPoint(transform.position), transform.position) < lastDistance)
            {
                lastDistance = Vector3.Distance(obj.ClosestPoint(transform.position), transform.position);
                result = obj.gameObject;
            }
        }
        return result;
    }
}
