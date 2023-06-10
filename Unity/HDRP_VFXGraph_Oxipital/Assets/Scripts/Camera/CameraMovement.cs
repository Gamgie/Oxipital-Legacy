using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class CameraMovement : MonoBehaviour
{
	protected bool _isActive;
	public CinemachineVirtualCamera virtualCamera;
	public CameraController.CameraMovementType type;

	protected Rigidbody _rigidbody;

	public bool IsActive { get => _isActive; }

	public virtual void Init()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	public abstract void Reset(float duration);

	public virtual bool UpdateMovement()
	{
		// Do not move our object if we are not the active camera
		if (!_isActive)
			return false;

		return true;
	}

	public abstract void UpdateFOV(float fov);

	public abstract void UpdateZOffset(float offset);

	public virtual void SetActive(bool activate)
	{
		_isActive = activate;

		if (_isActive)
		{
			virtualCamera.Priority = 10;
		}
		else
		{
			virtualCamera.Priority = 0;
		}
	}
}