using UnityEngine;
using UnityEngine.SceneManagement;


// Script for the Gun prefab, controlling firerate, damage and accessing the hit objects
public class GunScript : MonoBehaviour
{
    // getting playerinput
    private PlayerInput playerinput;

    // setting hyperparameters for weapon
    public float damage = 10f;
    public float range = 100f;
    // firerate of 4 means 4 shot per second
    public float fireRate = 4f;
    private float nextTimeToFire = 0f;


    // reference cam for raycasting the shots
    public Camera fpsCam;

    // referenc effects of the gun
    public ParticleSystem muzzleflash;

    public GameObject impactEffect;
    // this bool is changed in the Input handler constantly if the mouse button is pressed
    public bool shooting = false;

    // Update is called once per frame
    void Update(){
        //  if mouse 1 pressed and the temporal differnce is already in bounds of the firerate again (and we are not in the tutorial) 
        if (shooting && Time.time >= nextTimeToFire && SceneManager.GetActiveScene().buildIndex != 0){
            // set new time to fire
            nextTimeToFire = Time.time + 1f/fireRate;
            // set shooting to false 
            shooting = false;
            // call actual shooting function
            Shoot();
        }
    }

public void Shoot(){
    // play animation (in our case, muzzleflash + laser shots)
    muzzleflash.Play();

    // create hit variable to store hit Data (like in the interaction scripts)
    RaycastHit hit;
    // shoot ray with  prefefined range in the middel of our screen
    if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
        // create target e.g. a Drone with Type KamiKazeScript and access its component Script
        KamiKazeScript target = hit.transform.GetComponent<KamiKazeScript>();
        // if that script exists on our target
        if(target != null)
        {
            // access the Damage Function
            target.TakeDamage(damage);
        }
        // create effect at the point of impact and destroy ist after 2 Seconds 
        GameObject impactObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactObject, 2f);
    }

}
}
