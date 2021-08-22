using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraRotate : MonoBehaviour
{
    public float rotateYSpeed;
    public float rotateXSpeed;
    public float rotateRadius;
    public Vector3 rotationCenter;
    public Transform lookAtTarget;
    public Camera camera;
    public Transform cameraPositionTarget; // Camera follow this object. Allow me to smoothly move camera behind this target.
    public float resetDuration;
    public float XRotationValue;


    // Update is called once per frame
    void Update()
    {
        transform.position = rotationCenter;

        // Rotate this transform. This give the rotation to camera target
        //transform.Rotate(XRotationValue, rotateYSpeed * Time.deltaTime, 0);
        //transform.rotation.eulerAngles.x = XRotationValue;
        transform.rotation = Quaternion.Euler(XRotationValue, 0, 0);

        // Update position of target in Z. It is rotation's radius.
        cameraPositionTarget.transform.localPosition = new Vector3(0, 0, rotateRadius);

        // Update camera position smoothly
        Debug.Log(transform.localEulerAngles.x);

        if(XRotationValue < 90 && XRotationValue > -90)
        {
            camera.transform.LookAt(lookAtTarget, Vector3.up);
            Debug.Log("up");
        }
        else
        {
            camera.transform.LookAt(lookAtTarget, Vector3.down);
            Debug.Log("down");
        }
        //camera.transform.LookAt(lookAtTarget);
        //camera.transform.rotation = Quaternion.LookRotation(lookAtTarget.position - camera.transform.position);
        camera.transform.position = cameraPositionTarget.position;

    }
}
