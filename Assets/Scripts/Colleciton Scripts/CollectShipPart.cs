using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Collection component for Shipparts
public class CollectShipPart : MonoBehaviour
{
    // Same as for OxygenTanks, when PlayerCapsule collides, calls handling function of PlayerScript and remove the object
   void OnTriggerEnter(Collider other)
   {
    FindObjectOfType<PlayerStatsHandler>().ShipPartCollected();
    Destroy(gameObject);
   }
}
