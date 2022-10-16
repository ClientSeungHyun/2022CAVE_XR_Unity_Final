using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BgmType
{
    public string _name;
    public AudioClip _audioClip;
    public BgmType(string name, AudioClip audioClip)
    {
        _name = name;
        _audioClip = audioClip;

    }
}

public class SoundManager : MonoBehaviour
{


    public BgmType[] BGMList;

    private AudioSource soundSource;
    private string nowBgmName = "";

    // Start is called before the first frame update
    void Start()
    {
        soundSource = gameObject.AddComponent<AudioSource>();
        soundSource.loop = true;
        if (BGMList.Length > 0)
        {
            playBGM(BGMList[0]._name);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playBGM(string name)
    {

        if (nowBgmName.Equals(name)) return;    //같은 노래면 리턴

        soundSource.clip = BGMList[0]._audioClip;
        soundSource.Play();
        nowBgmName = name;
    }
}
