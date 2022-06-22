using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Script for handling the communication between the slider representing the Health in UI and the healthvariables handeled in scripts (i.e. PlayerStatsHandler)
public class HealthBarScript : MonoBehaviour
{
    // refenence to UI slider
    public Slider slider;
    
    // called at beginning of Scene by Statshandler, sets max value for slider -  takes int with max health and a bool whether the curretn health should be max health and adjusts slider values accordingly
    public void SetMaxHealth(int max, bool restore)
    {
        slider.maxValue = max;
        if(restore){
            RestoreMaxHealth(max);
        }
        
    }

    // sets slider value to max
    public void RestoreMaxHealth(int max)
    {
        slider.value = max;
    }

    // sets slider value to certain int which is given as input
    public void SetHealth(float oxygen)
    {
        slider.value = oxygen;
    }

    // reduce health inGame, used by playerstatshandler when taking damage
    public void Reduce(float x)
    {
        slider.value = slider.value - x;
        
    }
    // not really used but could be used ot access health value if it wasnt a public slider
    public float GetHealth()
    {
        return slider.value;
    }
}
