using UnityEngine;


// this script is used to process the mouse movement using the new input manager
public class PlayerLook : MonoBehaviour
{   
    // get the cam of the player to be able to modify it
    public Camera cam;
    private float xRotation = 0f;
    
    // define sensitivity, which can later be edited in the Unity Editor from scene to scene
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    
    // this method is called by the input manager class, that directly obtains the vector of mouse-movement
    public void ProcessLook(Vector2 input)
    {
        // seperate x and y values of vector
        float mouseX = input.x;
        float mouseY = input.y;
        // calc camera rotation for looking up down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
        
    }
}
