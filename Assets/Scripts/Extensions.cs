using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{

    public static float Map(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static double toRadians(this double value)
    {
        return value * Mathf.PI / 180.0f;
    }

    public static double toDegrees(this double value)
    {
        return value * 180.0f / Mathf.PI;
    }

    public static float toRadians(this float value)
    {
        return value * Mathf.PI / 180.0f;
    }

    public static float toDegrees(this float value)
    {
        return value * 180.0f / Mathf.PI;
    }
}