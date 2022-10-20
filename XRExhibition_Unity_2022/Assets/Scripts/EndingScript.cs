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
}
