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

    [Header("Pathfinding")]
    public float maxSpeed = 2;
    [Range(0, 1f)]
    public float chanceToSit = .1f;
    public LayerMask pigPathfindingLayer;
    public float pigSitMinTime = 1;
    public float pigSitMaxTime = 6;
    
    [SerializeField]
    private Rigidbody pigsRigidbody;
    #region private pathfinding fields
    Vector3 acceleration = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    float sitDownTimer = 0;
    float sitDownTimeLength;
    bool isSitting = false;
    #endregion
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
        maxSpeed = 0;
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
    public void Awake()
    {
        acceleration = new Vector3(Random.value - .5f, 0, Random.value - .5f);
    }
    public void Update()
    {

    }
    public void FixedUpdate()
    {
        HandlePathfinding();
        this.transform.up = velocity.normalized;
    }

    protected virtual void HandlePathfinding()
    {
        pigsRigidbody.velocity = Vector3.zero;
        float r = Random.Range(0f, 100f);
        if(r < chanceToSit)
        {
            isSitting = true;
            sitDownTimeLength = Random.Range(pigSitMinTime, pigSitMaxTime);
            sitDownTimer = 0;
            Debug.Log("Sitting");
        }
        if(isSitting && sitDownTimer < sitDownTimeLength)
        {
            velocity = Vector3.zero;
            sitDownTimer += Time.fixedDeltaTime;
            return;
        }
        isSitting = false;
        var cols = Physics.OverlapSphere(this.transform.position, 3);
        var pigsNearby = new List<Piggy>();
        for (int i = 0; i < cols.Length; i++)
        {
            var pig = cols[i].GetComponent<Piggy>();
            if (pig)
            {
                pigsNearby.Add(pig);
                acceleration += -(pig.transform.position - this.transform.position).normalized * .5f;
            }
        }
        acceleration += new Vector3(Random.value - .5f, 0, Random.value - .5f) * .4f;
        acceleration = acceleration.normalized;
        velocity += acceleration * Time.fixedDeltaTime;
        if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        velocity.y = 0;
        transform.position += velocity * Time.fixedDeltaTime;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (1<<collision.collider.gameObject.layer == pigPathfindingLayer.value)
        {
            acceleration = Vector3.zero;
            var newX = collision.GetContact(0).normal.x;
            var newZ = collision.GetContact(0).normal.z;
            velocity = new Vector3(newX, 0, newZ);
        }
    }
}
