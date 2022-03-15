using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landStateTracker : MonoBehaviour
{
    // Start is called before the first frame update
    public float watering_interval;
    public int plant_stage;
    public bool needWater, checkingWater;
    public bool needBugpick, checkingBugPick;
}
