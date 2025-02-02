using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Properties")]
    [SerializeField] public TowerSide towerSide;
    [SerializeField] private float towerRange;
    [SerializeField] private float attackCooldown;

    [Header("Additional stuff")]
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private GameObject bullet;


    /*    private RaycastHit2D fireSide;
        private RaycastHit2D waterSide;*/

    private RaycastHit2D hit;

    private int direction;
    private float attackCooldownTimer;

/*    [SerializeField] private float fireShootDelay = 1f;
    [SerializeField] private float waterShootDelay = 1f;*/

    void Start()
    {
        if (towerSide == TowerSide.FIRE)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    
    void Update()
    {
        /*        fireSide = Physics2D.Raycast(transform.position, -transform.right, 20f, EnemyLayer);
                waterSide = Physics2D.Raycast(transform.position, transform.right, 20f, EnemyLayer);
                if (fireSide.collider != null)
                {
                    if (fireSide.collider.gameObject.transform.position.x < 0f && fireShootDelay <= 0f)
                    {
                        Shoot(-1f);
                        fireShootDelay = 1f;
                    }
                    else if (fireSide.collider.gameObject.transform.position.x > 0f && fireShootDelay <= 0f)
                    {
                        Shoot(1f);
                        fireShootDelay = 1f;
                    }
                }
                if (waterSide.collider != null)
                {
                    if (waterSide.collider.gameObject.transform.position.x < 0f && waterShootDelay <= 0f)
                    {
                        Shoot(-1f);
                        waterShootDelay = 1f;
                    }
                    else if (waterSide.collider.gameObject.transform.position.x > 0f && waterShootDelay <= 0f)
                    {
                        Shoot(1f);
                        waterShootDelay = 1f;
                    }
                }
                fireShootDelay -= Time.deltaTime;
                waterShootDelay -= Time.deltaTime;*/

        hit = Physics2D.Raycast(transform.position, transform.right * direction, towerRange, EnemyLayer);
        if (hit.collider && attackCooldownTimer <= 0f)
        {
            Shoot();
            attackCooldownTimer = attackCooldown;
        }
        attackCooldownTimer -= Time.deltaTime;

    }

    private void Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}

public enum TowerSide
{
    FIRE,
    WATER
}
