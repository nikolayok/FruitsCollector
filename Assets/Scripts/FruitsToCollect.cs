using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FruitsToCollect : CollectManager
{
    [SerializeField] private Text _applesNeedToCollectText;
    [SerializeField] private Text _bananasNeedToCollectText;
    [SerializeField] private Text _orangesNeedToCollectText;

    public readonly List<Fruits> _listOfFruitsToCollect = new List<Fruits>();

    private const int _maxDifferentFruits = 3; // max amount of different fruits
    private const int _maxAmountOfFruits = 5; // in Random.Range need to set as _maxAmountOfFruits + 1
    private const int _minAmountOfFruits = 1;

    private void Start()
    {
        CreateCollectTask();
    }

    public void CreateCollectTask()
    {
        _listOfFruitsToCollect.Clear();

        int fruitsAmount = UnityEngine.Random.Range(_minAmountOfFruits, _maxAmountOfFruits + 1);

        for (int i = 0; i < fruitsAmount; ++i)
        {
            Fruits fruit = GetRandomFruit();
            _listOfFruitsToCollect.Add(fruit);
        }

        UpdateAmountOfFruitsToCollectText();
    }

    private Fruits GetRandomFruit()
    {
        Fruits randomFruit = (Fruits)UnityEngine.Random.Range(0, _maxDifferentFruits);
        return randomFruit;
    }

    private void UpdateAmountOfFruitsToCollectText()
    {
        _applesNeedToCollectText.text = Convert.ToString(_listOfFruitsToCollect.FindAll(ApplePredicate).Count);
        _bananasNeedToCollectText.text = Convert.ToString(_listOfFruitsToCollect.FindAll(BananaPredicate).Count);
        _orangesNeedToCollectText.text = Convert.ToString(_listOfFruitsToCollect.FindAll(OrangePredicate).Count);
    }

}
