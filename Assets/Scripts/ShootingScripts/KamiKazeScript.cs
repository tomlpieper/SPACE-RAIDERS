using UnityEngine.AI;
using UnityEngine;


// This script is responsible for controlling the enemy AI, Drones in our case.
// I got the idea and most of the concepts from youtube tutorials again
// The basic idea consists of NavMeshAgents, that are switching between 3 States: patroling, chasing, and attacking (in our case exploding)
// The States are dependent on the distance to the player - if the player steps inside sightRange, the drone will chase after him, if it gets close enough it will explode and damage him.
// In "Idle" / patroling mode, the drone will randomly select a walkpoint on the navmesh and fly there.
// Although each function is reletively basic, the change of states is smooth an gives the enemies a kind of smart appearence and makes the game more dynamic.

public class KamiKazeScript : MonoBehaviour
{
    // referencing the agent itself and the player 
    public NavMeshAgent agent;
    public Transform player;

    // getting the layermasks, needed for navigation and statechanges (the what is ground is needed, so the Agent doenst go offmap)
    public LayerMask whatIsGround, whatIsPlayer;
    // always chosen newly when patroling
    public Vector3 walkPoint;
    // max range of the chosen point
    public float walkPointRange;
    // height (y-value) of the walkpoint - needed, because our agent have the appearance of flying
    public float levelRange;
    // needed for agents who can attack multiple times
    public float timBetweenAttacks;
    // ranges to define borders for state changes 
    public float sightRange, attackRange;

    // bool for limiting the explosion to one time (as the explosion plays the drone is still in range of the player and continuously explodes again and again otherwise)
    bool alreadyAttacked = false;
    // bool whether the drone already has a walkpoint or should select a new one
    bool walkPointSet;
    // bools to trigger states
    bool playerInSightRange, playerInAttackRange;

    // referencews to Animations, and effects of the drone (small explosion didint work unfortunately)
    public GameObject explosion;
    public ParticleSystem explosionPS;

    public ParticleSystem explosion_small;

    //stats of drone 
    public int explosionDamage;
    public float explosionRange;
    public float health;

    private bool died = false;

    // basically start method for the NavAgent
    void Awake()
    {   //find playerobject and own agent in scene
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {   
        // check if player is in sight
        playerInSightRange =  Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange =  Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        //state based calling of agents functions 
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        if(playerInSightRange && playerInAttackRange && !alreadyAttacked) Attacking();
        
        
    }


    private void Patroling()
    {   
        // if doesnt have a walkpoint yet, select new
        if (!walkPointSet) SearchWalkPoint(); 
        // setDestination is a buildin method of Navmeshagents
        if (walkPointSet) agent.SetDestination(walkPoint);

        // compute distance to walkpoint for checking if reached
        Vector3  distanceToWalkPoint = transform.position - walkPoint;

        // walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
        
    }

    private void SearchWalkPoint()
    {
        // create random variables for x and y in range 
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        // create new walkpoint
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // check if walkpoint is on map
        if(Physics.Raycast(walkPoint, -transform.up, 5f, whatIsGround)){
            walkPointSet = true;
        }

    }

    private void Chasing()
    {
        // just move the agent at max speed towards the player
        agent.SetDestination(player.position);
       
    }

    private void Attacking()
    {
        // set bool, so explosion only happens once
        alreadyAttacked = true;
        // play anim
        explosionPS.Play();

        // disable renderer to make drone disappear instantly 
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        // get all objects with the layermask whatisplayer in explosionRange and store them in a list (only one entry in our case)
        Collider[] playerCollider = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
        
        // for each entry access the statshandler of the player and let him take Damage
        for (int i = 0; i < playerCollider.Length; i++)
        {
            if (playerCollider[i] != null) FindObjectOfType<PlayerStatsHandler>().TakeDamage(explosionDamage);
        }
        // for the animation to play, wait before deleting the gameobject
        Invoke("DelayDeletion", 4f);
        
    }

    private void DelayDeletion()
    {
        Destroy(gameObject);
    
    }



    // standard takeDamage function like in playerstatshandler - reduce health, unless helth = 0, then die
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }    
    }
    
    private void Die()
    {
        // again check if already died, to avoid multiple effectys
        // add Score to player
        if (!died) FindObjectOfType<PlayerStatsHandler>().EnemyDestroyed(100);
        // play anim
        explosion_small.Play();
        //disable all colliders and renderers
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        // delayed deletion for keeping the effect
        Invoke("DelayDeletion", 2f);

    }
}

