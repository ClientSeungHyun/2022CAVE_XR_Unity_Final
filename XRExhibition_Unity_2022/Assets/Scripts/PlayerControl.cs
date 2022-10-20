using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController; // ���� ĳ���Ͱ� �������ִ� ĳ���� ��Ʈ�ѷ� �ݶ��̴�
    private Vector3 moveDirection;   //�����̴� ����
    private Animation MoveAnim; //�ִϸ��̼� ���� 

    private float gravity = -9.8f;

    public bool isground;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        MoveAnim = transform.Find("OVRCameraRig").GetComponent<Animation>();//�ִϸ��̼� ������Ʈ ȣ��
        //DontDestroyOnLoad(this);
        
    }

    // Update is called once per frame
    void Update()
    {

        UseGravity();
        WalkShake();
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
                MoveAnim.Play("camera_move");//�ִϸ��̼� ��� 
            }
            if (thumbstick.y < 0)//�Ʒ� ���̽�ƽ ������
            {
                MoveAnim.Play("camera_move");//�ִϸ��̼� ��� 
            }
            if (thumbstick.y > 0)//�� ���̽�ƽ ������
            {
                MoveAnim.Play("camera_move");//�ִϸ��̼� ��� 
            }

        }
    }


}
