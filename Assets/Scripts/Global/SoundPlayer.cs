using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        SetUpSingleton();
        source = GetComponent<AudioSource>();
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

    public void SetVolume(float v)
    {
        source.volume = v;
    }

    public bool muteToggle()
    {
        source.mute = !source.mute;
        return source.mute;
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
