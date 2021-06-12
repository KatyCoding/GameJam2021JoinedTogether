using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public interface IGrabbable
{
    public void OnGrabbed(Transform grabbingObject);
    public void OnDragStart();
    public void WhileDragging();
    public void OnArrive();
}
