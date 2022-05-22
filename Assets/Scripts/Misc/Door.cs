using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    [SerializeField] AudioClip sound;

    SoundPlayer sp;

    private void Awake()
    {
        sp = FindObjectOfType<SoundPlayer>();
        sprites.AddRange(GetComponentsInChildren<SpriteRenderer>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform other = collision.transform;

        if (other.CompareTag("Player"))
        {
            PlayerStateManager playerState = other.GetComponent<PlayerStateManager>();

            if (playerState.HasKey())
            {
                playerState.UseKey();
                sp.PlaySound(sound);
                StartCoroutine(OpenDoor());

            }
        }
    }

    private IEnumerator OpenDoor()
    {
        float timer = 0f;

        while (timer <= 1f)
        {
            foreach (SpriteRenderer s in sprites)
            {
                s.material.SetFloat("_Fade", 1 - timer);
            }


            yield return new WaitForFixedUpdate();
            timer += Time.fixedDeltaTime;
        }

        Destroy(gameObject);
    }
}
