using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator spriteAnimator;

    [Header("Scripts")]
    public PlayerMovement movement = new PlayerMovement();
    public PlayerAnimationController Animations = new PlayerAnimationController();

    [Header("ParticleSystem")]
    public ParticleSystem walkParticles;


    [Header("Attributes")]
    public float movementSpeed;

    [Header("NPCs")]
    public LayerMask interactableNPCLayer;

    public bool isWalking;
    public bool isFlipped;

    public List<InteractableNPC> interactableNPCs = new List<InteractableNPC>();

    public enum PlayerState
    {
        Normal,
        inEvent
    }

    private PlayerState currentState = PlayerState.Normal;


    private void Update()
    {
        switch (currentState)
        {
            case PlayerState.Normal:
                movement.Move(this);
                Walk();
                Flip();
                break;
            case PlayerState.inEvent:
                break;
        }

    }

    private void Walk()
    {
        if (rb.velocity.x == 0)
        {
            isWalking = false;
            walkParticles.Stop();
            Animations.Idle(this);
        }
        else
        {
            if (!isWalking)
            {
                isWalking = true;
                Animations.Walk(this);
                walkParticles.Play();
            }
        }
    }

    private void Flip()
    {
        if(rb.velocity.x < 0 && !isFlipped)
        {
            isFlipped = true;
            transform.Rotate(0f, 180f, 0f);
        }
        if(rb.velocity.x > 0 && isFlipped)
        {
            transform.Rotate(0f, -180f, 0f);
            isFlipped = false;
        }
    }

    public void ChangeState(PlayerState state)
    {
        currentState = state;
    }
    
}
