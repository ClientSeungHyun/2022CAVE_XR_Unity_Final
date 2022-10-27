using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour
{

    public Animator animator;
    public GameObject player;
    public PlayerControl playerControl;
    public HandControler leftHand, rightHand;
    public GameObject []startPosition;
    public Camera InBoxCamera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        leftHand = GameObject.Find("CustomHandLeft").GetComponent<HandControler>();
        rightHand = GameObject.Find("CustomHandRight").GetComponent<HandControler>();

        if (InBoxCamera != null)
            InBoxCamera.enabled = false;

        if (playerControl.preScene == 0)
            player.transform.position = startPosition[0].transform.position;
        else if (playerControl.preScene == 1 && playerControl.nowScene == 2)
            player.transform.position = startPosition[0].transform.position;
        else if (playerControl.preScene == 3)
            player.transform.position = startPosition[1].transform.position;
        else if (playerControl.preScene == 2 && playerControl.nowScene == 3)
        {
            player.transform.position = startPosition[0].transform.position;
            leftHand.animator = rightHand.animator = animator;
            leftHand.animator.SetBool("Opening", false);
        }
        else if (playerControl.preScene == 2 && playerControl.nowScene == 1)
            player.transform.position = startPosition[1].transform.position;
        print(playerControl.preScene + playerControl.nowScene);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
