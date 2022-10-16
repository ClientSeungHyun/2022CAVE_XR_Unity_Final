using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneScr : MonoBehaviour
{
    [SerializeField]
    private string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(changeScene());
        }

    }
    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(2.5f);//FadeOutµ…µøæ» 3√  µÙ∑π¿Ã 
        LodingSceneControlScr.LoadScene(SceneName);//æ¿ ∑ŒµÂ 

    }
}
