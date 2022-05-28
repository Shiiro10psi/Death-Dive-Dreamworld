using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] AudioClip bgm;
    MusicPlayer mp;

    private void Start()
    {
        mp = FindObjectOfType<MusicPlayer>();
        StartMusic();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (mp == null) { mp = FindObjectOfType<MusicPlayer>(); StartMusic(); }
    }

    void StartMusic()
    {
        mp.ProvideSong(bgm);
    }
}
