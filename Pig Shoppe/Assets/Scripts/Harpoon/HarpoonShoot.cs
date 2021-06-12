using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class HarpoonShoot : MonoBehaviour,ICollisionEventListener
{
    public Transform spear;
    [SerializeField]
    private Rigidbody spearRigidbody;
    public Transform constraintTransform;
    [Range(1, 50)]
    public float speedMultiplier;
    [Range(1,20)]
    public float maxLength = 10;
    [SerializeField]
    private PositionConstraint positionConstraint;
    [SerializeField]
    private float pauseTimeBetweenHitAndStartDrag = .5f;
    public bool isFired { get; private set; }
    private IGrabbable grabbedObject = null;
    bool hasHitSomething = false;
    bool mouseDown = false;
    bool dragStarted = false;   
    float pauseHitAndDragTimer = 0;
    Vector3 launcPositionCache;
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFired && !mouseDown)
        {
            mouseDown = true;            
        }
    }
    void FixedUpdate()
    {
        if (mouseDown && !isFired)
        {
            isFired = true;
            mouseDown = false;
            launcPositionCache = spear.transform.position;
            positionConstraint.constraintActive = false;
        }
        if (isFired)
        {
            HandleFiredSpear();
        }
    }
    void HandleFiredSpear()
    {
        if (hasHitSomething)
        {
            if(pauseHitAndDragTimer < pauseTimeBetweenHitAndStartDrag && !dragStarted)
            {
                spearRigidbody.isKinematic = true;
                pauseHitAndDragTimer += Time.deltaTime;
                return;
            }
            else if(!dragStarted)
            {
                pauseHitAndDragTimer = 0;
                spearRigidbody.isKinematic = false;
                dragStarted = true;
                grabbedObject?.OnDragStart();

            }
            grabbedObject?.WhileDragging();
            spear.transform.position +=(launcPositionCache-spear.transform.position).normalized * speedMultiplier * .02f;
            if ((spear.transform.position - launcPositionCache).magnitude < .1f)
            {
                isFired = false;
                dragStarted = false;
                positionConstraint.constraintActive = true;
                hasHitSomething = false;
                grabbedObject?.OnArrive();
            }
        }
        else
        {
            
            spear.transform.position += this.transform.forward * speedMultiplier * .05f;
            if ((spear.transform.position - this.transform.position).magnitude > maxLength)
            {
                hasHitSomething = true;
            }
            
        }
    }

    public void ObjectWasHit(GameObject hitObject)
    {
        var grabbable = (hitObject).GetComponent<IGrabbable>();
        hasHitSomething = true;
        if (grabbable != null)
        {
            grabbedObject = grabbable;
            grabbedObject.OnGrabbed(constraintTransform);
        }
    }

    public void CollisionEventListener(Collision collision)
    {
        constraintTransform.position = collision.collider.transform.position;// - spear.transform.position;
        ObjectWasHit(collision.collider.gameObject);
    }
}
