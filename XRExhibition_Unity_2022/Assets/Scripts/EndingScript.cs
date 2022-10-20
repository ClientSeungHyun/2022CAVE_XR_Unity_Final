using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    private float Leftf, Rightf;
    private bool isLeftGrab;
    private bool isRightGrab;

    // Update is called once per frame
    void Update()
    {
        GrabCheck();
        if(isLeftGrab == true || isRightGrab == true)
        {
            SceneManager.LoadScene("Outdoor");
        }
    }

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
}
