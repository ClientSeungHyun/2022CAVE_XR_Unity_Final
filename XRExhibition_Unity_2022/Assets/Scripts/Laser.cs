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
        //������ ������ ����
        laser.positionCount = 2;
        //������ ����
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

        //��ư �Է� �޴� �Լ�
       /* GetButton();

        if (Physics.Raycast(transform.position, transform.forward, out Hit_obj))
        {
            //Debug.Log(Hit_obj.collider.name);

            //��ư�� Ray�� ���� ��
            if (Hit_obj.collider.gameObject.CompareTag("Button"))
            {

                //��Ʈ�ѷ��� ������ ���
                if (buttonPush_L == true || buttonPush_R == true)
                {
                    //��ư ����
                    Hit_obj.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }

            }
        }*/
    }
   /* private void GetButton()
    {
        //��Ʈ�ѷ��� ��ư �Է� �ޱ�(float����)
        buttonFloat_L = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        buttonFloat_R = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        //�Է¹��� �Ǽ��� bool������ ��ȯ
        if (buttonFloat_L > 0.9f)
        {
            buttonPush_L = true; //����
            Debug.Log("�޴���");
        }
        else if (buttonFloat_L <= 0.9f)
        {
            buttonPush_L = false;
            // Debug.Log("�� ����");
        }

        if (buttonFloat_R > 0.9f)
        {
            buttonPush_R = true;    //������
                                    // Debug.Log("������");
        }
        else if (buttonFloat_R <= 0.9f)
        {
            buttonPush_R = false;
            //Debug.Log("�� ����");
        }

    }
   */
   
}
