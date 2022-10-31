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
    public float soundSize;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GamaManager>();
        soundSource = this.gameObject.AddComponent<AudioSource>();
        soundSource.clip = BGMList[0];
        soundSource.loop = true;
        soundSource.Play();
        soundSize = 0.5f;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (BGSlider != null)
        {
            soundSize = (float)BGSlider.value;
        }
        soundSource.volume = soundSize;

        if (GM.gameState == 1)
        {
            soundSource.clip = BGMList[1];
        }
        if (GM.gameState == 2)
        {
            soundSource.clip = BGMList[2];
        }
        if (GM.gameState == 3)
        {
            soundSource.clip = BGMList[0];
        }

        if (!soundSource.isPlaying && !(soundSource.volume <=0))
        {
            soundSource.Play();
        }
        else
        {
            soundSource.Pause();
        }
    }


}
