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
        GM = GameObject.Find("GameManager").GetComponent<GamaManager>();
        soundSource = this.gameObject.AddComponent<AudioSource>();
        soundSource.clip = BGMList[0];
        soundSource.loop = true;
        soundSource.Play();

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(GM.gameState == 2)
            soundSource.clip = BGMList[2];
        soundSource.volume = BGSlider.value;
    }


}
