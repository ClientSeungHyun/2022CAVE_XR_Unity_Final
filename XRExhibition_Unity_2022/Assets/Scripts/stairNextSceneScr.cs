using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairNextSceneScr : MonoBehaviour
{
    [SerializeField]
    private string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1.0f);//FadeOut�ɵ��� 3�� ������ 
        LodingSceneControlScr.LoadScene(SceneName);//�� �ε� 

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(changeScene());
        }
    }
}
