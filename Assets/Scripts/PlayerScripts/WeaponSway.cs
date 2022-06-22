using UnityEngine;


// this class is put on the weapon the player is holding to move it slightly with the cam\
// makes the looking around more realistic and less static
public class WeaponSway : MonoBehaviour
{   

    // get reference to the input manager
    public PlayerInput _input;

        // assing master values
        [SerializeField]
        float rotateSpeed = 4f;

        [SerializeField]
        float maxTurn = 3f;


        // check if new input system is enabled
        private void OnEnable()
        {
            _input = new PlayerInput();
            _input.Enable();
        }
        // called every frame
        void Update()
        {
            // get input vector from inputManager 
            Vector2 mouseInput = _input.OnFoot.Look.ReadValue<Vector2>();
            // compute rotation and apply it 
            ApplyRotation(GetRotation(mouseInput));
        }
        // formular for compution 3 dimensional rotation from the 2d Vector input given by the mouse
        // to be fair, i did not find this computation on my own, I used a tutorial
        Quaternion GetRotation(Vector2 mouse)
        {
            mouse = Vector2.ClampMagnitude(mouse, maxTurn);

            Quaternion rotX = Quaternion.AngleAxis(-mouse.y, Vector3.right);
            Quaternion rotY = Quaternion.AngleAxis(mouse.x, Vector3.up);

            Quaternion targetRot = rotX * rotY;

            return targetRot;
        }

        // transform the gameobject assigned to the script (weapon in our case)
        void ApplyRotation(Quaternion targetRot)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, rotateSpeed * Time.deltaTime);
        }
}
