using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    float dirX;

    public void Move(PlayerManager player)
    {
        dirX = Input.GetAxisRaw("Horizontal");
        player.rb.velocity = new Vector2(dirX * player.movementSpeed, player.rb.velocity.y);
    }

}
