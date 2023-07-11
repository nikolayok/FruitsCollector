using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    public enum Fruits { Apple, Orange, Banana } // Apple, Orange, Banana не переименовывать

    protected bool ApplePredicate(Fruits fruit)
    {
        if (fruit == Fruits.Apple)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool BananaPredicate(Fruits fruit)
    {
        if (fruit == Fruits.Banana)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool OrangePredicate(Fruits fruit)
    {
        if (fruit == Fruits.Orange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
