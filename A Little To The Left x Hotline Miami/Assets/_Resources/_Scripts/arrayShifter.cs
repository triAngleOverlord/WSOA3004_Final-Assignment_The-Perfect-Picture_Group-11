using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrayShifter 
{
    public static T[] ShiftArray<T>(T[] array, int shiftAmount)
    {
        if (array == null || array.Length == 0)
            return array;

        int length = array.Length;

        // Normalize the shift amount to prevent excessive rotations
        shiftAmount %= length;

        // If shiftAmount is negative, convert it to an equivalent positive shift
        if (shiftAmount < 0)
        {
            shiftAmount += length;
        }

        // Perform the shift
        T[] result = new T[length];
        for (int i = 0; i < length; i++)
        {
            int newIndex = (i + shiftAmount) % length;
            result[newIndex] = array[i];
        }

        return result;
    }
}
