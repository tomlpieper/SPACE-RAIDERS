using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Script used for transition between scenes: Tutorial -> Level 1 e.g.

public class LevelLoader : MonoBehaviour
{
    // get references to the animator and set transition time
    public Animator transition;
    public int transitionTime;

    // Public void called from e.g. helmet script when interaction method is callled
    public void LoadNextLevel(){
        // call coroutine and add one to current level index to access next level
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }


    IEnumerator LoadLevel(int levelIndex)
    {
        // trigger anim
        transition.SetTrigger("EndScene");
        // wait for x seconds
        yield return new WaitForSeconds(transitionTime);
        // load next scene
        SceneManager.LoadScene(levelIndex);
    }
}
