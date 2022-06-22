using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Script for Doorgate in Level 1 (Maybe could have been concatenated with the CockPitDoorScript)
public class DoorGate : Interactable
{
    // get gameobject of actual door to access the components
    public GameObject door;
    // get bool whether door is already open (default is false in Level 1)
    public bool open;

    // on Interaction by the player
    protected override void Interact()
    {
        // change bool
        open = !open;
        // trigger the animator with changed bool
        door.GetComponent<Animator>().SetBool("open", open);
    }
}
