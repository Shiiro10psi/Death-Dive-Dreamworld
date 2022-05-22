using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour, IMenu
{
    CanvasGroup cg;
    [SerializeField] Button defaultButton;

    SettingsMenu settingsMenu;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Open();
    }

    public void Open()
    {
        cg.alpha = 1f;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        defaultButton.Select();
    }

    public void Close()
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    
    public void ReturnToMenuButton()
    {
        FindObjectOfType<SceneLoader>().LoadScene(0);
    }

    public void LoadLevelButton(int index)
    {
        FindObjectOfType<SceneLoader>().LoadScene(index);
    }

}

