using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TurretController : InteractableObject
{

    // Tweakable params
    [SerializeField]
    Vector2 lookSensitivity;
    
    // Private refs
    Camera playerCamera;

    float vertLookAngle = 0f;
    [SerializeField]
    public float turretDamage = 1;
    [SerializeField]
    public float fireRate = 0.25f; //player must wait fireRate seconds to fire turret again
    [SerializeField]
    public float weaponRange = 50f; //
    [SerializeField]
    public float hitForce = 100f;
    public Transform gunEnd;

    private Camera turretCam;


    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);  //determines how long laser will be drawn for inside coroutine

    private AudioSource gunAudio;
    private LineRenderer laserLine; //holds two points wherein a line is drawn between them
    private float nextFire; //holds the time for how long until player can fire again

    private InputAction click;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = gameObject.GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        turretCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Input */
    public void Look(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            gameObject.transform.Rotate(new Vector3(0, direction.x * lookSensitivity.x, 0));

            vertLookAngle += lookSensitivity.y * -direction.y;
            vertLookAngle = Mathf.Clamp(vertLookAngle, -90, 90);
            playerCamera.transform.localRotation = Quaternion.Euler(vertLookAngle, 0, 0);
        }
    }
    private IEnumerator ShotEffect()
    {
        gunAudio.Play(); //TODO: insert audio file
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
    public override void Interact(PlayerInteraction presser)
    {
        //copy kyles code (more orless)
    }
    public void LeaveTurrent(InputAction.CallbackContext context) {
        //copy kyles code (more or less)
    }
    public void Shoot(InputAction.CallbackContext context) {
        if (context.performed) {
            
                Debug.Log("clicked"); //debug
                if (Time.time <= nextFire) return; //do nothing if turret cannot fire again just yet (because of cooldown time nextFire)
                //otherwise fire turret

                nextFire = Time.time + fireRate; //update next time gun can fire
                StartCoroutine(ShotEffect()); //start coroutine for sound
                Vector3 rayOrigin = turretCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));  //camera center position

                RaycastHit hit;

                //set laser start point
                laserLine.SetPosition(0, gunEnd.position);

                if (Physics.Raycast(rayOrigin, turretCam.transform.forward, out hit, weaponRange))
                {
                    Debug.Log("hit");

                    //set laser end point
                    laserLine.SetPosition(1, hit.point);
                    //interact with hit object
                    //hit.collider.GetComponent<IMineable>()?.OnClick();

                    hit.collider.GetComponent<IMineable>()?.DamageHealth(turretDamage);
                    //hit.collider.GetComponent<CopperAsteroid>()?.DamageHealth(turretDamage);
                }
                else
                {
                    //set laser end point
                    laserLine.SetPosition(1, rayOrigin + (turretCam.transform.forward * weaponRange));
                }



                /*Vector3 coor = Mouse.current.position.ReadValue();
                if (Physics.Raycast(turretCam.ScreenPointToRay(coor), out hit))
                {
                    hit.collider.GetComponent<IClickable>()?.OnClick();
                }*/
            
        }
    }
}
