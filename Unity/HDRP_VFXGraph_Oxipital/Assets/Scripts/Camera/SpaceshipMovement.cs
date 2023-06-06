using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : CameraMovement
{
	public override void Init()
	{
		base.Init();
		type = CameraController.CameraMovementType.Spaceship;
	}

	public override bool UpdateMovement()
	{
		return base.UpdateMovement();
	}

	public override void Reset(float duration)
	{

	}

	public override void UpdateFOV(float fov)
	{
		virtualCamera.m_Lens.FieldOfView = fov;
	}

	public override void UpdateZOffset(float offset)
	{
	}

	public override void SetActive(bool activate)
	{
		// no need to update if they are equal
		if (activate == _isActive)
			return;

		base.SetActive(activate);
		_rigidbody.isKinematic = !activate;
	}
}
