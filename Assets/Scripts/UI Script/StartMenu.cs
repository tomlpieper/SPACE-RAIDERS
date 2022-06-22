using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// interaction function in StartScreen
public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {   
        // when clicked somewhere on the screen load tutorial and start game
        SceneManager.LoadScene(1);
    }
}
