using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] ParticleSystem bloodEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform other = collision.transform;
        
        if (other.CompareTag("Player"))
        {
            Instantiate(bloodEffect, collision.GetContact(0).point, Quaternion.identity, other);
            other.GetComponent<PlayerStateManager>().SpikeDeath();
        }
    }
}
