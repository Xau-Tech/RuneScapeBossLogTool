using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoubleRound
{
    public static double RoundDown(this double num)
    {
        return num - (num % 1.0d);
    }
}