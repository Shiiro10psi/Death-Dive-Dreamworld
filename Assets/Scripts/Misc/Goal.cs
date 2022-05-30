using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<SoundPlayer>().PlaySound(clip);
        StartCoroutine(Win(collision.transform));
    }

    private IEnumerator Win(Transform target)
    {
        float timer = 0f;

        while (timer < 2f)
        {
            transform.Rotate(target.position, 1);
            //target.Rotate(target.position, 1);
            transform.position = target.position + new Vector3(0, 0.5f, 0);
            transform.localScale = new Vector3(transform.localScale.x - .05f, transform.localScale.x + .05f, transform.localScale.z);
            target.localScale = new Vector3(target.localScale.x - .05f, target.localScale.x + .05f, target.localScale.z);

            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        FindObjectOfType<WinMenu>().Open();
    }
}
