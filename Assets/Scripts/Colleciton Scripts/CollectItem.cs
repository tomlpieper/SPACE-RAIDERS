using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Add to all Tanks
public class CollectItem : MonoBehaviour
{
   // if Collider hits with other Collider i.e. that of the PlayerCapsule 
   void OnTriggerEnter(Collider other)
   {
    // call PlayerStatsHandler and Oxygen-handler (responsible for UI) and change O2 Levels with public voids
    FindObjectOfType<PlayerStatsHandler>().TankCollected();
    FindObjectOfType<OxygenLevel>().TankCollected();
    // remove container
    Destroy(gameObject);
   }
}
