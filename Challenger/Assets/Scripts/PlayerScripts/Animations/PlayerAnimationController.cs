using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator PlayerAnimator;

    bool isAnimating;

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }
    public async void Invincibilty(int layer)
    {
        if (!isAnimating)
        {
            isAnimating = true;
            Physics2D.IgnoreLayerCollision(layer, 9, true);
            PlayerAnimator.Play("Player_Invincibility");
            float duration = PlayerAnimator.GetCurrentAnimatorStateInfo(0).length;
            await WaitForAnimation();
            Physics2D.IgnoreLayerCollision(layer, 9, false);
            isAnimating = false;
        }
    }

    private async Task WaitForAnimation()
    {
        int duration = (int)PlayerAnimator.GetCurrentAnimatorStateInfo(0).length;
        await Task.Delay(duration * 1000);
    }
}
