using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    SoundPlayer sp;

    [SerializeField] AudioClip sound;

    private void Awake()
    {
        sp = FindObjectOfType<SoundPlayer>();
    }
    private void Update()
    {

        if (sp == null) sp = FindObjectOfType<SoundPlayer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform other = collision.transform;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStateManager>().PickUpKey();
            sp.PlaySound(sound);
            Destroy(gameObject);
        }
    }
}
