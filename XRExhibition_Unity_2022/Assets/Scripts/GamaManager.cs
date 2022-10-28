using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamaManager : MonoBehaviour
{

    public enum GAMESTATE : int { MainMenu, OUTDOOR, INHOUSE, END };

    public SoundManager soundManager;
    public PlayerControl playerControl;
    private MonsterController monsterController;


    public GameObject menuUI, optionUI, titleText, btn1Text, btn2Text, btn3Text, laser;
    public GameObject InBoxCamera;
    public GameObject Monster;

    public bool isMonsterGone;
    public int gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = (int)GAMESTATE.MainMenu;
        optionUI.SetActive(false);
        gameState = 0;
        isMonsterGone = false;

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        if (playerControl.nowScene > 0)
            gameState = (int)GAMESTATE.INHOUSE;




    }


    public int getGameState()
    {
        return gameState;
    }
    



    //버튼 관리
    public void ClickStart()
    {
        gameState = (int)GAMESTATE.OUTDOOR;
        //soundManager.soundSource.Stop();
        //soundManager.soundSource.clip = soundManager.BGMList[gameState];
        //soundManager.soundSource.Play();

        laser.SetActive(false);
        StartCoroutine(Fade());
    }
    public void ClickOptions()
    {
        optionUI.SetActive(true);
    }
    public void ClickGameOver()
    {
        Application.Quit();
    }
    public void ClickBack()
    {
        optionUI.SetActive(false);
    }


    //canvas 사라지게
    IEnumerator Fade()
    {
        Color c1 = menuUI.GetComponent<Image>().color;
        Color c2 = titleText.GetComponent<TextMeshProUGUI>().color;
        Color c3 = btn1Text.GetComponent<TextMeshProUGUI>().color;
        Color c4 = btn2Text.GetComponent<TextMeshProUGUI>().color;
        Color c5 = btn3Text.GetComponent<TextMeshProUGUI>().color;

        for (float alpha = 1f; alpha >= -1; alpha -= 0.1f)
        {
            c1.a = alpha;
            c2.a = alpha;
            c3.a = alpha;
            c4.a = alpha;
            c5.a = alpha;
            menuUI.GetComponent<Image>().color = c1;
            titleText.GetComponent<TextMeshProUGUI>().color = c2;
            btn1Text.GetComponent<TextMeshProUGUI>().color = c3;
            btn2Text.GetComponent<TextMeshProUGUI>().color = c4;
            btn3Text.GetComponent<TextMeshProUGUI>().color = c5;

            if (alpha < 0)
            {
                alpha = 0.0f;
                menuUI.SetActive(false);
                StopCoroutine(Fade());
            }

            yield return new WaitForSeconds(.2f);


        }
    }
}
