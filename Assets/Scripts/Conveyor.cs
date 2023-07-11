using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _speed = 0.25f;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _distanceBeforeTeleportToStartPosition = 0.001f;
    private Vector3 _velocity;

    private Material _conveyorMaterial;
    Vector2 _offset = Vector2.zero;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.mass = 1000;
        _transform = transform;
        _startPosition = _transform.position;
        _endPosition = _transform.position;
        _endPosition.z += _distanceBeforeTeleportToStartPosition;

        _velocity.z = _speed;
        _conveyorMaterial = GetComponent<Renderer>().material;
        _offset = _conveyorMaterial.mainTextureOffset;
    }

    private void FixedUpdate()
    {
        MoveConveyor();
        MoveConveyorTexture();
    }

    private void MoveConveyor()
    {
        _rigidbody.velocity = _velocity;

        if (_transform.position.z >= _endPosition.z)
        {
            _transform.position = _startPosition;
        }
    }

    private void MoveConveyorTexture()
    {
        _offset.y += _speed * Time.fixedDeltaTime * 3;//1.5f;// / 1.5f;
        _conveyorMaterial.mainTextureOffset = _offset;

        if (_offset.y >= 10000)
        {
            _offset.y = 0;
        }
    }
}
