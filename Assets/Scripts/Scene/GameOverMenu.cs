using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour, IMenu
{
    CanvasGroup cg;
    [SerializeField] Button defaultButton;

    SettingsMenu settingsMenu;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void Open()
    {
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
    }

    public void SettingsButton()
    {
        if (settingsMenu == null)
        {
            settingsMenu = FindObjectOfType<SettingsMenu>();
        }

        settingsMenu.Open(this);
    }

    public void ReturnToMenuButton()
    {
        FindObjectOfType<SceneLoader>().LoadScene(0);
    }

    public void RestartLevelButton()
    {
        FindObjectOfType<SceneLoader>().ReloadScene();
    }
}
