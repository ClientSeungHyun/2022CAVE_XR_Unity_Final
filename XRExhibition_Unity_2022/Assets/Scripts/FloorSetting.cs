using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSetting : MonoBehaviour
{

    public Animator animator;
    public GameObject player;
    public GameObject monster;
    public PlayerControl playerControl;
    public HandControler leftHand, rightHand;
    public GameObject []startPosition;
    public Camera InBoxCamera;
    public GameObject hidingBox;


    AudioSource monsterSource;
    AudioClip monsterSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        monster = GameObject.Find("Monster");
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        leftHand = GameObject.Find("CustomHandLeft").GetComponent<HandControler>();
        rightHand = GameObject.Find("CustomHandRight").GetComponent<HandControler>();

        if (InBoxCamera != null)
            InBoxCamera.enabled = false;

        if (playerControl.preScene == 0) //�Ϲ� 1��
            player.transform.position = startPosition[0].transform.position;
        else if (playerControl.preScene == 1)   //�Ϲ� 2��_1
            player.transform.position = startPosition[0].transform.position;
        else if (playerControl.preScene == 3)
        {  //3������ 2��
            player.transform.position = startPosition[1].transform.position;
            if(playerControl.isHaveLastKey == true) //������ Ű�� ���� �ִٸ�
            {
                monsterSource = monster.GetComponent<AudioSource>();
                monsterSource.loop = false;
                monsterSource.Play();
            }
        }
        else if (playerControl.preScene == 2 && playerControl.nowScene == 3)    //�Ϲ� 3��
        {
            player.transform.position = startPosition[0].transform.position;
            leftHand.animator = rightHand.animator = animator;
            leftHand.animator.SetBool("Opening", false);
        }
        else if (playerControl.preScene == 2 && playerControl.nowScene == 1) //�Ϲ� 1��
            player.transform.position = startPosition[1].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //������ ���踦 ������ ���� �� �ִ� ���¿����� ���ڰ� ����
        if(playerControl.isHaveLastKey == true)
        {
            hidingBox.GetComponent<Light>().range = 10;
            hidingBox.GetComponent<Light>().intensity = 2;
        }
        else
        {
            hidingBox.GetComponent<Light>().range = 0;
            hidingBox.GetComponent<Light>().intensity = 1;
        }
        if(playerControl.nowScene == 2 && playerControl.isHiding == true)   //2������ ������ ������ ����
        {
            player.SetActive(false);
            GameObject.Find("InBoxCamera").GetComponent<Camera>().enabled = true;
            monster.SetActive(true);
        }
        else
        {
            player.SetActive(true);
            GameObject.Find("InBoxCamera").GetComponent<Camera>().enabled = false;
            monster.SetActive(false);
        }
    }
}
