using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonTurnControls : MonoBehaviour
{
    public float rotationMultiplier = 1;
    public Camera gameCam;
    [SerializeField]
    private HarpoonShoot shootControls;
    public void FixedUpdate()
    {
        if (shootControls.isFired)
            return;
        var positionToFace = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(gameCam.transform.position,gameCam.ScreenPointToRay(Input.mousePosition).direction, out hit, 50))//add layer for ground only
        {
            positionToFace = new Vector3(hit.point.x, 0, hit.point.z);
        }
        else
        {
            return;
        }
        var targetRotation = Quaternion.LookRotation(positionToFace.normalized, Vector3.up);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.fixedDeltaTime * rotationMultiplier);
    }
}
