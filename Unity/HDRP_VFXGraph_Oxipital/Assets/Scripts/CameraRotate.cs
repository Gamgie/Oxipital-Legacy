using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotateYSpeed;
    public float rotateXSpeed;
    public float rotateRadius;
    public Vector3 rotationCenter;
    public Transform lookAtTarget;
    public Camera camera;
    public Transform cameraPositionTarget; // Camera follow this object. Allow me to smoothly move camera behind this target.


    // Update is called once per frame
    void Update()
    {
        transform.position = rotationCenter;

        // Rotate this transform. This give the rotation to camera target
        transform.Rotate(rotateXSpeed * Time.deltaTime, rotateYSpeed * Time.deltaTime, 0);

        // Update position of target in Z. It is rotation's radius.
        cameraPositionTarget.transform.localPosition = new Vector3(0, 0, rotateRadius);

        // Update camera position smoothly
        camera.transform.LookAt(lookAtTarget);
        camera.transform.position = Vector3.Lerp(camera.transform.position,cameraPositionTarget.position,0.1f);

    }

    public void ResetPosition()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        rotateYSpeed = 0;
        rotateXSpeed = 0;
    }
}
