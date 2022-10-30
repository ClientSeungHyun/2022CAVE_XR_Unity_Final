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


    //������ ����
    private void GrabCheck()
    {

        Leftf = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);       //�޼� ��ư �Է°�

        Rightf = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);     //������ ��ư �Է°�
        if (Leftf > 0.9)
        {
            isLeftGrab = true;    //��ư ������ true��
        }
        else if (Rightf > 0.9)
        {
            isRightGrab = true;    //��ư ������ true��
        }
        else
        {
            isLeftGrab = isRightGrab = false;   //��ư ���� false
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hidePos")  //�� �ȿ� ����
        {
            if (isRightGrab == true || isLeftGrab == true)
            {
                if (player.GetComponent<PlayerControl>().isHaveLastKey == true && isHideDone == false) //������ Ű�� ���� ���¿����� ����
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
                
                if(GameObject.Find("Player").GetComponent<PlayerControl>().isHaveLastKey == true)   //1�� ������ �κ�
                {
                    //GameObject.Find("GamaManager").GetComponent<GamaManager>().monsterApp(1);
                    isLastDoorOpen = true;
                    player.GetComponent<PlayerControl>().showMessage("�� ���谡 �ƴϾ�..! ����..");

                }
                else
                {
                    player.GetComponent<PlayerControl>().showMessage("���� ����. ���谡 �ʿ���..");
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

        if (other.gameObject.tag == "Item")  //Item tag�� �͸� �ε������� �۵�
        {

            if (isRightGrab == true || isLeftGrab == true)
            {
                Destroy(other.gameObject);  //���� ����
            }

        }
        if (other.gameObject.tag == "Key")  //Item tag�� �͸� �ε������� �۵�
        {
            Debug.Log(isRightGrab);
            if (isRightGrab == true || isLeftGrab == true)
            {
                getKey = true;
                Destroy(other.gameObject);  //���� ����
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
