using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script for handling the communication between the slider representing the o2 in UI and the oxygenvariables handeled in scripts (i.e. PlayerStatsHandler)

public class OxygenLevel : MonoBehaviour
{
    // refenence to UI slider
    public Slider slider;

    // called at beginning of Scene by Statshandler, sets max value for slider -  takes int with max oxygen and a bool whether the curretn oxygen should be max oxygen and adjusts slider values accordingly
    public void SetMaxOxygen(int max, bool restore)
    {
        slider.maxValue = max;
        if(restore){
            RestoreMaxOxygen(max);
        }
        
    }
    // set slidervalue to max
    public void RestoreMaxOxygen(int max)
    {
        slider.value = max;
    }

    // sets slider value to certain int which is given as input
    public void SetOxygen(float oxygen)
    {
        slider.value = oxygen;
    }

    // if oxygen tank is collected add 10 to slidevalue
    public void TankCollected()
    {
        slider.value = slider.value + 10;
    }

    // reduce oxygen by certain float (constantly called in Playerstatshandler update function when outside the ship)
    public void Reduce(float x)
    {
        slider.value = slider.value - x;
    }
    // also not used, but returns value when called
    public float GetOxygen()
    {
        return slider.value;
    }
}
