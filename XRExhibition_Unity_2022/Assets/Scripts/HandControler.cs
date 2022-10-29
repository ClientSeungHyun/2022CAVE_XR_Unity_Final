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
    

    public bool isIn;

    private void Awake()
    {
        player =GameObject.Find("Player");
        MonsterHead = GameObject.Find("Head");
        hidPosObj = GameObject.Find("hidePos");
        Monster = GameObject.Find("Monster");
        isLeftGrab = false;
        isRightGrab = false;
        isIn = false;
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
            //print("asdf");
            Debug.Log("�޼� ����");
        }
        else if (Rightf > 0.9)
        {
            Debug.Log("���� �� ����");
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
                if (player.GetComponent<PlayerControl>().isHaveLastKey == true) //������ Ű�� ���� ���¿����� ����
                {
                    GameObject.Find("Player").GetComponent<PlayerControl>().isHiding = true;
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
                    Monster.transform.LookAt(player.transform.position);
                    Vector3 dir = MonsterHead.transform.position - player.transform.position;
                    player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 3);
                    Monster.GetComponent<MonsterController>().chasePlayer();
                    
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



        if (other.gameObject.tag == "Door")
        {
            print("�� ����");
            if (isRightGrab == true || isLeftGrab == true)
            {
                print("�� ����");
                if (getKey)
                    other.transform.parent.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        if (other.gameObject.tag == "Box")
        {
            //print("asdf");
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
