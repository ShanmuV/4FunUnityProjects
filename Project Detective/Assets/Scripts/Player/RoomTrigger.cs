using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Vector2 positionInNextRoom;

    private void Start()
    {
        if(GameManager.instance != null)
        {
            gameManager = GameManager.instance;
        }
        else
        {
            Debug.LogError("GameManager Singleton Not found");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameManager.LoadRoom(sceneToLoad, positionInNextRoom);
        }
    }
}
