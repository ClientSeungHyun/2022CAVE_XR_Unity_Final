using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    private LineRenderer laser;
    private RaycastHit Hit_obj;

    //private float buttonFloat_L, buttonFloat_R;
   // private bool buttonPush_L = false, buttonPush_R = false;
    // Start is called before the first frame update
    void Start()
    {
        laser = this.gameObject.AddComponent<LineRenderer>();
        //레이저 연결점 개수
        laser.positionCount = 2;
        //레이저 굵기
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
        laser.material.color = Color.white;
        laser.enabled = true;
    }

    void Update()
    {
        //Debug.Log(transform.forward);
        if(GameObject.Find("GameManager").GetComponent<GamaManager>().laserShow == true)
        {
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, transform.position + (transform.forward * 0.8f));
        }
        else
        {
            laser.enabled = false;
        }

        //버튼 입력 받는 함수
       /* GetButton();

        if (Physics.Raycast(transform.position, transform.forward, out Hit_obj))
        {
            //Debug.Log(Hit_obj.collider.name);

            //버튼에 Ray가 닿을 시
            if (Hit_obj.collider.gameObject.CompareTag("Button"))
            {

                //컨트롤러가 눌렸을 경우
                if (buttonPush_L == true || buttonPush_R == true)
                {
                    //버튼 실행
                    Hit_obj.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }

            }
        }*/
    }
   /* private void GetButton()
    {
        //컨트롤러의 버튼 입력 받기(float형태)
        buttonFloat_L = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        buttonFloat_R = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        //입력받은 실수를 bool값으로 전환
        if (buttonFloat_L > 0.9f)
        {
            buttonPush_L = true; //왼쪽
            Debug.Log("왼눌림");
        }
        else if (buttonFloat_L <= 0.9f)
        {
            buttonPush_L = false;
            // Debug.Log("왼 떼짐");
        }

        if (buttonFloat_R > 0.9f)
        {
            buttonPush_R = true;    //오른쪽
                                    // Debug.Log("오눌림");
        }
        else if (buttonFloat_R <= 0.9f)
        {
            buttonPush_R = false;
            //Debug.Log("오 떼짐");
        }

    }
   */
   
}
