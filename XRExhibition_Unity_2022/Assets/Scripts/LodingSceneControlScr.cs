using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingSceneControlScr : MonoBehaviour
{
    static string nextScene;
    [SerializeField]
    Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LodingScene");//중간 로딩 중 씬 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);//이동할 씬 받아 오기 
        op.allowSceneActivation = false;//강제로 로딩 90퍼때 멈추기 

        float timer = 0;//시간 변수 
        while (!op.isDone)//씬 로딩이 끝날때까지 반복 
        {
            yield return null;
            if (op.progress < 0.9f)//씬 로딩 정도 90퍼 이하 
            {
                Debug.Log("로딩 완료");
            }
            else//fake 로딩 
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);//1초 타이머 
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;// 로딩 완료  불러오기 
                    yield break;//코루틴 빠저나오기 
                }
            }
        }
    }
}
