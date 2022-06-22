using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// just a first tryout of animating objects, not actually used in the game
public class IntSphere : Interactable
{

    private bool big = false;

    // in here we do animations and shit with the object
        protected override void Interact()
    {
        if (!big) 
        {
            gameObject.transform.localScale += new Vector3(1,1,1);
            big = !big;
        } else 
        {
            Destroy(gameObject);
        }
        
    }
}
