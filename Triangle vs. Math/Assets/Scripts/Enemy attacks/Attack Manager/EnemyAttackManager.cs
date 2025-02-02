using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerManager player;

    [Header("Normal Digit Attacks")]
    [SerializeField] int attackGap;
    [SerializeField] List<GameObject> normalAttacks = new List<GameObject>();

    [Header("Tan Attack")]
    [SerializeField] GameObject tanAttack;

    [Header("Integral")]
    [SerializeField] GameObject integralAttack;


    public void DoNormalAttack()
    {
        float positionX = GetRandomPosition();
        int x = Random.Range(0, normalAttacks.Count);
        Instantiate(normalAttacks[x], player.currentPath.transform.position + player.currentPath.transform.TransformDirection(new Vector3(positionX, 6.5f,0f)), player.currentPath.transform.rotation);
    }

    public void DoTanAttack()
    {
        float positionX = GetRandomPosition();
        Instantiate(tanAttack, player.currentPath.transform.position + player.currentPath.transform.TransformDirection(new Vector3(positionX, 6.5f, 0f)), player.currentPath.transform.rotation);
    }

    private float GetRandomPosition()
    {
        int min = (int)player.currentPath.leftEdge.localPosition.x;
        int max = (int)player.currentPath.rightEdge.localPosition.x;
        int num_of_possibilities = ((max - min) / attackGap) + 1;
        int randomIndex = Random.Range(0, num_of_possibilities);

        return min + (attackGap * randomIndex);
    }

    public void DoIntegralAttack()
    {
        float positionX = GetRandomPosition();
        Instantiate(integralAttack, player.currentPath.transform.position + player.currentPath.transform.TransformDirection(new Vector3(positionX, 6.5f, 0f)), player.currentPath.transform.rotation);
    }
}
