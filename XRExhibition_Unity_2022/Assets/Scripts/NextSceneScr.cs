using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class NextSceneScr : MonoBehaviour
{
    OVRScreenFade OFade;
    public GameObject CenterEyeObj;
    [SerializeField]
    private string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        OFade = CenterEyeObj.transform.GetComponent<OVRScreenFade>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("stairTrigger"))
        {
            Debug.Log("충돌");
            OFade.FadeOut();
            StartCoroutine(changeScene());
        }
    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1.0f);//FadeOut될동안 3초 딜레이 
        LodingSceneControlScr.LoadScene(SceneName);//씬 로드 

    }
    public void fadeOut()
    {
        OFade.FadeOut();
    }
}
