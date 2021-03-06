using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    List<AudioSource> audioSources = new List<AudioSource>();

    int CurrentSourceIndex = 0;

    float volumeSetting = 0.7f;

    [SerializeField] float switchSpeed = 3f;


    private void Awake()
    {
        SetUpSingleton();
        audioSources.AddRange(GetComponentsInChildren<AudioSource>());
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

        // Update is called once per frame
        void Update()
    {
        
            for (int i = 0; i < audioSources.Count; i++)
            {
                if (i == CurrentSourceIndex)
                {
                    audioSources[i].volume = Mathf.Lerp(audioSources[i].volume,volumeSetting,(Time.unscaledDeltaTime) * switchSpeed);
                }
                if (i != CurrentSourceIndex)
                {
                    audioSources[i].volume = Mathf.Lerp(audioSources[i].volume, 0, (Time.unscaledDeltaTime) * switchSpeed);
                    if (audioSources[i].volume <= 0.1f)
                    {
                        audioSources[i].Stop();
                    }
                }
            }
    }

    public void ProvideSong(AudioClip audioClip)
    {
        if (audioSources[CurrentSourceIndex].clip != audioClip)
        {
            CurrentSourceIndex = (CurrentSourceIndex + 1) % 2;
            audioSources[CurrentSourceIndex].clip = audioClip;
            audioSources[CurrentSourceIndex].loop = true;
            audioSources[CurrentSourceIndex].Play();
        }
    }

    public void SetVolume(float v)
    {
        volumeSetting = v;
        audioSources[CurrentSourceIndex].volume = volumeSetting;
    }

    public void StopPlaying()
    {
        audioSources[CurrentSourceIndex].Stop();
    }
}
