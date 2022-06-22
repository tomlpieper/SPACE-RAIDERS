using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Tryout of state-based animations for doors etc., also not in the final game, base for CockPitDoor and Gate
public class Keypad : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;


     protected override void Interact()
    {
        
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
        
        
    }
}
