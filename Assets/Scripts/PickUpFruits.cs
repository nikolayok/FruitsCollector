using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickUpFruits : MonoBehaviour
{
    [SerializeField] private Transform _rightHandControllerTransform;
    private Transform _targetFruit;
    private float _rightHandSpeed = 3;
    private bool _isPickingUp = false;
    private bool _isPickedUp = false;
    private bool _isMovingHandToDefaultPosition = false;
    private const bool _ableToPickUp = true;

    [SerializeField] private FruitsCollector _fruitsCollector;
    [SerializeField] private Transform _putIntoBasket;
    [SerializeField] private Transform _rightHandDefaultTransform;

    const int _collectedFruitLayer = 9; // spawned object layer

    public event Action<bool> PickUpEvent;

    private void Update()
    {
        PickingUp();
        PutFruitIntoBasket();
        MoveHandToDefaultPosition();
    }

    private void OnEnable()
    {
        _fruitsCollector.PickUpEvent += StartPickUpFruit;
    }

    private void OnDisable()
    {
        _fruitsCollector.PickUpEvent -= StartPickUpFruit;
    }

    private void StartPickUpFruit(Transform targetFruit)
    {
        _targetFruit = targetFruit;
        _isPickingUp = true;
        _targetFruit.tag = "CollectedFruit";
    }

    private void PickingUp()
    {
        if (MoveRightHand(_isPickingUp, _rightHandControllerTransform, _targetFruit, true)) // is finished moving, true is isPickingUpFruit
        {
            _isPickingUp = false;
            _isPickedUp = true;
            PickUpFruit();
        }
    }

    private void PickUpFruit()
    {
        _targetFruit.parent = _rightHandControllerTransform;
        _targetFruit.gameObject.layer = _collectedFruitLayer;
        _targetFruit.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void PutFruitIntoBasket()
    {
        if (MoveRightHand(_isPickedUp, _rightHandControllerTransform, _putIntoBasket))
        {
            _isPickedUp = false;
            _isMovingHandToDefaultPosition = true;
            _targetFruit.parent = transform.parent;
            _targetFruit.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void MoveHandToDefaultPosition()
    {
        if (MoveRightHand(_isMovingHandToDefaultPosition, _rightHandControllerTransform, _rightHandDefaultTransform))
        {
            _isMovingHandToDefaultPosition = false;
            AllowPickUp();
        }
    }

    private void AllowPickUp()
    {
         PickUpEvent?.Invoke(_ableToPickUp);
    }

    private bool MoveRightHand(bool condition, Transform startTransform, Transform endTransform, bool isPickingUpFruit = false)
    {
        if ( ! condition)
        {
            return false;
        }

        Vector3 startPosition = startTransform.position;
        Vector3 endPosition = endTransform.position;

        if (isPickingUpFruit)
        {
            endPosition.y = endPosition.y + 0.14f; // position above fruit
        }

        startTransform.Translate((endPosition - startPosition).normalized * _rightHandSpeed * Time.deltaTime, Space.World);
        if (Vector3.Distance(startTransform.position, endTransform.position) <= 0.15f)
        {
            return true;
        }

        return false;
    }
}
