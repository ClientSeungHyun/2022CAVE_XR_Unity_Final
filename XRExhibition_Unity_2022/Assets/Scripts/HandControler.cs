using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class HandControler : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public GameObject MonsterHead;
    private GameObject hidPosObj;
    public GameObject Monster;

    private bool boxOpen = false;
    private float Leftf, Rightf;
    private bool isLeftGrab;
    private bool isRightGrab;
    private bool getKey = false;
    public bool isLastDoorOpen;
    public bool isHideDone;

    private void Awake()
    {
        player =GameObject.Find("Player");
        MonsterHead = GameObject.Find("Head");
        hidPosObj = GameObject.Find("hidePos");
        Monster = GameObject.Find("Monster");
        isLeftGrab = false;
        isRightGrab = false;
        isLastDoorOpen = false;
        isHideDone = false;
    }

    // Update is called once per frame

    void Update()
    {
        GrabCheck();

    }

    public bool isKey()
    {
        return getKey;
    }

    public void setKey(bool key)
    {
        getKey = key;
    }


    //아이템 습득
    private void GrabCheck()
    {

        Leftf = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);       //왼손 버튼 입력값

        Rightf = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);     //오른손 버튼 입력값
        if (Leftf > 0.9)
        {
            isLeftGrab = true;    //버튼 눌리면 true로
        }
        else if (Rightf > 0.9)
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
        if (other.gameObject.tag == "hidePos")  //통 안에 숨기
        {
            if (isRightGrab == true || isLeftGrab == true)
            {
                if (player.GetComponent<PlayerControl>().isHaveLastKey == true && isHideDone == false) //마지막 키를 얻은 상태에서만 가능
                {
                    GameObject.Find("Player").GetComponent<PlayerControl>().isHiding = true;
                    isHideDone = true;
                }

            }
            
        }
        if (other.gameObject.tag == "lastDoor")
        {
            if (isRightGrab == true || isLeftGrab == true)
            {
                
                if(GameObject.Find("Player").GetComponent<PlayerControl>().isHaveLastKey == true)   //1층 마무리 부분
                {
                    //GameObject.Find("GamaManager").GetComponent<GamaManager>().monsterApp(1);
                    isLastDoorOpen = true;
                    player.GetComponent<PlayerControl>().showMessage("이 열쇠가 아니야..! 젠장..");

                }
                else
                {
                    player.GetComponent<PlayerControl>().showMessage("문이 잠겼다. 열쇠가 필요해..");
                }
            }
        }
        if (other.gameObject.tag == "LastKey")
        {
            if(boxOpen == true)
            {
                if (isRightGrab == true || isLeftGrab == true)
                {
                    GameObject.Find("Player").GetComponent<PlayerControl>().isHaveLastKey = true;
                    Destroy(other.gameObject);
                }
            }
            
        }

        if (other.gameObject.tag == "Item")  //Item tag인 것만 부딪혔을때 작동
        {

            if (isRightGrab == true || isLeftGrab == true)
            {
                Destroy(other.gameObject);  //물건 삭제
            }

        }
        if (other.gameObject.tag == "Key")  //Item tag인 것만 부딪혔을때 작동
        {
            Debug.Log(isRightGrab);
            if (isRightGrab == true || isLeftGrab == true)
            {
                getKey = true;
                Destroy(other.gameObject);  //물건 삭제
            }

        }

        if (other.gameObject.tag == "Box")
        {
            if(getKey == true)
            {
                if (isRightGrab == true || isLeftGrab == true)
                {
                    if (animator != null)
                    {
                        animator.SetBool("Opening", true);
                        getKey = false;
                        boxOpen = true;
                    }
                }

            }

        }


    }


}
