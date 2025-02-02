using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] Animator SwordAnimator;

    [Header("Laser Attack Animation")]
    [SerializeField] GameObject LaserAttack;
    private Animator laserAnimator;

    private Vector3 positionOfLaserAttack = new Vector3(-25, 7, 0);

    public async Task PlayLaserAnimation()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject currentLaser = Instantiate(LaserAttack, positionOfLaserAttack, Quaternion.identity);
            positionOfLaserAttack.y -= 3f;
            laserAnimator = currentLaser.GetComponent<Animator>();
            laserAnimator.Play("Laser");
            int duration =(int) (laserAnimator.GetCurrentAnimatorStateInfo(0).length) * 1000;
            await Task.Delay(duration);
            laserAnimator.StopPlayback();
            Destroy(currentLaser);
        }
    }

    private IEnumerator _PlayLaserAnimation()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject currentLaser = Instantiate(LaserAttack, positionOfLaserAttack, Quaternion.identity);
            positionOfLaserAttack.y -= 3f;
            laserAnimator = currentLaser.GetComponent<Animator>();
            laserAnimator.Play("Laser");
            float duration = laserAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(duration);
            Destroy(currentLaser);
        }
    }
}
