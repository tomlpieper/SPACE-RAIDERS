using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script is a Superscript, handling all the information that the player itself has, e.g. lifes, health, score etc. 
// All other parts of the game interact with this class through public functions when interaction between player and environment are happening
// It is also responsible for permanently storing player stats betweenm scenes, getting hit by enemies and reducing the o2 level when outside
// As it is used on the prefab of the player in every level, a lot of the public variables are newly assigned in each level
public class PlayerStatsHandler : MonoBehaviour
{
    // set max values for health and o2
    public int oxygenMax = 100;

    public int healthMax = 100;
    // assign the amount of parts to win the level
    public int shipPartsThisLevel;

    // static ints to store the score, life, and already collected shipparts of the player and access them through other scripts
    public static int playerScore;
    public static int playerLifes;
    public static int shipparts;
    // current health and oxygen level 
    public float oxygen;
    public float health;
    // float defines in which intervals i.e. how fast the oxygen drains
    public float delayAmount = 0.1f;

    // mainly used for debugging the game (starts any level with perfect values to be able to tryout)
    public bool startFresh;
    // checks if current scene is not the tutorial (only in the tutorial, there is no constant drain of oxygen)
    public bool outside;

    // references to the playerUI modules displaying the current states 
    public GameObject scoreText;
    public GameObject lifeCounterUI;
    public GameObject partsCounter;
    
    // timer used for reducing the oxygen outside the ship
    protected float timer;


    // Start is called before the first frame update
    void Start()
    {   
        // if not tutorial, set outside true
        if(SceneManager.GetActiveScene().buildIndex != 1){
            outside = true;
        }
     

        // Note: For bigger projects with more scenes this could use some rewriting, but it works as it is currently (you know the drill: never touch a running system)
        // debugging and tutorial
        if(startFresh)
        {
            // reset player stats for new start
            PlayerPrefs.DeleteAll();
            // assign standard stats
            playerScore = 0;
            playerLifes = 3;
            // set max oxygen
            FindObjectOfType<OxygenLevel>().SetMaxOxygen(oxygenMax, true);
            // for tutorial this is reduced, so the player collects the tanks inside the ship and therefore start level 1 with 100% oxygen only if he finds all tanks
            FindObjectOfType<OxygenLevel>().SetOxygen(20);
            // set health to Max
            FindObjectOfType<HealthBarScript>().SetMaxHealth(healthMax, true);
        }
        else {
            // set max values
            FindObjectOfType<OxygenLevel>().SetMaxOxygen(oxygenMax, false);
            FindObjectOfType<HealthBarScript>().SetMaxHealth(healthMax, true);
            // if not tutorial
            if(outside){
                // load player stats from previous level
                playerScore = PlayerPrefs.GetInt("score");
                oxygen = PlayerPrefs.GetFloat("oxygen");
                shipparts = PlayerPrefs.GetInt("parts");
                health = PlayerPrefs.GetInt("health");
                FindObjectOfType<OxygenLevel>().SetOxygen(oxygen);
                
            } else {
                // if tutorial, set oxygen values to standard 
                FindObjectOfType<OxygenLevel>().SetMaxOxygen(oxygenMax, false);
                FindObjectOfType<OxygenLevel>().Reduce(70);
                
            }
            // load amount of lifes
            playerLifes = PlayerPrefs.GetInt("lifes");

            
            

        }
        // display lifes in UI
        lifeCounterUI.GetComponent<Text>().text = "Lifes: " + playerLifes;
        
       
        
    }

    // Update is called once per frame
    void Update()
    {   

        // drains oxygen every 0.1 seconds by 2 (1/50) if outside the ship
        if(outside){
            timer += Time.deltaTime;
            if (timer >= delayAmount){
                timer = 0;
                FindObjectOfType<OxygenLevel>().Reduce(2f);
            }
        }
        

        // update Player UI 
         scoreText.GetComponent<Text>().text = "Score: " + playerScore;
         // check if tutorial and deactivate parts UI, as there are no parts in this level
         if (SceneManager.GetActiveScene().buildIndex == 0) partsCounter.GetComponent<Text>().text = "";
         // else display the amount of already collected and needed parts
         else{
            partsCounter.GetComponent<Text>().text = "Parts: " + shipparts + "/" + shipPartsThisLevel;
         }
         // if oxygen runs out or health is down to 0, call gamemanager and endGame (We can work with epual 0 here, because the value of a slider cannot go below that)
         if (FindObjectOfType<OxygenLevel>().slider.value == 0 || FindObjectOfType<HealthBarScript>().slider.value == 0){
            FindObjectOfType<GameManager>().EndGame();
         }
         
    }
    // give player scorepoints for collecting Oxygen tanks
    public void TankCollected()
    {
        playerScore += 10;
    }


    // update amount of parts already collected
    public void ShipPartCollected()
    {
        shipparts += 1;

         // if all parts of level 1 are collected, change Level to winning Screen

        if(SceneManager.GetActiveScene().buildIndex == 2 && shipparts == 3)
        {   
            // safe score etc. 
            SafeStats();
            // load next (in this case winning screen)
            FindObjectOfType<LevelLoader>().LoadNextLevel();
        }
        
    }

    // as the playerobject is newly loaded everytime we die or change scenes, Playerprefs gives an easy way to store variables permanently and load them after the reload
    public void SafeStats()
    {
        // store Data permanent in PlayerPrefs
        PlayerPrefs.SetInt("score", playerScore);
        PlayerPrefs.SetFloat("oxygen",FindObjectOfType<OxygenLevel>().GetOxygen());
        PlayerPrefs.SetInt("lifes",playerLifes);
        PlayerPrefs.SetInt("parts", shipparts);
    }
    // called in class as well as by other classes 
    public void Die()
    {   // if we are not down to last life yet
        if (playerLifes >= 2){
            //reduce lifes by one
            playerLifes -= 1;
            // safe in playerprefs (we dont use the SaveStats() here, because we dont want to save the score, when we die)
            PlayerPrefs.SetInt("lifes",playerLifes);
        // if we died with our last life, restart the whole game -> back to startscreen
        } else {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    // called by e.g. Drones, when exploding
    public void TakeDamage(int damage)
    {   
        // reduce health in the healthbarscript (for visualization, I put the handling of this not here but stored in the slider value - still however in this class update function checked constantly)
        FindObjectOfType<HealthBarScript>().Reduce(damage);
    }

    // when we kill an enemy, increase the playerscore
    public void EnemyDestroyed(int score)
    {
        playerScore += score;
    }
}

