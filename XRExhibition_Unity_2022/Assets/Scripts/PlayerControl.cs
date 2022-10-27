using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public HandControler leftHand, rightHand;

    private CharacterController characterController; // ���� ĳ���Ͱ� �������ִ� ĳ���� ��Ʈ�ѷ� �ݶ��̴�
    private Vector3 moveDirection;   //�����̴� ����
    private Animation MoveAnim; //�ִϸ��̼� ���� 

    public bool isground;
    public bool isHaveKey;
    public int nowFloor;
    private float gravity = -9.8f;
    private string sceneName;
   

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        leftHand = transform.Find("CustomHandLeft").GetComponent<HandControler>();
        rightHand = transform.Find("CustomHandRight").GetComponent<HandControler>();
        
        MoveAnim = transform.Find("OVRCameraRig").GetComponent<Animation>();//�ִϸ��̼� ������Ʈ ȣ��
       
        DontDestroyOnLoad(this);
        isHaveKey = false;

    }

    // Update is called once per frame
    void Update()
    {
        print(characterController);
        setKey();

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
                MoveAnim.Play("camera_move");//�ִϸ��̼� ��� 
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

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "First")
        {
            sceneName = "FirstFloor";
            StartCoroutine(changeScene());
        }
        if (other.tag == "Second")
        {
            sceneName = "SecondFloor";
            StartCoroutine(changeScene());
        }
        if (other.tag == "Third")
        {
            sceneName = "ThirdFloor";
            StartCoroutine(changeScene());
        }
        

    }
}
