using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomGenerator 
{
    public static bool RandomNum(int prob)
    {
        int random = Random.Range(0, 100);
        if (random < prob)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
