using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public enum CameraMovementType { Orbital, Spaceship, Freefly }

    [Header("General Parameters")]
    public Transform lookAtTarget;
    public new Camera camera;
    public float fov;
    public float followZOffset;
    public float resetDuration;
    public CameraMovementType cameraType;

    public OrbitalMovement orbitalCamera;
    public SpaceshipMovement spaceshipCamera;
    //public CinemachineVirtualCamera freeflyCamera;

    public bool showXwing;
    public GameObject xWing;
    
    private Camera _cameraFeedback;
    private List<CameraMovement> _cameraList;
    private CameraMovement _activeCamera;

    private void OnEnable()
    {
        _cameraFeedback = camera.transform.GetChild(0).GetComponent<Camera>();

        _cameraList = new List<CameraMovement>();
        _cameraList.Add(orbitalCamera);
        _cameraList.Add(spaceshipCamera);

        foreach (CameraMovement c in _cameraList)
        {
            c.Init();
        }

        SwitchCamera(cameraType);

        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        if(_activeCamera != null && cameraType != _activeCamera.type)
		{
            SwitchCamera(cameraType);
		}

        // update feedback camera FOV
        if (_cameraFeedback != null)
        {
            _cameraFeedback.fieldOfView = fov;
        }

        xWing.SetActive(showXwing);
    }

    // Movement need physics so we need to update 
	private void FixedUpdate()
	{
        // Update our active camera
        foreach (CameraMovement c in _cameraList)
        {
            if (c != null && c.IsActive)
            {
                c.UpdateMovement();
                c.UpdateZOffset(followZOffset);
                c.UpdateFOV(fov);
            }
        }
    }

	void SwitchCamera(CameraMovementType type)
	{
        // Deactivate all cameras
        foreach(CameraMovement c in _cameraList)
		{
            c.SetActive(false);
		}

        // Activate the selected camera
        switch (type)
		{
            case CameraMovementType.Freefly:
                break;
            case CameraMovementType.Orbital:
                _activeCamera = orbitalCamera;

                break;
            case CameraMovementType.Spaceship:
                _activeCamera = spaceshipCamera;
                break;
        }

        _activeCamera.SetActive(true);
    }

    public void ResetCameraPosition()
    {
        foreach (CameraMovement c in _cameraList)
        {
            if (c.IsActive)
                c.Reset(resetDuration);
        }
    }

    public CameraControllerData StoreData()
	{
        CameraControllerData result = new CameraControllerData();

        result.fov = fov;
        result.followZOffset = followZOffset;
        result.resetDuration = resetDuration;
        result.cameraType = (int)cameraType;
        result.showXWing = showXwing;

        return result;
    }

    public void LoadData()
	{
        // Find Data manager and load its data.
        DataManager dataMngr = GameObject.FindGameObjectWithTag("Data Manager").GetComponent<DataManager>();
        OxipitalData data = dataMngr.LoadData();

        // Update cameraController parameters with this data
        fov = data.cameraControllerData.fov;
        followZOffset = data.cameraControllerData.followZOffset;
        resetDuration = data.cameraControllerData.resetDuration;
        cameraType = (CameraMovementType) data.cameraControllerData.cameraType;
        showXwing = data.cameraControllerData.showXWing;

        // Then update each camera parameters
        spaceshipCamera.LoadData(data.spaceshipMovementData);
        orbitalCamera.LoadData(data.orbitalMovementData);
    }

    public List<CameraMovement> GetCameraList()
	{
        return _cameraList;
	}
}

[System.Serializable]
public class CameraControllerData
{
    public float fov;
    public float followZOffset;
    public float resetDuration;
    public int cameraType;
    public bool showXWing;
}