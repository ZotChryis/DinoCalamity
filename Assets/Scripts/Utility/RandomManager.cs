using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager
{
    // Not sure if this should be a float or string, a string would allow word seeds but this may not matter at all.
    public string Seed { get;  private set; }

    public RandomManager()
    {
        // TODO: Make the default seed some random hash of the current date and time.
        Seed = "1";
    }

    public RandomManager(string seed)
    {
        Seed = seed;
    }

    public int Range(int lower, int higher)
    {
        throw new NotImplementedException();
    }

    public float Range(float lower, float higher)
    {
        throw new NotImplementedException();
    }
}
