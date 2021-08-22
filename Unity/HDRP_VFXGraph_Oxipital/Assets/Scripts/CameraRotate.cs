using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraRotate : MonoBehaviour
{
    public float rotateYSpeed;
    public float rotateXSpeed;
    public Transform lookAtTarget;
    public Camera camera;
    public float radius;
    public float resetDuration;
    public float moveDuration;
    public Vector3 positionTarget;
    public OrbitalDragon orbitalDragon;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer transposer;

    private void OnEnable()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        // We cannot follow orbital dragon and rotate around center at the same time.
        // Here I disable rotation around center while following dragon.
        if (orbitalDragon.IsActive == false)
        {
            // Rotate target transform.
            lookAtTarget.Rotate(rotateXSpeed * Time.deltaTime, rotateYSpeed * Time.deltaTime, 0);
            if (transposer != null)
            {
                transposer.m_FollowOffset = new Vector3(0,0, -radius);
            }
        }
    }

    public void ResetCameraPosition()
    {
        rotateYSpeed = 0;
        rotateXSpeed = 0;
        lookAtTarget.transform.DOMove(Vector3.zero,resetDuration);
        lookAtTarget.transform.DORotate(Vector3.zero, resetDuration);
    }

    public void TopView()
    {
        rotateYSpeed = 0;
        rotateXSpeed = 0;
        lookAtTarget.transform.DORotate(new Vector3(90,0,0), moveDuration);
    }

    public void DownView()
    {
        rotateYSpeed = 0;
        rotateXSpeed = 0;
        lookAtTarget.transform.DORotate(new Vector3(-90, 0, 0), moveDuration);
    }

    public void LeftView()
    {
        rotateYSpeed = 0;
        rotateXSpeed = 0;
        lookAtTarget.transform.DORotate(new Vector3(0, 90, 0), moveDuration);
    }

    public void RightView()
    {
        rotateYSpeed = 0;
        rotateXSpeed = 0;
        lookAtTarget.transform.DORotate(new Vector3(0, -90, 0), moveDuration);
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
