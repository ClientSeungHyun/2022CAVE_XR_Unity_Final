using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject menuUI, titleText, btn1Text, btn2Text, btn3Text;

    public void ClickStart()
    {
        StartCoroutine(Fade());
        Debug.Log("start눌림");
    }
    public void ClickOptions()
    {
        Debug.Log("option눌림");
    }
    public void ClickGameOver()
    {

        Application.Quit();
        Debug.Log("quit눌림");
    }




    IEnumerator Fade()
    {
        Color c1 = menuUI.GetComponent<Image>().color;
        Color c2 = titleText.GetComponent<Text>().color;
        Color c3 = btn1Text.GetComponent<Text>().color;
        Color c4 = btn2Text.GetComponent<Text>().color;
        Color c5 = btn3Text.GetComponent<Text>().color;

        for (float alpha = 1f; alpha >= -1; alpha -= 0.1f)
        {
            c1.a = alpha;
            c2.a = alpha;
            c3.a = alpha;
            c4.a = alpha;
            c5.a = alpha;
            menuUI.GetComponent<Image>().color = c1;
            titleText.GetComponent<Text>().color = c2;
            btn1Text.GetComponent<Text>().color = c3;
            btn2Text.GetComponent<Text>().color = c4;
            btn3Text.GetComponent<Text>().color = c5;

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
