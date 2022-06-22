using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


// Script to handle restarting scenes or the whole game when the player has died 

public class GameManager : MonoBehaviour
{
   // variables, how long the fade to restart should be and reference to the animator responsible for the fade 
   bool gameHasEnded = false;
   public int gameOverScreenDuration = 5;
   public Animator deathScreen;


    public void EndGame()
    {   
        // avoiding bugs and repition
        if(!gameHasEnded)
        {   
            // let player die 
            FindObjectOfType<PlayerStatsHandler>().Die();
            gameHasEnded = true;
            // Starting Coroutine to process multiple things parallel
            StartCoroutine(Restart(gameOverScreenDuration));
        }
        
    }

    IEnumerator Restart(int gameOverScreenDuration)
    {   
        // activate animation of game over screen by trigger
        deathScreen.SetTrigger("Died");
        // wait for x seconds
        yield return new WaitForSeconds(gameOverScreenDuration);
        // reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // called when player lifes are 0
    public void GameOver()
    {   
        // remove all permanently stored date
        PlayerPrefs.DeleteAll();
        // load starting Screen
        SceneManager.LoadScene(0);
    }


    



}
