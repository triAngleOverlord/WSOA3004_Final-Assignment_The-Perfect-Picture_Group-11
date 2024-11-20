using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Dead Body Manager", order = 1)]
public class differentDeadBodies: ScriptableObject
{
    [SerializeField] public Sprite stabbed;
    [SerializeField] public Sprite headShot;
    [SerializeField] public Sprite holeInChest;
    [SerializeField] public Sprite gutsOut;
    [SerializeField] public Sprite inHalf;
    [SerializeField] public Sprite limbsOff;
}
