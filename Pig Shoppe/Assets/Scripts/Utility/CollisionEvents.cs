using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionEvents : MonoBehaviour
{
    public List<MonoBehaviour> onCollisionEnterEventListeners;
    private void OnCollisionEnter(Collision collision)
    {
        for(int i = 0;i<onCollisionEnterEventListeners.Count;i++)
        {
            onCollisionEnterEventListeners[i].SendMessage("CollisionEventListener", collision);
        }
    }
}
