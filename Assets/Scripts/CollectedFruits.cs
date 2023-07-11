using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectedFruits : CollectManager
{
    [SerializeField] private Text _applesCollectedText;
    [SerializeField] private Text _bananasCollectedText;
    [SerializeField] private Text _orangesCollectedText;
    [SerializeField] private Text _taskWaveText;

    [SerializeField] private FruitsCollector _fruitsCollector;

    [SerializeField] private LevelFinished _levelFinished;
    [SerializeField] private FruitsToCollect _fruitsToCollect;

    private List<Fruits> _listOfCollectedFruits = new List<Fruits>();

    private int _currentWaveTask = 1;
    private const int _maxWaveTaskAmount = 3;

    private void OnEnable()
    {
        _fruitsCollector.PickUpSpecificFruitEvent += AddFruit;
    }

    private void OnDisable()
    {
        _fruitsCollector.PickUpSpecificFruitEvent -= AddFruit;
    }

    private void AddFruit(string fruit)
    {
        Fruits currentFruit;
        Enum.TryParse(fruit, out currentFruit);
        _listOfCollectedFruits.Add(currentFruit);

        UpdateAmountOfCollectedFruitsTexts();
        CheckFruitsToCollectList();
    }

    private void UpdateAmountOfCollectedFruitsTexts()
    {
        _applesCollectedText.text = Convert.ToString(_listOfCollectedFruits.FindAll(ApplePredicate).Count);
        _bananasCollectedText.text = Convert.ToString(_listOfCollectedFruits.FindAll(BananaPredicate).Count);
        _orangesCollectedText.text = Convert.ToString(_listOfCollectedFruits.FindAll(OrangePredicate).Count);
    }

    private void CheckFruitsToCollectList()
    {
        int amountOfApplesToCollect = _fruitsToCollect._listOfFruitsToCollect.FindAll(ApplePredicate).Count;
        int amountOfBananasToCollect = _fruitsToCollect._listOfFruitsToCollect.FindAll(BananaPredicate).Count;
        int amountOfOrangesToCollect = _fruitsToCollect._listOfFruitsToCollect.FindAll(OrangePredicate).Count;

        int amountOfCollectedApples = _listOfCollectedFruits.FindAll(ApplePredicate).Count;
        int amountOfCollectedBananas = _listOfCollectedFruits.FindAll(BananaPredicate).Count;
        int amountOfCollectedOranges = _listOfCollectedFruits.FindAll(OrangePredicate).Count;

        if (amountOfCollectedApples >= amountOfApplesToCollect)
        {
            if (amountOfCollectedBananas >= amountOfBananasToCollect)
            {
                if (amountOfCollectedOranges >= amountOfOrangesToCollect)
                {
                    ++_currentWaveTask;
                    if (_currentWaveTask >= _maxWaveTaskAmount + 1) // + 1 to make inclusive
                    {
                        FinishLevel();
                    }
                    else
                    {
                        _taskWaveText.text = "Задание " + _currentWaveTask + ":";
                        ResetFruitsValuesAndCreateFruitsTask();
                    }
                }
            }
        }
    }

    private void ResetFruitsValuesAndCreateFruitsTask()
    {
        _fruitsToCollect.CreateCollectTask();
        _listOfCollectedFruits.Clear();
        UpdateAmountOfCollectedFruitsTexts();
    }

    private void FinishLevel()
    {
        _levelFinished.FinishLevel();
    }
}
