using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
[RequireComponent(typeof(PositionConstraint))]
public class Piggy : MonoBehaviour, IGrabbable
{
    [Header("Grabbable Related Fields")]
    public AudioClip OnGrabbedAudio;
    public string OnGrabbedAnimationTrigger;
    public PositionConstraint harpoonGrabbedConstraint;
    public void OnArrive()
    {
        harpoonGrabbedConstraint.RemoveSource(0);
        harpoonGrabbedConstraint.constraintActive = false;
        this.gameObject.SetActive(false);
    }

    public void OnDragStart()
    {
    }

    public void OnGrabbed(Transform grabbingObject)
    {
        //playAudio
        //triggeranimation
        ConstraintSource cs = new ConstraintSource();
        cs.sourceTransform = grabbingObject;
        cs.weight = 1;
        harpoonGrabbedConstraint.AddSource(cs);
        harpoonGrabbedConstraint.constraintActive = true;
    }

    public void WhileDragging()
    {
    }

    
}
