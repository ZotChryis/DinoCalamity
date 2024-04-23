using System;
using UnityEngine;

public class RandomManager
{
    // Not sure if this should be a float or string, a string would allow word seeds but this may not matter at all.
    [SerializeField] private string _seed;
    public string Seed { get => _seed;  private set { _seed = value; } }

    public RandomManager()
    {
        Init();
    }

    /// <summary>
    /// Initializes the random seed to a default value.
    /// </summary>
    public void Init()
    {
        // The default seed is a hash of the current date and time.
        System.DateTime date = System.DateTime.Now;
        Debug.Log($"RandomManager: date binary ({date.ToBinary()}).");
        Seed = "1";
    }

    /// <summary>
    /// Initializes the random seed to the input value
    /// </summary>
    /// <param name="seed"></param>
    public void Init(string seed)
    {
        Seed = seed;
    }

    /// <summary>
    /// Generates a random integer within the input range. 
    /// </summary>
    /// <param name="lower">The lower bound (inclusive).</param>
    /// <param name="higher">The lower bound (inclusive).</param>
    /// <returns></returns>
    public int Range(int lower, int upper)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Generates a random float within the input range.
    /// </summary>
    /// <param name="lower">The lower bound (inclusive).</param>
    /// <param name="higher">The upper bound (exclusive).</param>
    /// <returns></returns>
    public float Range(float lower, float upper)
    {
        throw new NotImplementedException();
    }
}
