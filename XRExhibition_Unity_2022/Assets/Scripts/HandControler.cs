using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControler : MonoBehaviour
{
    private float Leftf,Rightf;
    private bool isLeftGrab;
    private bool isRightGrab;
    public GameObject door;

    private void Awake()
    {
        isLeftGrab = false;
        isRightGrab = false;
    }

    // Update is called once per frame
    void Update()
    {
        GrabCheck();
        if (Input.GetKeyDown(KeyCode.Space))
            door.transform.eulerAngles = new Vector3(0, 180, 0);
    }


    //아이템 습득
    private void GrabCheck()
    {
        Leftf = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);       //왼손 버튼 입력값
        Rightf = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);     //오른손 버튼 입력값
        if (Leftf > 0.9)
        {
            isLeftGrab = true;    //버튼 눌리면 true로
            print("asdf");
        }
        else if(Rightf > 0.9)
        {
            isRightGrab = true;    //버튼 눌리면 true로
        }
        else
        {
            isLeftGrab = isRightGrab = false;   //버튼 떼면 false
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")  //Item tag인 것만 부딪혔을때 작동
        {
           
            if(isRightGrab == true || isLeftGrab == true)
            {
                Destroy(other.gameObject);  //물건 삭제
            }
           
        }
        if(other.gameObject.tag == "Door")
        {
            if (isRightGrab == true || isLeftGrab == true)
            {
                door.transform.eulerAngles = new Vector3(0, 180, 0);
            }

        }
    }
}
