using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


// this class is used to update the prompt message to be displayed when looking at interactable objects
public class PlayerUI : MonoBehaviour
{
    // UI GameObject of The text
    [SerializeField]
    private TextMeshProUGUI promptText;
    
    // function called to set the text in UI
    public void UpdateText(String message){
        promptText.text = message;
    }
}
