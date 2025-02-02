using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{


    EnemyBaseState currentState;

    public IdleState idleState = new IdleState();
    public MeleeAttackState meleeAttackState = new MeleeAttackState();
    public LaserAttackState laserAttackState = new LaserAttackState();
    public EruptionAttackState eruptionAttackState = new EruptionAttackState();
    

    public EnemyAnimationController enemyAnimationController;
    public EnemyMovement enemyMovement;
    public Rigidbody2D rb;
    public Transform target;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer Sword;
    public Collider2D enemyCollider;

    public static float health = 200f;
    public HealthBar healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();


        healthBar.SetMaxHealth(200);
        healthBar.UpdateHealth((int)health);

        currentState = eruptionAttackState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);   
        if(PlayerHealth.health <= 0 )
        {
            Destroy(gameObject);
        }
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealth((int)health);
    }

}
