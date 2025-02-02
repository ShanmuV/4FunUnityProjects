using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private MainCamera mainCamera;
    [SerializeField] private NewPlayerMovement playerMovement;
    [SerializeField] private NewPlayerManager playerManager;
    [SerializeField] private Animator sceneAnimator;
    [SerializeField] private Animator roomCardAnimator;
    [SerializeField] private TextMeshProUGUI roomCard;

    private Coroutine roomCardCoroutine;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        roomCard.text = SceneManager.GetActiveScene().name;
        roomCardCoroutine = StartCoroutine(RoomCardAnimation());
    }

    private void OnDisable()
    {
        if(roomCardCoroutine != null)
        {
            StopCoroutine(roomCardCoroutine);
            roomCardCoroutine = null;
        }
    }


    private IEnumerator RoomCardAnimation()
    {
        roomCardAnimator.Play("RoomCardFadeIn");
        yield return new WaitForSeconds(2f);
        roomCardAnimator.Play("RoomCardFadeOut");
        roomCardCoroutine = null;
    }



    private void Start()
    {
        if(NewPlayerManager.instance != null)
        {
            playerManager = NewPlayerManager.instance;
            playerMovement = playerManager.playerMovement;
            sceneAnimator = GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("No Player Movement Singleton found");
        }

        if(MainCamera.instance != null)
        {
            mainCamera = MainCamera.instance;
        }
        else
        {
            Debug.LogError("Main Camera Singleton isnt found");
        }
    }

    public void LoadRoom(string roomName, Vector2 positionInNextRoom)
    {
        if(roomCardCoroutine != null)
        {
            StopCoroutine(roomCardCoroutine);
            roomCardCoroutine = null;
        }
        StartCoroutine(LoadRoomCrossFade(roomName, positionInNextRoom));
    }

    private IEnumerator LoadRoomCrossFade(string roomName, Vector2 positionInNextRoom)
    {
        playerMovement.StopMoving();
        sceneAnimator.Play("Fade_Out");
        yield return new WaitForSeconds(0.5f);

        roomCard.text = roomName;
        SceneManager.LoadScene(roomName);
        roomCardCoroutine = StartCoroutine(RoomCardAnimation());
        playerManager.transform.position = positionInNextRoom;
        mainCamera.transform.position = positionInNextRoom;

        sceneAnimator.Play("Fade_In");
        yield return new WaitForSeconds(0.25f);
        playerMovement.StartMoving();
    }
    
}
