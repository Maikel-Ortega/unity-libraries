using UnityEngine;
using System.Collections;

public class RDSRandom  
{
    #region TYPE INITIALIZER
    private static System.Random rnd = null;

    static RDSRandom()
    {
        SetRandomizer(null);
    }


    /// <summary>
    /// You may replace the randomizer used by calling the SetRandomizer method with any object derived from Random.
    /// Supply NULL to SetRandomizer to reset it to the default RNGCryptoServiceProvider.
    /// </summary>
    /// <param name="randomizer">The randomizer to use.</param>
    public static void SetRandomizer(System.Random randomizer)
    {
        if (randomizer == null)
        {
            rnd = new System.Random();
        }
        else
        {   
            rnd = randomizer;
        }
    }

    #endregion

    #region DOUBLE

    public static double GetDoubleValue(double max)
    {
        return rnd.NextDouble() * max;
    }

    public static double GetDoubleValue(double min, double max)
    {
        return rnd.NextDouble() * (max - min);
    }


    #endregion

    #region INT

    public static double GetIntValue(int max)
    {
        return rnd.Next() * max;
    }

    public static double GetIntValue(int min, int max)
    {
        return rnd.Next() * (max-min);
    }


    #endregion

    #region ISPERCENTHIT METHOD
    /// <summary>
    /// Determines whether a given percent chance is hit.
    /// Example: If you have a 3.5% chance of something happening, use this method
    /// as "if (IsPercentHit(0.035)) ...".
    /// </summary>
    /// <param name="percent">The percent. Value must be between 0.00 and 1.00. 
    /// Negative values will always result in a false return.</param>
    /// <returns>
    ///   <c>true</c> if [is percent hit] [the specified percent]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsPercentHit(double percent)
    {
        return (rnd.NextDouble() < percent);
    }
    #endregion


}
