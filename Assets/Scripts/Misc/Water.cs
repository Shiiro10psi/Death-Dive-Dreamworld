using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    DrowningVolumeEffects drownEffects;

    private void Awake()
    {
        drownEffects = FindObjectOfType<DrowningVolumeEffects>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform other = collision.transform;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStateManager>().EnterWater();
            drownEffects.Inform(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Transform other = collision.transform;
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStateManager>().ExitWater();
            drownEffects.Inform(false);
        }
    }
}
