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
            Debug.Log("�浹");
            OFade.FadeOut();
            StartCoroutine(changeScene());
        }
    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1.0f);//FadeOut�ɵ��� 3�� ������ 
        LodingSceneControlScr.LoadScene(SceneName);//�� �ε� 

    }
    public void fadeOut()
    {
        OFade.FadeOut();
    }
}
