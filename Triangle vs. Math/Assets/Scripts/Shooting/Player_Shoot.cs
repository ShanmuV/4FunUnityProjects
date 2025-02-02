using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{

    public float shootInterval = 0.5f;
    private float timer = 0f;

    public bool isShootPressed;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform muzzle;

    [SerializeField] private EnemyAttackManager enemyAttacks;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isShootPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isShootPressed = false;
        }

    }

    private void FixedUpdate()
    {
        if (isShootPressed && timer > shootInterval)
        {
            Instantiate(bullet, muzzle.position, transform.rotation);
            timer = 0f;
        }
        timer += Time.deltaTime;
    }
}
