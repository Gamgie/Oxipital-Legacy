using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMovement : CameraMovement
{
	[Range(0, 100)]
	public float thrust;
	[Range(-1,1)]
	public float thrust1D;

	[Range(0, 100)]
	public float yawTorque;
	[Range(-1, 1)]
	public float yaw1D;

	[Range(0, 100)]
	public float pitchTorque;
	[Range(-1, 1)]
	public float pitch1D;

	[Range(0, 100)]
	public float rollTorque;
	[Range(-1, 1)]
	public float roll1D;

	[Header("Physics Parameters")]
	[Range(0, 5)]
	public float directionnalDrag;
	[Range(0, 5)]
	public float angularDrag;

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


			_rigidbody.drag = directionnalDrag;
			_rigidbody.angularDrag= angularDrag;
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
		base.SetActive(activate);
		_rigidbody.isKinematic = !activate;
	}

	public SpaceshipMovementData StoreData()
	{
		SpaceshipMovementData data = new SpaceshipMovementData();

		data.thrust = thrust;
		data.yawTorque = yawTorque;
		data.pitchTorque = pitchTorque;
		data.rollTorque = rollTorque;
		data.directionnalDrag = directionnalDrag;
		data.angularDrag = angularDrag;

		return data;
	}

	public void LoadData(SpaceshipMovementData data)
	{
		thrust = data.thrust;
		yawTorque = data.yawTorque;
		pitchTorque = data.pitchTorque;
		rollTorque = data.rollTorque;
		directionnalDrag = data.directionnalDrag;
		angularDrag = data.angularDrag;
	}
}

[System.Serializable]
public class SpaceshipMovementData
{
	public float thrust;
	public float yawTorque;
	public float pitchTorque;
	public float rollTorque;
	public float directionnalDrag;
	public float angularDrag;
}