using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public HandControler leftHand, rightHand;

    private CharacterController characterController; // ���� ĳ���Ͱ� �������ִ� ĳ���� ��Ʈ�ѷ� �ݶ��̴�
    private Vector3 moveDirection;   //�����̴� ����
    private Animation MoveAnim; //�ִϸ��̼� ���� 
    public AudioSource playerSound;
    public AudioClip []playerClip;

    public bool isMove;
    public bool isground;
    public bool isHaveKey;
    public bool isHiding;

    public static bool isHaveLastKey;
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

        characterController = GetComponent<CharacterController>();
        MoveAnim = transform.Find("OVRCameraRig").GetComponent<Animation>();//�ִϸ��̼� ������Ʈ ȣ��
        playerSound = GetComponent<AudioSource>();
        playerSound.loop = false;

        isMove = false;
        isHaveKey = false;
        isHaveLastKey = false;
        isHiding = false;

        preScene = 0;
        nowScene = 0;


    }

    // Update is called once per frame
    void Update()
    {

        //setKey();
        UseGravity();
        WalkShake();
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
        //print(characterController);
        isground = characterController.isGrounded;
        if (characterController.isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * 5.0f * Time.deltaTime);
    }

    public void WalkShake()
    {
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))//���� ���̽�ƽ �Է¹ޱ�
        {
            Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            if (thumbstick.x < 0)//���� ���̽�ƽ ������
            {

            }
            if (thumbstick.x > 0)//������ ���̽�ƽ ������
            {

            }
            if (thumbstick.y < 0)//�Ʒ� ���̽�ƽ ������
            {

            }
            if (thumbstick.y > 0)//�� ���̽�ƽ ������
            {

            }

        }
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1.0f);//FadeOut�ɵ��� 3�� ������ 
        LodingSceneControlScr.LoadScene(sceneName);//�� �ε� 
        if (preScene == 0)
        {
            playerSound.clip = playerClip[0];
            playerSound.Play();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnterDoor")
        {
            print("�Ա� ����");
            leftHand.isIn = rightHand.isIn = false;
            preScene = nowScene;
            nowScene = 1;
            sceneName = "FirstFloor";
            StartCoroutine(changeScene());
        }
        if (other.tag == "First")
        {
            preScene = nowScene;
            nowScene = 1;
            sceneName = "FirstFloor";
            StartCoroutine(changeScene());

            playerSound.clip = playerClip[2];
            playerSound.Play();
        }
        if (other.tag == "Second")
        {
            preScene = nowScene;
            nowScene = 2;
            sceneName = "SecondFloor";
            StartCoroutine(changeScene());

            playerSound.clip = playerClip[2];
            playerSound.Play();
        }
        if (other.tag == "Third")
        {
            print("3��");
            preScene = nowScene;
            nowScene = 3;
            sceneName = "ThirdFloor";
            StartCoroutine(changeScene());

            playerSound.clip = playerClip[2];
            playerSound.Play();
        }
        
        
    }
}
