using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] AudioClip bgm;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<MusicPlayer>().ProvideSong(bgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
