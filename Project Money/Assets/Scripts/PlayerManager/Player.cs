using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask Enemies;

    public Collider2D[] enemies;

    public GameObject closestEnemy;

    public Transform muzzle;

    [Header("Bullet Projectiles")]
    public GameObject DartProjectile;
    public GameObject TripleShotProjectile;

    Weapon currentWeapon;

    float shootCounter;
    float shootDelay;

    List<PowerUp> powerUps = new List<PowerUp>();

    Darts dart = new Darts();
    Tripleshot tripleShot = new Tripleshot();

    private void Start()
    {
        currentWeapon = dart;

        shootDelay = currentWeapon.GetShootDelay();
        shootCounter = shootDelay;

        currentWeapon.Initialize(DartProjectile);
        tripleShot.Initialize(TripleShotProjectile);

        powerUps.Add(tripleShot);

    }

    private void Update()
    {
        closestEnemy = FindClosestEnemy();

        if(shootCounter < 0f && closestEnemy != null)
        {
            currentWeapon.Shoot(this);
            shootCounter = shootDelay;
        }
        shootCounter -= Time.deltaTime;

        foreach( PowerUp powerUp in powerUps )
        {
            powerUp.UpdateState(this);
        }

    }

    public Collider2D[] GetEnemiesInRange()
    {
        Collider2D[] arr = Physics2D.OverlapCircleAll(transform.position, 5f, Enemies);
        return arr;
    }

    /*private void FixedUpdate()
    {
        GameObject g = FindClosestEnemy()?.gameObject; 
        if(enemies.Length > 0)
        {
            foreach(Collider2D c in enemies)
            {
                SpriteRenderer sp = c.gameObject.GetComponentInChildren<SpriteRenderer>();
                sp.color = Color.red;
            }
        }
        if(g != null)
        {
            SpriteRenderer spriteRenderer = g.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = Color.white;
        }
    }*/

    public GameObject FindClosestEnemy()
    {
        float minDistance = Mathf.Infinity;
        Transform closest = null;
        enemies = GetEnemiesInRange();
        float x;
        if(enemies.Length > 0)
        {
            foreach (Collider2D c in enemies)
            {
                if((x = Vector2.Distance(transform.position, c.transform.position)) < minDistance)
                {
                    minDistance = x;
                    closest = c.transform;
                }
            }
        }
        return closest?.gameObject;
    }
}
