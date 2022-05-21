using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignTrigger : MonoBehaviour
{
    SignUITextBox textBox;

    [SerializeField] string signText;

    private void Awake()
    {
        textBox = FindObjectOfType<SignUITextBox>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        textBox.DisplaySign(signText);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        textBox.HideSign();
    }
}
