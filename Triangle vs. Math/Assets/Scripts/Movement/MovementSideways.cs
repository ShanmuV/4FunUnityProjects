using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovementSideWays : Movement
{

    [SerializeField] private float speed = 10f;


    private float dir;

    public override void UpdateMovement(PlayerManager player)
    {
        dir = Input.GetAxisRaw("Horizontal");
        if((dir > 0 && player.transform.position.x < player.currentPath.rightEdge.position.x) || (dir < 0 && player.transform.position.x > player.currentPath.leftEdge.position.x))
        {
            player.transform.position = new Vector3(player.transform.position.x + speed * dir * Time.deltaTime, player.transform.position.y, 0);
        }
    }

}
