using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : CameraMovement
{
	public float thrust;
	public float thrust1D;

	public float yawTorque;
	public float yaw1D;

	public float pitchTorque;
	public float pitch1D;

	public float rollTorque;
	public float roll1D;

	public override void Init()
	{
		base.Init();
		type = CameraController.CameraMovementType.Spaceship;
	}

	public override bool UpdateMovement()
	{
		if (!base.UpdateMovement())
			return false;

		if(_rigidbody != null)
		{
			// Thrust
			_rigidbody.AddRelativeForce(Vector3.forward * thrust * thrust1D * Time.deltaTime, ForceMode.Force);

			// Roll
			_rigidbody.AddRelativeTorque(Vector3.back * roll1D * rollTorque * Time.deltaTime);

			// Pitch
			_rigidbody.AddRelativeTorque(Vector3.left * pitch1D * pitchTorque * Time.deltaTime);

			// Yaw
			_rigidbody.AddRelativeTorque(Vector3.up * yaw1D * yawTorque * Time.deltaTime);
		}

		return true;
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
