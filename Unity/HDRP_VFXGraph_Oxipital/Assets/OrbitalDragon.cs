using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalDragon : MonoBehaviour
{
    public float radius;
    public float velocity;

    private Vector3 _oldPosition;
    private Vector3 _velocityDirection;
    private float _lerpedRadius;

    // Start is called before the first frame update
    void Start()
    {
        _oldPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _lerpedRadius = Mathf.Lerp(_lerpedRadius, radius, 0.5f);

        // Compute position according to radius    
        transform.position = _lerpedRadius * Vector3.Normalize(this.transform.position - Vector3.zero);

        // Rotate around center
        transform.RotateAround(Vector3.zero, Vector3.down, velocity * Time.deltaTime);

        _velocityDirection = transform.position - _oldPosition;
        transform.rotation = Quaternion.LookRotation(_velocityDirection, Vector3.up);

        //update old position
        _oldPosition = transform.position;
    }
}
