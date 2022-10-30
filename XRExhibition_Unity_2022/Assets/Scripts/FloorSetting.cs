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

        if (playerControl.preScene == 0)//�Ϲ� 1��
        {
            player.transform.position = startPosition[0].transform.position;
            monster.SetActive(false);
        }
        if (playerControl.preScene == 1)
        {   //�Ϲ� 2��_1
            player.transform.position = startPosition[0].transform.position;
        }
        if (playerControl.preScene == 3 && playerControl.nowScene == 2)
        {  //3������ 2��
            player.transform.position = startPosition[1].transform.position;
            if (playerControl.isHaveLastKey == true) //������ Ű�� ���� �ִٸ�
            {
                monsterSource = GetComponent<AudioSource>();
                monsterSource.loop = false;
                monsterSource.Play();   //���� �Ҹ��� �鸲
            }
        }
        if (playerControl.preScene == 2 && playerControl.nowScene == 3)    //�Ϲ� 3��
        {
            player.transform.position = startPosition[0].transform.position;
            leftHand.animator = rightHand.animator = animator;
            leftHand.animator.SetBool("Opening", false);
        }
        if (playerControl.preScene == 2 && playerControl.nowScene == 1) //�Ϲ� 1��
            player.transform.position = startPosition[1].transform.position;


        if (monster != null)
            monster.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //2������ ������ ���踦 ������ ���� �� �ִ� ���¿����� ���ڰ� ����
        if (hidingBox != null)
        {
            if (playerControl.isHaveLastKey == true)
            {
                hidingBox.GetComponent<Light>().range = 10;
                hidingBox.GetComponent<Light>().intensity = 2;
            }
            else
            {
                hidingBox.GetComponent<Light>().range = 0;
                hidingBox.GetComponent<Light>().intensity = 1;
            }
        }
        if(playerControl.nowScene == 2 && playerControl.isHiding == true)   //2������ ������ ������ ����
        {
            player.transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("InBoxCamera").GetComponent<Camera>().enabled = true;
            monster.SetActive(true);
        }
        else if(playerControl.nowScene == 2 && monster.GetComponent<MonsterController>().isGone == true)
        {
            Destroy(monster);
            Invoke("hidingOver", 3f);
        }
        

        if(playerControl.isLastDoorOpen == true)    //������ ���� ����
        {
            monster.SetActive(true);    //���� Ȱ��ȭ(������Ʈ�ѽ�ũ��Ʈ���� �˾Ƽ� �Ѿư�)
        }
    }

    public void hidingOver()
    {
        player.transform.GetChild(1).gameObject.SetActive(true);
        playerControl.isHiding = false;
        playerControl.hideOver = true;

        if(GameObject.Find("InBoxCamera") != null)
            GameObject.Find("InBoxCamera").GetComponent<Camera>().enabled = false;
    }
}
