using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//script used to display score in the UI of the final screen 
public class ShowScoreWinningScreen : MonoBehaviour
{
    private int score;
    // Start is called before the first frame update
    void Start()
    {   
        // get playerscore form permanent store
        score = PlayerPrefs.GetInt("score");
    }

    // Update is called once per frame
    void Update()
{       
    // update the text of of the scoretext
        GetComponent<Text>().text = "Your Score: " + score;    
    }
}
