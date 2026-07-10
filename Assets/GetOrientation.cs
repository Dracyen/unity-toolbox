using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOrientation : MonoBehaviour
{
    [SerializeField] public SphereCollider FLWheelCollider;
    [SerializeField] public SphereCollider FRWheelCollider;
    [SerializeField] public SphereCollider BLWheelCollider;
    [SerializeField] public LayerMask SpawnTargetsLayerMask;

    public void SetOneOrientation()
    {
        //ResetOrientation();

        RaycastHit FLWheelHit = GetWheelHit(FLWheelCollider.transform.position);
        RaycastHit BLWheelHit = GetWheelHit(BLWheelCollider.transform.position);

        Vector3 a = BLWheelHit.point - FLWheelHit.point;

        Vector3 directional1 = a.normalized;

        Debug.Log("1/ Direction: " + directional1);

        transform.forward = directional1;
    }

    public void SetTwoOrientation()
    {
        //ResetOrientation();

        RaycastHit FLWheelHit = GetWheelHit(FLWheelCollider.transform.position);
        RaycastHit BLWheelHit = GetWheelHit(BLWheelCollider.transform.position);
        RaycastHit FRWheelHit = GetWheelHit(FRWheelCollider.transform.position);

        Vector3 forwardDirection = BLWheelHit.point - FLWheelHit.point;

        Vector3 forwardNormalized = forwardDirection.normalized;

        Vector3 rightDirection = FLWheelHit.point - FRWheelHit.point;

        Vector3 rightNormalized = rightDirection.normalized;

        Vector3 downNormalized = Vector3.Cross(rightNormalized, forwardNormalized);

        Quaternion targetRotation = Quaternion.LookRotation(forwardNormalized, -downNormalized);

        transform.rotation = targetRotation;
    }

    public void NewSetOrientation()
    {
        ///*We want to save our current transform.up vector so we can smoothly change it later*/
        //var prev_up = transform.up;
        ///*Now we set all angles to zero except for the Y which corresponds to the Yaw*/
        //transform.rotation = Quaternion.Euler(0, yaw, 0);

        ///*Here are the meat and potatoes: first we calculate the new up vector for the ship using lerp so that it is smoothed*/
        //Vector3 desired_up = Vector3.Lerp(prev_up, hit.normal, Time.deltaTime * pitch_smooth);
        ///*Then we get the angle that we have to rotate in quaternion format*/
        //Quaternion tilt = Quaternion.FromToRotation(transform.up, desired_up);
        ///*Now we apply it to the ship with the quaternion product property*/
        //transform.rotation = tilt * transform.rotation;

        ///*Smoothly adjust our height*/
        //smooth_y = Mathf.Lerp(smooth_y, hover_height - hit.distance, Time.deltaTime * height_smooth);
        //transform.localPosition += prev_up * smooth_y;
    }

    public void ResetOrientation()
    {
        transform.forward = Vector3.forward;
    }

    private RaycastHit GetWheelHit(Vector3 pos)
    {
        Ray ray;

        RaycastHit hit;

        ray = new Ray(pos, Vector3.down);

        Physics.Raycast(ray, out hit, 50, SpawnTargetsLayerMask);

        return hit;
    }
}
