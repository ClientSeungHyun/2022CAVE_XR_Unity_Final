using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControl : MonoBehaviour
{
    public HandControler leftHand, rightHand;
    public SoundManager soundManager;
    private CharacterController characterController; // ���� ĳ���Ͱ� �������ִ� ĳ���� ��Ʈ�ѷ� �ݶ��̴�
    private Vector3 moveDirection;   //�����̴� ����
    private Animation MoveAnim; //�ִϸ��̼� ���� 
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
        MoveAnim = transform.Find("OVRCameraRig").GetComponent<Animation>();//�ִϸ��̼� ������Ʈ ȣ��
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
        GetComponent<OVRPlayerController>().Acceleration = 0;   //���ӵ��� 0���� �ؼ� �� �����̰�

        

    }

    // Update is called once per frame
    void Update()
    {

        if(leftHand.isLastDoorOpen == true || rightHand.isLastDoorOpen == true)
        {
            isLastDoorOpen = leftHand.isLastDoorOpen = rightHand.isLastDoorOpen = true;
            GetComponent<OVRPlayerController>().Acceleration = 0;   //���ӵ��� 0���� �ؼ� �� �����̰�
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

    public void setKey()    //Ű ���� ����
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
                showMessage("������ ���� �־� �������!!");
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
