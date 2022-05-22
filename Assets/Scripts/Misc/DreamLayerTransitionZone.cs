using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamLayerTransitionZone : MonoBehaviour
{
    [SerializeField] int Layer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform other = collision.transform;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStateManager>().CurrentLayer = Layer;
        }
    }
}
