using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    
    [Header("Animator Controller")]
    [SerializeField] private Animator EnemyAnimator;

    [Header("Camera Shake")]
    [SerializeField] private Animator Camera;

    [Header("Melee Attack")]
    [SerializeField] private AudioSource SwordSlash;
    [SerializeField] private Animator Sword;

    [Header("Laser Attack")]
    [SerializeField] private ParticleSystem LaserTeleportParticle;
    [SerializeField] private GameObject LaserAttack;
    private Vector3 LaserLocation = new Vector3(-16.5f, 7f, -5f);
    Quaternion rotation = Quaternion.AngleAxis(180f, Vector3.forward);
    Quaternion LaserRotation = Quaternion.identity;

    [Header("Eruption Attack")]
    [SerializeField] private ParticleSystem EruptionTeleportParticle;
    [SerializeField] private GameObject EruptionAttackParticle;
    [SerializeField] private Transform Player;




    public bool isAttacking = false;



    public async void MeleeAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            Sword.StopPlayback();
            Sword.Play("Melee_Attack");

            int duration = (int)Sword.GetCurrentAnimatorStateInfo(0).length;
            await Task.Delay(duration * 1000);
            //SwordSlash.Play();

            Sword.StopPlayback();
            Sword.Play("Melee_Idle");

            Invoke("StopAttacking", 0.2f);
        }
    }

    public async Task DoLaserAttack()
    {
        if(!isAttacking)
        {
            isAttacking = true;

            EnemyAnimator.Play("Laser_Disappear");
            await WaitForAnimation();

            enemy.position = new Vector2(0f, 4f);

            EnemyAnimator.Play("Laser_Appear");
            await WaitForAnimation();

            EnemyAnimator.Play("Laser_Idle");
            LaserTeleportParticle.Play();
            await Task.Delay(1500);

            for (int i =0; i< 5; i++)
            {
                GameObject Laser = Instantiate(LaserAttack, LaserLocation, LaserRotation);
                ParticleSystem part = Laser.GetComponentInChildren<ParticleSystem>();
                Animator animator = part.GetComponent<Animator>();
                Camera.Play("Moderate_Shake");
                part.Play();
                animator.Play("HitBox");
                await Task.Delay(1500);
                Destroy(Laser);
                LaserLocation.x *= -1;
                LaserLocation.y -= 3.5f;
                LaserRotation = rotation * LaserRotation;
            }

            LaserLocation = new Vector3(-16.5f, 7f, -5f);
            LaserRotation = Quaternion.identity;

            EnemyAnimator.StopPlayback();
            LaserTeleportParticle.Stop();
            EnemyAnimator.Play("Laser_Disappear_End");
            await WaitForAnimation();

            enemy.position = new Vector2(0f, -5f);

            EnemyAnimator.Play("Laser_Appear_End");
            await WaitForAnimation();


            await Task.Delay(500);

            Invoke("StopAttacking", 0.2f);
        }

    }


    public async Task EruptionAttack()
    {
        if(!isAttacking)
        {
            isAttacking=true;


            EnemyAnimator.Play("Eruption_Disappear");
            await WaitForAnimation();

            enemy.position = new Vector2(0f, 4f);

            EnemyAnimator.Play("Eruption_Appear");
            await WaitForAnimation();

            EnemyAnimator.Play("Eruption_Idle");
            EruptionTeleportParticle.Play();

            await Task.Delay(1000);

            for (int i =0; i<7; i++)
            {
                float PlayerXPos = Player.transform.position.x;
                await Task.Delay(300);
                GameObject eruption = Instantiate(EruptionAttackParticle, new Vector3(PlayerXPos, -9.5f, -9f), Quaternion.identity);
                ParticleSystem part = eruption.GetComponentInChildren<ParticleSystem>();
                Camera.Play("Moderate_Shake");
                part.Play();
                await Task.Delay(1200);
                Destroy(eruption);
            }

            EruptionTeleportParticle.Stop();
            EnemyAnimator.StopPlayback();

            EnemyAnimator.Play("Eruption_Disappear_End");
            await WaitForAnimation();

            enemy.position = new Vector2(0f, -5f);

            EnemyAnimator.Play("Eruption_Appear_End");
            await WaitForAnimation();

            await Task.Delay(500);

            Invoke("StopAttacking", 0.2f);

        }
    }


    private void StopAttacking()
    {
        EnemyAnimator.StopPlayback();
        isAttacking = false;
    }

    private async Task WaitForAnimation()
    {
        int duration = (int)EnemyAnimator.GetCurrentAnimatorStateInfo(0).length;
        await Task.Delay(duration * 1000);
        EnemyAnimator.StopPlayback();
    }

}
