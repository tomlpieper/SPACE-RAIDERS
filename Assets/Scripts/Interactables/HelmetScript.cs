using System.Collections;
using UnityEngine;


// Script used for the animated helmet at end of tutorial to rotate, get lifted and Load the first Level with our custom LevelloaderScript
public class HelmetScript : Interactable 
{   
    // animator reference
    public Animator helmet;


    protected override void Interact()
    {
        // use Coroutine rather than function to be able to do multiple things parallel
        StartCoroutine(AnimateHelmet());

    }

    // Coroutine is used like Function with IEnumerator rathe than void
    IEnumerator AnimateHelmet()
    {
        // save stats permanently 
        FindObjectOfType<PlayerStatsHandler>().SafeStats();

        // trigger animation of helmet(defined in the Animtion-Window in Unity)
        helmet.SetTrigger("Collected");

        // just a comment to hold for some time, as to not load the next level instantly (animation wouldnt be seen)
        yield return new WaitForSeconds(1);
        
        // start next level
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }
}
