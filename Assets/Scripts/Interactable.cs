using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is a Class template for Interactables
// it overrides functions, enabling us to use a custom Interact() function in every object of type interactable, which automatically displays a promptmessage and handles the function when interaction key is pressed
public abstract class Interactable : MonoBehaviour
{   
    // using unityevents
    public bool useEvents;
    [SerializeField]
    // the message to be displayed when player is in range an looking at object
    public string promptMessage;
   
   // the interaction itself
    public void BaseInteract()
    {
        // if bool is true object of layer interactable can use interaction with unity events and does not need any scripts
        if (useEvents)
        {
            GetComponent<InteractionEvent>().onInteract.Invoke();
        }
        // else call the function in the corresponding script
        Interact();
    }
   protected virtual void Interact()
   {
       // template to be overriden by subclasses 
   }
}
