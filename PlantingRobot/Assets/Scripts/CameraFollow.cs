using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float minDistance = 25;
    public float maxDistance = 60;
    public float offsetLength = 1f;
    public float movementSpeed = 4;

    void LateUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0) {
            offsetLength -= Input.GetAxis("Mouse ScrollWheel");
            if((offset * offsetLength).magnitude < minDistance) {
                offsetLength = minDistance / offset.magnitude;
            }

            if ((offset * offsetLength).magnitude > maxDistance) {
                offsetLength = maxDistance / offset.magnitude;
            }
        }

        //transform.LookAt(target);
        transform.position = (target.position + offset * offsetLength);
        //Vector3.Lerp(transform.position, (target.position + offset * offsetLength), Time.deltaTime * movementSpeed);
    }
}
