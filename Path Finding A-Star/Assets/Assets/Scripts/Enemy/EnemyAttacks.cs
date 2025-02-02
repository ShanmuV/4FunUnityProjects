using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour 
{

    [SerializeField] Animator SwordAnimationController;
    [SerializeField] Animator EnemyAnimationController;
    [SerializeField] Collider2D SwordCollider;
    [SerializeField] TrailRenderer SwordTrailRenderer;



    public bool isAttacking = false;



    public void SwordAttack()
    {
        if (!isAttacking)
        {
            StartCoroutine(_SwordAttack());
        }

    }

    public void LaserAttack()
    {

    }

    private IEnumerator _LaserAttack()
    {
        EnemyAnimationController.Play("Enemy_Laser_Teleport");
        float duration = EnemyAnimationController.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration/2);
        transform.position = new Vector3(0f, 6f);
        yield return new WaitForSeconds(duration/2);
        

    }

    public void StopAttacking()
    {
        isAttacking=false;
    }

    private IEnumerator _SwordAttack()
    {
        isAttacking = true;
        SwordCollider.enabled = true;
        SwordTrailRenderer.enabled = true;
        SwordAnimationController.Play("Sword_Attack");
        yield return new WaitForSeconds(0.5f);
        SwordAnimationController.Play("Sword_Idle");
        SwordCollider.enabled = false;
        SwordTrailRenderer.enabled = false;
        StopAttacking();
    }
}
