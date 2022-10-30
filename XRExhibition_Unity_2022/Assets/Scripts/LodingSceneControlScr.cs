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
    //private string[] tipStrings; = new string[] {"������ ���� ã�Ƴ��� ����\n���� ���踦 ã�� Ż���ؾ���...", "���� �Ҹ��� �鸮�� �������...", "���踦 ����� ���� ���ܳ����� �ʾ��� �ٵ�..."};
    static string nextScene;
    [SerializeField]
    Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
        tipList.Add("������ ���� ã�Ƴ��� ����\n���� ���踦 ã�� Ż���ؾ���...");
        tipList.Add("���� �Ҹ��� �鸮�� �������...");
        tipList.Add("���踦 ����� ���� ���ܵ����� �ʾ��� �ٵ�...");
        tipList.Add("������ ����� �ֱ⸦...");
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
        SceneManager.LoadScene("LoadingScene");//�߰� �ε� �� �� 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);//�̵��� �� �޾� ���� 
        op.allowSceneActivation = false;//������ �ε� 90�۶� ���߱� 

        float timer = 5f;//�ð� ���� 
        while (!op.isDone)//�� �ε��� ���������� �ݺ� 
        {
            yield return null;
            if (op.progress < 0.9f)//�� �ε� ���� 90�� ���� 
            {
                progressBar.fillAmount = op.progress;
            }
            else//fake �ε� 
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;// �ε� �Ϸ�  �ҷ����� 
                    player.GetComponent<OVRPlayerController>().Acceleration = 0.15f;
                    yield break;//�ڷ�ƾ ���������� 
                }
            }
        }
    }
}
