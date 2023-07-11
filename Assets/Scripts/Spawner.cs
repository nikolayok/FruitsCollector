using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToSpawnList;
    private BoxCollider _collider;
    private Vector3 randomLocation;
    private Bounds _bounds;
    private Vector3 _minBounds;
    private Vector3 _maxBounds;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _bounds = _collider.bounds;
        _minBounds = _bounds.min;
        _maxBounds = _bounds.max;

        InvokeRepeating("SpawnCube", 0f, 0.5f);
    }

    private Vector3 GetRandomLocation()
    {
        randomLocation.x = Random.Range(_minBounds.x, _maxBounds.x);
        randomLocation.y = Random.Range(_minBounds.y, _maxBounds.y);
        randomLocation.z = Random.Range(_minBounds.z, _maxBounds.z);

        return randomLocation;
    }

    private int GetRandomSpawnObjectNumber()
    {
        int randomSpawnObjectNumber = Random.Range(0, _objectsToSpawnList.Count);
        return randomSpawnObjectNumber;
    }

    private void SpawnCube()
    {
        GetRandomLocation();
        int randomSpawnObjectNumber = GetRandomSpawnObjectNumber();
        Instantiate(_objectsToSpawnList[randomSpawnObjectNumber], randomLocation, Random.rotation);
    }
}
