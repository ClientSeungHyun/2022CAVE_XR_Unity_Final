using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public HandControler leftHand, rightHand;
    public SoundManager soundManager;
    private CharacterController characterController; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더
    private Vector3 moveDirection;   //움직이는 방향
    private Animation MoveAnim; //애니메이션 변수 
    public AudioSource playerSound;
    public AudioClip []playerClip;
    public GameObject messageCanvas;
    public TextMeshProUGUI messageText;


    public bool isMove;
    public bool isground;
    public bool isHaveKey;
    public bool isHiding;
    public bool hideOver;
    public bool isLastDoorOpen;

    public bool isHaveLastKey;
    public int preScene;
    public int nowScene;
    private float gravity = -9.8f;
    private string sceneName;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<OVRPlayerController>().Acceleration = 0;
        characterController = GetComponent<CharacterController>();
        MoveAnim = transform.Find("OVRCameraRig").GetComponent<Animation>();//애니메이션 컴포넌트 호출
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        playerSound = GetComponent<AudioSource>();
        playerSound.loop = false;

        isMove = false;
        isHaveKey = false;
        isHaveLastKey = false;
        isHiding = false;
        hideOver = false;
        isLastDoorOpen = false;

        preScene = 0;
        nowScene = 0;

        messageCanvas.SetActive(false);
        GetComponent<OVRPlayerController>().Acceleration = 0;   //가속도를 0으로 해서 못 움직이게

        

    }

    // Update is called once per frame
    void Update()
    {

        if(leftHand.isLastDoorOpen == true || rightHand.isLastDoorOpen == true)
        {
            isLastDoorOpen = leftHand.isLastDoorOpen = rightHand.isLastDoorOpen = true;
            GetComponent<OVRPlayerController>().Acceleration = 0;   //가속도를 0으로 해서 못 움직이게
        }
        setKey();
        UseGravity();
    }

    public void showMessage(string s)
    {
        messageCanvas.SetActive(true);
        messageText.text = s;
        Invoke("deleteMessage", 3f);
    }
    public void deleteMessage()
    {
        messageCanvas.SetActive(false);
    }

    public void setKey()    //키 존재 유무
    {
        if(leftHand.isKey() || rightHand.isKey())
        {
            isHaveKey = true;
            
        }
        else
        {
            isHaveKey = false;
        }
        leftHand.setKey(isHaveKey);
        rightHand.setKey(isHaveKey);
    }

    public void UseGravity()
    {
        isground = characterController.isGrounded;
        if (characterController.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * 5.0f * Time.deltaTime);
    }

    void changeScene()
    {
        LodingSceneControlScr.LoadScene(sceneName);
        if (preScene == 0)
        {
            playerSound.loop = false;
            playerSound.clip = playerClip[0];
            playerSound.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnterDoor")
        {
            preScene = nowScene;
            nowScene = 1;
            sceneName = "FirstFloor";
            this.gameObject.GetComponent<CharacterController>().stepOffset = 0.3f;
            changeScene();
        }
        if (other.tag == "First")
        {
            if (isHaveLastKey && !hideOver)
            {
                showMessage("괴물이 오고 있어 숨어야해!!");
            }
            else
            {
                preScene = nowScene;
                nowScene = 1;
                sceneName = "FirstFloor";
                changeScene();

                playerSound.clip = playerClip[1];
                playerSound.Play();
            }
        }
        if (other.tag == "Second")
        {


            preScene = nowScene;
            nowScene = 2;
            sceneName = "SecondFloor";
            changeScene();

            playerSound.clip = playerClip[1];
            playerSound.Play();

        }
        if (other.tag == "Third")
        {
            preScene = nowScene;
            nowScene = 3;
            sceneName = "ThirdFloor";
            changeScene();

            playerSound.clip = playerClip[1];
            playerSound.Play();
        }
        
        
    }
}
