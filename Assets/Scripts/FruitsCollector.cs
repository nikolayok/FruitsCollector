using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FruitsCollector : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    const int _spawnedObjectLayer = 7; // spawned object layer
    int _spawnedObjectLayerMask = 0;  // set in start function

    public event Action<Transform> PickUpEvent;
    public event Action<string> PickUpSpecificFruitEvent;

    [SerializeField] private PickUpFruits _pickUpFruits;
    private bool _isAbleToPickUp = true;

    private void Start()
    {
        _spawnedObjectLayerMask = 1 << _spawnedObjectLayer; // Bit shift the index of the layer (layer) to get a bit mask
    }

    private void Update()
    {
        if (_isAbleToPickUp)
        {
            GetInput();
        }
    }

    private void OnEnable()
    {
        _pickUpFruits.PickUpEvent += SetIsAbleToPickUp;
    }

    private void OnDisable()
    {
        _pickUpFruits.PickUpEvent -= SetIsAbleToPickUp;
    }

    private void SetIsAbleToPickUp(bool isAbleToPick)
    {
        _isAbleToPickUp = isAbleToPick;
    }

    private void GetInput()
    {
#if   UNITY_EDITOR
        MouseInput();

#elif UNITY_IOS
        MobileInput();

#elif UNITY_ANDROID
        MobileInput();

#elif UNITY_STANDALONE_OSX
        MouseInput();

#elif UNITY_STANDALONE_WIN
        MouseInput();
#else
        if ( ! MouseInput() )
        {
            MobileInput();
        }
#endif
    }

    private void MobileInput()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.touches[0].position;
            PickUpObject(touchPosition);
        }
    }

    private bool MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            PickUpObject(mousePosition);

            return true;
        }
        else
        {
            return false;
        }
    }

    private void PickUpObject(Vector3 inputPosition)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(inputPosition);

        if (Physics.Raycast(ray, out hit, 100f, _spawnedObjectLayerMask))
        {
            _isAbleToPickUp = false;
            Transform spawnedObjectTransform = hit.transform;

            PickUpEvent?.Invoke(spawnedObjectTransform);

            string fruitName = spawnedObjectTransform.GetComponent<SpawnObject>().GetName();
            PickUpSpecificFruitEvent?.Invoke(fruitName);
        }
    }
}
