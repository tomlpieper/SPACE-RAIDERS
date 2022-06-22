using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this is actually used on the keypad next to the door, besides it being named Cockpitdoorscript

public class CockpitDoor : Interactable
{   
    // assign the door to open and a bool whether it is already open
    public  GameObject door;
    public bool opened;
    private Collider collider;
    void Start()
    {   
        // assigning the BoxCollider of the door to be able to disable it, when door is being opened
        collider = door.GetComponent<BoxCollider>();
    }
    protected override void Interact()
    {   
        // change state of door
        opened = !opened;
        // access the animator of the door and trigger a change of state
        door.GetComponent<Animator>().SetBool("opened", opened);
        // disable collider
        door.GetComponent<BoxCollider>().enabled = false;
    }
}
