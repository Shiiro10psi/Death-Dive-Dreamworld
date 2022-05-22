using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform other = collision.transform;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStateManager>().EnterWater();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Transform other = collision.transform;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStateManager>().ExitWater();
        }
    }
}
