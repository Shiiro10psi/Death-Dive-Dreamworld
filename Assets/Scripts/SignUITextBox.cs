using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignUITextBox : MonoBehaviour
{
    CanvasGroup cg;
    TextMeshProUGUI textObject;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        textObject = GetComponentInChildren<TextMeshProUGUI>();
    }


    public void DisplaySign(string text)
    {
        textObject.text = text;

        cg.alpha = 1;
    }

    public void HideSign()
    {
        cg.alpha = 0;
    }
}
