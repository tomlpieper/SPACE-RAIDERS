using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Add Component to all items that should be constantly rotated
public class Rotation : MonoBehaviour
{

 

    // Update is called once per frame
    void Update()
    {   
        // transform the gameobject once per frame, to animate them with a continuous rotation (tanks and ShipParts)
        transform.Rotate(0,0.5f,0);

       
    }
}
