using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControler : MonoBehaviour
{
    public GameObject door;

    private float Leftf,Rightf;
    private bool isLeftGrab;
    private bool isRightGrab;
    private bool getKey=false;

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
            print("asdf");
        }
        else if(Rightf > 0.9)
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
        if(other.gameObject.tag == "Item")  //Item tag�� �͸� �ε������� �۵�
        {
           
            if(isRightGrab == true || isLeftGrab == true)
            {
                Destroy(other.gameObject);  //���� ����
            }
           
        }
        if (other.gameObject.tag == "Key")  //Item tag�� �͸� �ε������� �۵�
        {

            if (isRightGrab == true || isLeftGrab == true)
            {
                getKey = true;
                Destroy(other.gameObject);  //���� ����
            }

        }
        if(getKey == true)  //Ű�� ���� �� �� �浹
        {
            if (other.gameObject.tag == "Door")
            {
                if (isRightGrab == true || isLeftGrab == true)
                {
                    door.transform.eulerAngles = new Vector3(0, 180, 0);
                }

            }
        }
       
    }
}
