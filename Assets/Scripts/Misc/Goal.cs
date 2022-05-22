using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<SoundPlayer>().PlaySound(clip);
        FindObjectOfType<WinMenu>().Open();
    }
}
