using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController characterController; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더
    private Vector3 moveDirection;   //움직이는 방향
    private Animation MoveAnim; //애니메이션 변수 

    private float gravity = -9.8f;

    public bool isground;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        MoveAnim = transform.Find("OVRCameraRig").GetComponent<Animation>();//애니메이션 컴포넌트 호출
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
        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))//왼쪽 조이스틱 입력받기
        {
            Vector2 thumbstick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            if (thumbstick.x < 0)//왼쪽 조이스틱 움직임
            {
                MoveAnim.Play("camera_move");//애니메이션 재생 
            }
            if (thumbstick.x > 0)//오른쪽 조이스틱 움직임
            {
                MoveAnim.Play("camera_move");//애니메이션 재생 
            }
            if (thumbstick.y < 0)//아래 조이스틱 움직임
            {
                MoveAnim.Play("camera_move");//애니메이션 재생 
            }
            if (thumbstick.y > 0)//위 조이스틱 움직임
            {
                MoveAnim.Play("camera_move");//애니메이션 재생 
            }

        }
    }


}
