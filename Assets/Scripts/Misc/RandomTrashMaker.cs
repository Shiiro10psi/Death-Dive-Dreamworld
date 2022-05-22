using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrashMaker : MonoBehaviour
{
    float timer = 0f;

    [SerializeField] float itemsPerSecond = 5;

    [SerializeField] List<Sprite> sprites;

    [SerializeField] GameObject trash;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1f / itemsPerSecond)
        {
            GameObject newTrash = Instantiate(trash, gameObject.transform.position, Quaternion.identity);

            newTrash.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
            newTrash.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), 0), ForceMode2D.Impulse);
            newTrash.GetComponent<Rigidbody2D>().gravityScale = Random.Range(.5f, 3f);

            timer = 0f;
        }
    }
}
