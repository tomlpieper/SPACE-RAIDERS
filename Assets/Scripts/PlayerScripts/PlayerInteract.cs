using UnityEngine;


// this script uses raycasting of the FPS-Cam-View to interact with object that have the customized class and layermask "interactable"
// other scripts needed for this are the override Interactable Script, and creating a layermask for the unity Editor
public class PlayerInteract : MonoBehaviour
{   
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;


    // Start is called before the first frame update
    void Start()
    {   
        // get playercam
        cam = GetComponent<PlayerLook>().cam;

        // get the Canvas the player is looking at/through
        playerUI = GetComponent<PlayerUI>();

        // get the (new) inputmanager to see if key e (assigned to interact action) is pressed 
        inputManager = GetComponent<InputManager>();

        // in scenes where we have a playermodel i.e. not UI based scenes like menu or the last screen, disable the cursor 
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        // creates ray 
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        // create a RaycastHit to store our rayinformation
        RaycastHit hitInfo;

        // if the ray hits an object with the interactable mask in the predefined distance (3 in our case), store its information in hitinfo
        if (Physics.Raycast(ray, out hitInfo, distance, mask)) {
            
            // if the object has an interactable component i.e. is of Class Interactable
            if (hitInfo.collider.GetComponent<Interactable>() != null) {
                // access that component of the hit object
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                // get the Prompt-message of that object and display it in the PlayerUI
                playerUI.UpdateText(interactable.promptMessage);

                // if player presses e, call the customized interaction function of this object
                if(inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
