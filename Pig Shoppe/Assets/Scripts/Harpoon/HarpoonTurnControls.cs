using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonTurnControls : MonoBehaviour
{
    public float rotationMultiplier = 1;
    public Camera gameCam;
    [SerializeField]
    private HarpoonShoot shootControls;
    private Vector3 prevMouse = Vector3.zero;
    public void Start()
    {
        prevMouse = Input.mousePosition;
    }
    public void FixedUpdate()
    {
        if (shootControls.isFired)
            return;
        //new Quaternion(0f, Mathf.Sin(angleXDelta / 2f), 0, Mathf.Cos(angleXDelta / 2f)).normalized;

        var angleXDelta = (Input.mousePosition - prevMouse).x * .003f;
        var angleYDelta = (Input.mousePosition - prevMouse).y * .003f;
        prevMouse = Input.mousePosition;
        this.transform.rotation *= new Quaternion(Mathf.Sin(-angleYDelta / 2f), 0f, 0f, Mathf.Cos(-angleYDelta / 2f));
        this.transform.rotation = new Quaternion(0f, Mathf.Sin(angleXDelta / 2f), 0, Mathf.Cos(angleXDelta / 2f)).normalized * this.transform.rotation; //new Quaternion(Mathf.Sin(angleXDelta / 2f) * this.transform.up.x, Mathf.Sin(angleXDelta / 2f) * this.transform.up.y, Mathf.Sin(angleXDelta / 2f) * this.transform.up.z, Mathf.Cos(angleXDelta / 2f)).normalized * this.transform.rotation;
        if (Vector3.Dot(this.transform.forward,new Vector3(this.transform.forward.x,0,this.transform.forward.z).normalized) < .5f)
        {
            //this.transform.rotation *= new Quaternion(Mathf.Sin(-angleYDelta / 2f), 0f, 0f, Mathf.Cos(-angleYDelta / 2f));

        }

        #region Top Down

        //var positionToFace = Vector3.zero;
        //RaycastHit hit;
        //if (Physics.Raycast(gameCam.transform.position,gameCam.ScreenPointToRay(Input.mousePosition).direction, out hit, 50))//add layer for ground only
        //{
        //    positionToFace = new Vector3(hit.point.x, 0, hit.point.z);
        //}
        //else
        //{
        //    return;
        //}
        //var targetRotation = Quaternion.LookRotation(positionToFace.normalized, Vector3.up);
        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.fixedDeltaTime * rotationMultiplier);
        #endregion
    }
}
