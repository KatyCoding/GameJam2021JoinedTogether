using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class HarpoonShoot : MonoBehaviour
{
    public Transform spear;
    [Range(1, 50)]
    public float speedMultiplier;
    [Range(1,20)]
    public float maxLength = 10;
    [SerializeField]
    private PositionConstraint positionConstraint;
    public bool isFired { get; private set; }
    bool hasHitSomething = false;
    bool mouseDown = false;
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
            spear.transform.position -= this.transform.forward * speedMultiplier * .02f;
            if ((spear.transform.position - this.transform.position).magnitude < .1f)
            {
                isFired = false;
                positionConstraint.constraintActive = true;
                hasHitSomething = false;
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


    public void ObjectWasHit()
    {
        hasHitSomething = true;
    }
}
