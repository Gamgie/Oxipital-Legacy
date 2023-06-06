using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class CameraMovement : MonoBehaviour
{
	public bool isActive;
	public CinemachineVirtualCamera virtualCamera;
	public CameraController.CameraMovementType type;

	public abstract void Init();

	public abstract void Reset(float duration);

	public virtual bool UpdateMovement()
	{
		// Do not move our object if we are not the active camera
		if (!isActive)
			return false;

		return true;
	}

	public abstract void UpdateFOV(float fov);

	public abstract void UpdateZOffset(float offset);

}
