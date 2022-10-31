using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class FadeOutScr : MonoBehaviour
{
    OVRScreenFade OFade;
    public GameObject CenterEyeObj;
    // Start is called before the first frame update
    void Start()
    {
        OFade = CenterEyeObj.transform.GetComponent<OVRScreenFade>();
    }

    public void fadeOut()
    {
        OFade.FadeOut();
    }
}
