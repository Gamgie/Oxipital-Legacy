using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbitalDragon : MonoBehaviour
{
    
    public float radius;
    public float velocity;
    public float dampSmoothTime = 0.3f;
    public bool showDebugSphere;
    public GameObject debugSphere;

    private bool _isActive = false;
    private Vector3 _oldPosition;
    private Vector3 _velocityDirection;
    private Vector3 _dampVelocity = Vector3.zero;
    private float _dampedRadiusVelocity;
    private float _smoothRadius;

    public bool IsActive { get => _isActive; set => _isActive = value; }


    // Start is called before the first frame update
    void Start()
    {
        _oldPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        debugSphere.SetActive(showDebugSphere);

        if (!IsActive)
        {
            return;
        }

        _smoothRadius = radius;
        FollowOrbital();
    }

    void OrientAlongVelocity()
    {
        // velocity direction
        Vector3 actualDirection = transform.position - _oldPosition;

        // damped the actual velocity direction to the old one 
        _velocityDirection = Vector3.SmoothDamp(_velocityDirection, actualDirection, ref _dampVelocity, dampSmoothTime);

        // rotate accordingly
        transform.rotation = Quaternion.LookRotation(_velocityDirection, Vector3.up);
    }

    public void Init()
    {
        if(!IsActive)
        {
            transform.position = new Vector3(0.1f, 0, 0);
            IsActive = true;
        }
    }

    public void Stop()
    {
        transform.DOMove(Vector3.zero, 5.0f);
        IsActive = false;
    }

    void FollowOrbital()
    {
        // Compute position according to radius    
        transform.position = _smoothRadius * Vector3.Normalize(this.transform.position - Vector3.zero);

        // Rotate around center
        transform.RotateAround(Vector3.zero, Vector3.down, velocity * Time.deltaTime);

        OrientAlongVelocity();

        //update old position
        _oldPosition = transform.position;
    }

    IEnumerator InitSequence()
    {
        _smoothRadius = 0;
        while(true)
        {
            _smoothRadius = Mathf.SmoothDamp(_smoothRadius, radius, ref _dampedRadiusVelocity, 0.1f,0.1f);
            FollowOrbital();

            if (radius - _smoothRadius < 0.1f)
                break;

            yield return new WaitForEndOfFrame();
        }
        
        IsActive = true;
    }
}
