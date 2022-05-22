using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrashEater : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
    }
}
