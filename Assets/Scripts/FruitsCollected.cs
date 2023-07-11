using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsCollected : MonoBehaviour
{
    [SerializeField] private GameObject _correctFruitCollectedCanvas;
    [SerializeField] private GameObject _incorrectFruitCollectedCanvas;
    [SerializeField] private Camera _camera;

    [SerializeField] private FruitsCollector _fruitsCollector;

    [SerializeField] private Transform _basket;

    private void OnEnable()
    {
        _fruitsCollector.PickUpEvent += SpawnFruitCollectedCanvas;
    }

    private void OnDisable()
    {
        _fruitsCollector.PickUpEvent -= SpawnFruitCollectedCanvas;
    }

    private void SpawnFruitCollectedCanvas(Transform targetFruit)
    {
        int correct = UnityEngine.Random.Range(0, 2);
        bool isCorrect = false;
        if (correct == 1)
        {
            isCorrect = true;
        }

        GameObject gameObject;
        if (isCorrect)
        {
            gameObject = Instantiate(_correctFruitCollectedCanvas, _basket.position, Quaternion.Euler(0, -90, 0));
        }
        else
        {
            gameObject = Instantiate(_incorrectFruitCollectedCanvas, _basket.position, Quaternion.Euler(0, -90, 0));
        }

        gameObject.GetComponent<Canvas>().worldCamera = _camera;
    }
}
