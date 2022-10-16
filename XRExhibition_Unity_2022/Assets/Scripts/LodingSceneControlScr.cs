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
        SceneManager.LoadScene("LodingScene");//�߰� �ε� �� �� 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);//�̵��� �� �޾� ���� 
        op.allowSceneActivation = false;//������ �ε� 90�۶� ���߱� 

        float timer = 0;//�ð� ���� 
        while (!op.isDone)//�� �ε��� ���������� �ݺ� 
        {
            yield return null;
            if (op.progress < 0.9f)//�� �ε� ���� 90�� ���� 
            {
                Debug.Log("�ε� �Ϸ�");
            }
            else//fake �ε� 
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);//1�� Ÿ�̸� 
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;// �ε� �Ϸ�  �ҷ����� 
                    yield break;//�ڷ�ƾ ���������� 
                }
            }
        }
    }
}
