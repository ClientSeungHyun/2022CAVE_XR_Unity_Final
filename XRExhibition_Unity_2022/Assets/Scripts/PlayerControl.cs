using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public HandControler leftHand, rightHand;

    private CharacterController characterController; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더
    private Vector3 moveDirection;   //움직이는 방향
    private Animation MoveAnim; //애니메이션 변수 
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
        MoveAnim = transform.Find("OVRCameraRig").GetComponent<Animation>();//애니메이션 컴포넌트 호출
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
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))//왼쪽 조이스틱 입력받기
        {
            Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            if (thumbstick.x < 0)//왼쪽 조이스틱 움직임
            {

            }
            if (thumbstick.x > 0)//오른쪽 조이스틱 움직임
            {

            }
            if (thumbstick.y < 0)//아래 조이스틱 움직임
            {

            }
            if (thumbstick.y > 0)//위 조이스틱 움직임
            {

            }

        }
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1.0f);//FadeOut될동안 3초 딜레이 
        LodingSceneControlScr.LoadScene(sceneName);//씬 로드 
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
            print("입구 닿음");
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
            print("3층");
            preScene = nowScene;
            nowScene = 3;
            sceneName = "ThirdFloor";
            StartCoroutine(changeScene());

            playerSound.clip = playerClip[2];
            playerSound.Play();
        }
        
        
    }
}
