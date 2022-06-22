using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// handling the input by the player according to the new action input system - one can look at it as some kind of distributer and link between input and processing of actions
public class InputManager : MonoBehaviour
{   
    // reference to input
    private PlayerInput playerInput;
    // reference to the action-map onFoot 
    public PlayerInput.OnFootActions onFoot;
    //reference to the custom motor script we wrote
    private PlayerMotor motor;
    //same for the looking script
    private PlayerLook look;
    // reference to the gun script for shooting action
    private GunScript gun;
    void Awake()
    {   
        // assign the playerinput and action map
        playerInput = new PlayerInput();
        
        onFoot = playerInput.OnFoot;
        
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        // for each action-input performed, create new viewContext and access the scripts responsible for processing the input values
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.SprintPressed.performed += ctx => motor.SprintStart();
        onFoot.SprintReleased.performed += ctx => motor.SprintStop();
        // in the shooting case, we handle the input a little differently, therefore only change the bool and dont call the method itself
        onFoot.Shoot.performed += ctx => FindObjectOfType<GunScript>().shooting = true;
    }

    // Delegate the movement actions to the motorscript
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        bool leftMousePressed = onFoot.Shoot.ReadValue<float>() > 0.1f;
        if (leftMousePressed){
            FindObjectOfType<GunScript>().shooting = true;
        }
    }

    // delegate the vector2 inputs describing the mouse position to he playerLook script
    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    // enable action map
    private void OnEnable()
    {
        onFoot.Enable();
    }
    // disable action map (could be later used for getting in vehicle and change the input handling)
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
