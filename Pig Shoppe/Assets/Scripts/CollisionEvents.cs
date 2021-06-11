using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionEvents : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onCollisionEnterEvents;
    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnterEvents?.Invoke();
    }
}
