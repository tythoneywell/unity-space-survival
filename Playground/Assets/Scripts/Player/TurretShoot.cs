using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    [SerializeField]
    public int turretDamage = 1;
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




    
    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        turretCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
