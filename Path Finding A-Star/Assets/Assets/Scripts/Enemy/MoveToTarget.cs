using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] GridMap gridMap;
    [SerializeField] LayerMask platforms;

    private Coroutine coroutine;
    private EnemyMovement enemyMovement;


    private bool movingEnemy = false;

    // Start is called before the first frame update
    private void Start()
    {

        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    /*private void Update()
    {
        if(!movingEnemy)
        {
            coroutine = StartCoroutine(MoveEnemy());
            movingEnemy = true;
        }

    }*/

    public IEnumerator MoveEnemy(GridMap gridMap)
    {
        while (gridMap.path != null && gridMap.path.Count > 0)
        {
            Node current = gridMap.path[0];
            gridMap.path.RemoveAt(0);

            

            if(transform.localPosition.x < current.GetCenterPosition().x)
            {
                enemyMovement.MoveRight();
            }
            else if(transform.localPosition.x > current.GetCenterPosition().x)
            {
                enemyMovement.MoveLeft();
            }
            if (current.worldPos.y > gameObject.transform.position.y && !IsWallAbove())
            {
                //Debug.Log("Current: "+current.worldPos.y+"\nEnemy: "+transform.position.y);
                enemyMovement.Jump();
                yield return new WaitForSeconds(0.7f);
            }

        }
        Invoke("StopmovingEnemy", 0.2f);

    }


    private void StopmovingEnemy()
    {
        movingEnemy=false;
    }

    private bool IsWallAbove()
    {
        //Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 5f),Color.white,0.1f);
        return Physics2D.Raycast(transform.position, Vector2.up, 5f, platforms);
    }

    public void StartMovingEnemy(GridMap gridMap)
    {
        if (!movingEnemy)
        {
            coroutine = StartCoroutine(MoveEnemy(gridMap));
            movingEnemy = true;
        }
    }
}
