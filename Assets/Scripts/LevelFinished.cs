using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinished : MonoBehaviour
{
    [SerializeField] private GameObject _levelFinishedCanvas;
    [SerializeField] private List<GameObject> _objectsToRemoveList;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _cameraTargetTransform;

    private bool _isLevelCompleted = false;
    private float _speed = 2;

    private void Update()
    {
        if (_isLevelCompleted)
        {
            MoveCameraToTargetPosition();
        }
    }

    public void FinishLevel()
    {
        _isLevelCompleted = true;
        Invoke("RemoveObjects", 1);
        Invoke("DeleteAllNotCollectedFruits", 1);
        Invoke("PlayLevelCompletedAnimation", 1);
        Invoke("OpenLevelFinishedCanvas", 3);
    }

    private void PlayLevelCompletedAnimation()
    {
        _animator.SetTrigger("LevelCompletedCelebrationTrigger");
    }

    private void RemoveObjects()
    {
        foreach (GameObject objectToRemove in _objectsToRemoveList)
        {
            objectToRemove.SetActive(false);
        }
    }

    private void OpenLevelFinishedCanvas()
    {
        _levelFinishedCanvas.SetActive(true);
    }

    private void DeleteAllNotCollectedFruits()
    {
        GameObject[] notCollectedFruits = GameObject.FindGameObjectsWithTag("Spawned");

        foreach (GameObject notCollectedFruit in notCollectedFruits)
        {
            Destroy(notCollectedFruit);            
        }
    }

    private void MoveCameraToTargetPosition()
    {
        _cameraTransform.Translate((_cameraTargetTransform.position - _cameraTransform.position).normalized * _speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(_cameraTransform.position, _cameraTargetTransform.position) <= 0.15f)
        {
            _isLevelCompleted = false;
        }
    }
}
