using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LodingSceneControlScr : MonoBehaviour
{
    GameObject player;
    public Transform playerPosition;
    public TextMeshProUGUI tipText;

    private List<string> tipList = new List<string>();
    //private string[] tipStrings; = new string[] {"괴물이 나를 찾아내기 전에\n현관 열쇠를 찾아 탈출해야해...", "괴물 소리가 들리면 숨어야해...", "열쇠를 어려군 곳에 숨겨놓지는 않았을 텐데..."};
    static string nextScene;
    [SerializeField]
    Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
        tipList.Add("괴물이 나를 찾아내기 전에\n현관 열쇠를 찾아 탈출해야해...");
        tipList.Add("괴물 소리가 들리면 숨어야해...");
        tipList.Add("열쇠를 어려운 곳에 숨겨두지는 않았을 텐데...");
        tipList.Add("나에게 희망이 있기를...");
        tipText.text = tipList[Random.Range(0, 4)];

        player = GameObject.Find("Player");
        player.transform.position = playerPosition.position;
        player.transform.rotation = playerPosition.rotation;
        tipText = GetComponent<TextMeshProUGUI>();

        player.GetComponent<OVRPlayerController>().Acceleration = 0;
        StartCoroutine(LoadSceneProcess());
    }
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");//중간 로딩 중 씬 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);//이동할 씬 받아 오기 
        op.allowSceneActivation = false;//강제로 로딩 90퍼때 멈추기 

        float timer = 5f;//시간 변수 
        while (!op.isDone)//씬 로딩이 끝날때까지 반복 
        {
            yield return null;
            if (op.progress < 0.9f)//씬 로딩 정도 90퍼 이하 
            {
                progressBar.fillAmount = op.progress;
            }
            else//fake 로딩 
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;// 로딩 완료  불러오기 
                    player.GetComponent<OVRPlayerController>().Acceleration = 0.15f;
                    yield break;//코루틴 빠저나오기 
                }
            }
        }
    }
}
