using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : CameraMovement
{
	public override void Init()
	{
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
		throw new System.NotImplementedException();
	}
}
