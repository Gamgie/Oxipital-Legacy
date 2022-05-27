using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraRotate : MonoBehaviour
{
    public float rotateYSpeed;
    public float rotateXSpeed;
    public bool controlRotateWithAngle = false;
    public float rotateYAngle;
    public float rotateXAngle;
    public Transform lookAtTarget;
    public Camera camera;
    public float fov;
    public float radius;
    public float resetDuration;
    public float moveToDuration;
    public Vector3 positionTarget;
    public OrbitalDragon orbitalDragon;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer transposer;
    private Camera _cameraFeedback;

    

    private void OnEnable()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _cameraFeedback = camera.transform.GetChild(0).GetComponent<Camera>();
        StopDragon();
    }

    // Update is called once per frame
    void Update()
    {
        // We cannot follow orbital dragon and rotate around center at the same time.
        // Here I disable rotation around center while following dragon.
        if (orbitalDragon.IsActive == false)
        {
            // Rotate target transform according to speed
            if(controlRotateWithAngle == false)
            {
                lookAtTarget.Rotate(rotateXSpeed * Time.deltaTime, rotateYSpeed * Time.deltaTime, 0);
                rotateXAngle = lookAtTarget.eulerAngles.x;
                rotateYAngle = lookAtTarget.eulerAngles.y;
            }
            else // We want to control rotation with angle directly
            {
                lookAtTarget.transform.eulerAngles = new Vector3(rotateXAngle, rotateYAngle, lookAtTarget.eulerAngles.z);
            }

            lookAtTarget.transform.position = positionTarget;
        }
        else // we are in orbital dragon mode
        {
            lookAtTarget.transform.position = new Vector3(lookAtTarget.position.x, positionTarget.y, lookAtTarget.position.z);
        }

        // Control distance between target and camera
        if (transposer != null)
        {
            transposer.m_FollowOffset = new Vector3(0, 0, -radius);
        }

        // Update main camera FOV
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.FieldOfView = fov;
        }

        // update feedback camera FOV
        if(_cameraFeedback != null)
        {
            _cameraFeedback.fieldOfView = fov;
        }
    }

    public void ResetCameraPosition()
    {
        rotateYSpeed = 0;
        rotateXSpeed = 0;
        //rotateXAngle = 0;
        //rotateYAngle = 0;
        lookAtTarget.transform.DOMove(Vector3.zero,resetDuration);
        lookAtTarget.transform.DORotate(Vector3.zero, resetDuration);
        StopDragon();
    }

    public void TopView()
    {
        rotateYSpeed = 0;
        rotateXSpeed = 0;
        lookAtTarget.transform.DORotate(new Vector3(90,0,0), moveToDuration);
    }

    public void DownView()
    {
        rotateYSpeed = 0;
        rotateXSpeed = 0;
        lookAtTarget.transform.DORotate(new Vector3(-90, 0, 0), moveToDuration);
    }

    public void LeftView()
    {
        rotateYSpeed = 0;
        rotateXSpeed = 0;
        lookAtTarget.transform.DORotate(new Vector3(0, 90, 0), moveToDuration);
    }

    public void RightView()
    {
        rotateYSpeed = 0;
        rotateXSpeed = 0;
        lookAtTarget.transform.DORotate(new Vector3(0, -90, 0), moveToDuration);
    }

    public void StartDragon()
    {
        orbitalDragon.Init();
    }

    public void StopDragon()
    {
        orbitalDragon.Stop();
        
    }
}
