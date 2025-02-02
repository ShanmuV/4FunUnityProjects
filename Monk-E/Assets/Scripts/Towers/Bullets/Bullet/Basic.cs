using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 3f;
    [SerializeField] private float damage = 5f;
    private int dir;

    void Start()
    {
        if(transform.position.x < 0)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(1, 0, 0) * dir * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Name: "+collision.gameObject.name);
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Soldier>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
