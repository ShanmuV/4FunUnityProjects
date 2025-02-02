using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{


    public bool isShootPressed;

    public NormalWeapon normalWeapon = new NormalWeapon();
    public TripleShot tripleShot = new TripleShot();
    public RocketShot rocketShot = new RocketShot();


    public WeaponBase currentWeapon;

    public Transform muzzle;

    [Header("Normal Bullet")]
    public GameObject bullet;

    [Header("Rocket Shot")]
    public GameObject rocket;

    private void Start()
    {
        currentWeapon = normalWeapon;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShootPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isShootPressed = false;
        }
        currentWeapon.UpdateTimer(this);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = normalWeapon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = tripleShot;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = rocketShot;
        }

    }

    private void FixedUpdate()
    {
        if (isShootPressed)
        {
            currentWeapon.Shoot(this);
        }
    }
}
