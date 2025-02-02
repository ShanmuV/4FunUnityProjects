using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovementUpwards : Movement
{

    [SerializeField] private float speed = 10f;


    private float dir;

    public override void UpdateMovement(PlayerManager player)
    {
        dir = Input.GetAxisRaw("Vertical");
        if((dir > 0 && player.transform.position.y < player.currentPath.leftEdge.position.y) || (dir<0 && player.transform.position.y > player.currentPath.rightEdge.position.y))
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + speed * dir * Time.deltaTime, 0);
        }
    }

}
