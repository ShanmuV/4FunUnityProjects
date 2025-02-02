using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MrMath : MonoBehaviour
{
    [SerializeField] public EnemyAttackManager Attack;

    public NormalAttackPhase normalAttack = new NormalAttackPhase();
    public TanAttackPhase tanAttack = new TanAttackPhase();
    public IdleState idle = new IdleState();
    public IntegralAttackPhase integralAttack = new IntegralAttackPhase();

    public EnemyHealth health;


    public bool playerAlive = true;

    public UnityEvent EnemyDeath;

    EnemyAttackBaseState currentPhase;

    void Start()
    {
        currentPhase = normalAttack;
        currentPhase.EnterPhase(this);
    }

    void Update()
    {
        currentPhase.UpdatePhase(this);

        if(health.health <= 0)
        {
            EnemyDeath?.Invoke();
        }
    }

    public void StopAttacking()
    {
        playerAlive = false;
        currentPhase = idle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NormalBullet"))
        {
            health.TakeDamage(2f);
        }
        if (collision.CompareTag("RocketShot"))
        {
            health.TakeDamage(20f);
        }
    }

    public List<EnemyAttackBaseState> GetEnemyPhases()
    {
        return new List<EnemyAttackBaseState> { normalAttack, tanAttack, integralAttack };
    }

    public void SwitchState(EnemyAttackBaseState phase)
    {
        currentPhase = phase;
        currentPhase.EnterPhase(this);
    }

}
