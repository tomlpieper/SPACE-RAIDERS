using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// this script processes the (keyboard)inputs given by the input manager class to transform and move the playerobject 
public class PlayerMotor : MonoBehaviour
{   
    // refence character controller
    private CharacterController controller;
    // current velocity
    private Vector3 playerVelocity;
    // checks whether player is on the ground (to avoid double jumps)
    private bool isGrounded;
    // bools for the other current movementstates of the player
    private bool crouching, lerpCrouch, sprinting, forwardMove;

    private float crouchTimer;
    public float speed;
    // define physical boundaries
    public float gravity = -9.8f;
    public float jumpHeight = 2f;
    // Start is called before the first frame update
    void Start()
    {
        // assign character controller
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        // check for every frame whether player is on the ground
        isGrounded = controller.isGrounded;
        
        // bool getting changed by crouch function below
        if (lerpCrouch)
        {   
            // add passed time to variable
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            // if player crouches, change height of playercontroller to be able to pass more shallow parts of the game (also changes camview)
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1 , p);
            }
            else {
                controller.height = Mathf.Lerp(controller.height, 2 , p);
            }
            // if crouched for certain time (computed above) deactivate crouching and restart timer
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
        // if player jumps or runs outside the map i.e. is falling into the void, EndGame/Die
        if (transform.position.y <= -10) {
            FindObjectOfType<GameManager>().EndGame();
        }

    }
    //receives inputs from input manager script and apply them to Character controller
    public void ProcessMove(Vector2 input)
    {   
        // create empty vector3
        Vector3 moveDirection = Vector3.zero;
        //split input into x and y
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        // actually move the player according to input and predefined max speed
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        // stop moving in y direction when falling back to ground
        if (isGrounded && playerVelocity.y < 0) 
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    // when player is on the ground and Jump (space) is pressed, calculate change of y position by predefined jumpheight and jump
    public void Jump()
    {   
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
        }
    }
    // activates crouching by changing the lerpcrouch bool -> activates lerpcrouch function in Update Function
    public void Crouch()
    {   
        // change state of crouching
        crouching = !crouching;
        // reset timer (not used anymore in my current version)
        crouchTimer = 0;
        lerpCrouch = true;
        
    }

    public void Sprint()
    {
        // if player is moving forward (so we cant sprint the the back of site)
        if (forwardMove){
                sprinting = !sprinting;
            if (sprinting)
            {
                // increase playerspeed
                speed = 6;
                //stop crouching
                crouching = false;
            } else {
                //reset speed to normal playerspeed
                speed = 3;
            }
        }
        
    }

    // additional possibilty of using the sprint function (I let this in here, is not used anymore though, because the function above proved to be more easy in regards of checking forwardmove etc.)
    public void SprintStart()
    {
        speed = 6;
    }

    public void SprintStop()
    {
        speed = 3;
    }
}
