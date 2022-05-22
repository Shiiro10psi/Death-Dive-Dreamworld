using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    IMenu lastMenu;

    CanvasGroup cg;
    [SerializeField] Button defaultButton;

    MusicPlayer mp;
    SoundPlayer sp;

    private void Awake()
    {
        SetUpSingleton();
        cg = GetComponent<CanvasGroup>();
        mp = FindObjectOfType<MusicPlayer>();
        sp = FindObjectOfType<SoundPlayer>();
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

    public void Open(IMenu menu)
    {
        lastMenu = menu;

        lastMenu.Close();

        Time.timeScale = 0f;

        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        defaultButton.Select();
    }

    public void Close()
    {
        Time.timeScale = 1f;

        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;

        lastMenu.Open();
    }

    public void MusicSlider(float value)
    {
        mp.SetVolume(value);
    }

    public void SoundSlider(float value)
    {
        sp.SetVolume(value);
    }

    public void ReturnToMenuButton()
    {
        FindObjectOfType<SceneLoader>().LoadScene(0);
    }
}

