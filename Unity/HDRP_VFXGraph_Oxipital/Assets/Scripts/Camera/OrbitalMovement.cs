using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class OrbitalMovement : CameraMovement
{
	public float rotateYSpeed;
	public float rotateXSpeed;
	public float rotateZSpeed;
	[Space()]
	public bool controlRotateWithAngle = false;
	public float rotateYAngle;
	public float rotateXAngle;
	public float rotateZAngle;
	[Space()]
	public Vector3 positionTarget;
	public float moveToDuration;

	private CinemachineTransposer transposer;

	public override void Init()
	{
		transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
		type = CameraController.CameraMovementType.Orbital;
		isActive = false;
	}

	public override bool UpdateMovement()
	{
		// our camera is not active so leave now.
		if (!base.UpdateMovement())
			return false;

		// Rotate target transform according to speed
		if (controlRotateWithAngle == false)
		{
			transform.Rotate(rotateXSpeed * Time.deltaTime, rotateYSpeed * Time.deltaTime, rotateZSpeed * Time.deltaTime);
			rotateXAngle = transform.eulerAngles.x;
			rotateYAngle = transform.eulerAngles.y;
			rotateZAngle = transform.eulerAngles.z;
		}
		else // We want to control rotation with angle directly
		{
			transform.eulerAngles = new Vector3(rotateXAngle, rotateYAngle, rotateZAngle);
		}

		transform.position = positionTarget;
		
		return true;
	}

	public override void Reset(float duration)
	{
		rotateYSpeed = 0;
		rotateXSpeed = 0;
		rotateZSpeed = 0;
		transform.DOMove(Vector3.zero, duration);
		transform.DORotate(Vector3.zero, duration);
	}

	public override void UpdateFOV(float fov)
	{
		virtualCamera.m_Lens.FieldOfView = fov;
	}

	public override void UpdateZOffset(float offset)
	{
		// Control distance between target and camera
		if (transposer != null)
		{
			transposer.m_FollowOffset = new Vector3(0, 0, -offset);
		}
	}

	public void TopView()
	{
		rotateYSpeed = 0;
		rotateXSpeed = 0;
		transform.DORotate(new Vector3(90, 0, 0), moveToDuration);
	}

	public void DownView()
	{
		rotateYSpeed = 0;
		rotateXSpeed = 0;
		transform.DORotate(new Vector3(-90, 0, 0), moveToDuration);
	}

	public void LeftView()
	{
		rotateYSpeed = 0;
		rotateXSpeed = 0;
		transform.DORotate(new Vector3(0, 90, 0), moveToDuration);
	}

	public void RightView()
	{
		rotateYSpeed = 0;
		rotateXSpeed = 0;
		transform.DORotate(new Vector3(0, -90, 0), moveToDuration);
	}

	public void FrontView()
	{
		rotateYSpeed = 0;
		rotateXSpeed = 0;
		transform.transform.DORotate(new Vector3(0, 0, 0), moveToDuration);
	}
}
