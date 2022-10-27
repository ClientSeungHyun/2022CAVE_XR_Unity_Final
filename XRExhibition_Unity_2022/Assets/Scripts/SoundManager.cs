using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    private GamaManager GM;

    public AudioClip[] BGMList;
    public AudioSource soundSource;
    public Slider BGSlider;
    //private Slider EFSlider;

    private string nowBgmName = "";

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("MenuController").GetComponent<GamaManager>();
        soundSource = this.gameObject.AddComponent<AudioSource>();
        soundSource.clip = BGMList[0];
        soundSource.loop = true;
        soundSource.Play();

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        soundSource.volume = BGSlider.value;
        soundSource.clip = BGMList[GM.getGameState()];  //상황에 맞춘 오디오 삽입
        soundSource.Play();
    }


}
