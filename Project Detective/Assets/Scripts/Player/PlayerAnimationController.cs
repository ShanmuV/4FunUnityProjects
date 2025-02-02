using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController 
{
    public void Walk(PlayerManager player)
    {
        player.spriteAnimator.Play("Walk");
        player.walkParticles.Play();
    }
    public void Idle(PlayerManager player)
    {
        player.spriteAnimator.Play("Idle");
        player.walkParticles.Stop();
    }
}
