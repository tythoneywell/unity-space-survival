using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntegratedTurret : MonoBehaviour
{
    [SerializeField]
    public float turretDamage = 1;
    [SerializeField]
    public float fireRate = 0.25f; //player must wait fireRate seconds to fire turret again
    [SerializeField]
    public float weaponRange = 50f; //
    [SerializeField]
    public float hitForce = 100f;
    public Transform gunEnd;

    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);  //determines how long laser will be drawn for inside coroutine

    public Transform swivelTransform;
    public Transform barrelTransform;

    private AudioSource gunAudio;
    public AudioClip startAudio;
    private LineRenderer laserLine; //holds two points wherein a line is drawn between them

    private bool firing;
    private float nextFire; //holds the time for how long until player can fire again

    private Camera turretCam;
    public GameObject turretCamObject;

    public LaserSystem laserSystem;

    void Start()
    {
        laserLine = GetComponentInChildren<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        turretCam = turretCamObject.GetComponent<Camera>();
    }


    void Update()
    {
        Vector3 laserTargetPoint;

        Ray laserRay = turretCam.ScreenPointToRay(Mouse.current.position.ReadValue());  // Mouse aiming direction
        RaycastHit hit;
        if (Physics.Raycast(laserRay, out hit))
        {
            //Debug.Log("laser target found");
            laserTargetPoint = hit.point;
        }
        else
        {
            laserTargetPoint = turretCam.transform.position + laserRay.direction * (weaponRange + laserSystem.bonusRange) * 1.2f;
        }

        if (firing)
        {
            // Set laser start point
            laserLine.SetPosition(0, gunEnd.position);
            if (Vector3.Distance(gunEnd.position, laserTargetPoint) < (weaponRange + laserSystem.bonusRange))
            {
                //Debug.Log("hit");

                //set laser end point to target
                SetLaserEndPoint(laserTargetPoint);

                if (Time.time > nextFire)
                {
                    if (Random.value <= ShipSystemController.main.powerSatisfaction)
                    {
                        StartLaserFX();
                        if (hit.collider != null)
                            hit.collider.GetComponent<IMineable>()?.DamageHealth(turretDamage + laserSystem.bonusDamage);
                    }
                    else
                    {
                        StopLaserFX();
                    }
                    nextFire = Time.time + fireRate; //update next time gun can fire
                }
            }
            else
            {
                //set laser end point to partway to target
                SetLaserEndPoint(gunEnd.transform.position + (laserTargetPoint - gunEnd.transform.position).normalized * (weaponRange + laserSystem.bonusRange));
                if (Time.time > nextFire)
                {
                    if (Random.value <= ShipSystemController.main.powerSatisfaction)
                    {
                        StartLaserFX();
                    }
                    else
                    {
                        StopLaserFX();
                    }
                    nextFire = Time.time + fireRate; //update next time gun can fire
                }
            }
        }

        swivelTransform.LookAt(transform.position + Vector3.ProjectOnPlane(laserTargetPoint - transform.position, transform.up), transform.up);
        barrelTransform.LookAt(transform.position + Vector3.ProjectOnPlane(laserTargetPoint - transform.position, swivelTransform.right), transform.up);
    }
    public void StartLaser()
    {
        firing = true;
        gunAudio.Play();
        StartLaserFX();
        gunAudio.PlayOneShot(startAudio);
        laserSystem.powerConsumption = 0.5f * laserSystem.powerMul;
    }
    public void StopLaser()
    {
        firing = false;
        gunAudio.Stop();
        StopLaserFX();
        laserSystem.powerConsumption = 0f;
    }

    private void StartLaserFX()
    {
        gunAudio.UnPause(); //TODO: insert audio file
        laserLine.enabled = true;
    }
    private void StopLaserFX()
    {
        gunAudio.Pause(); //TODO: insert audio file
        laserLine.enabled = false;
    }

    void SetLaserEndPoint(Vector3 point)
    {
        Vector3 startPos = laserLine.GetPosition(0);
        for (int i = 1; i < laserLine.positionCount; i++)
        {
            laserLine.SetPosition(i, startPos + (point - startPos) * ((float)i / (laserLine.positionCount - 1)));
        }
    }
}