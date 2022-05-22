using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] AudioClip bgm;
    MusicPlayer mp;

    private void Awake()
    {
        mp = FindObjectOfType<MusicPlayer>();
        StartMusic();
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<MusicPlayer>().ProvideSong(bgm);
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
