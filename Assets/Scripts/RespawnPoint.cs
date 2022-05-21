using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField]
    public int DreamLayer { get { return m_DreamLayer; } private set { m_DreamLayer = value; } }
    public int m_DreamLayer = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform other = collision.transform;

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStateManager>().SetRespawnPoint(this);
        }
    }
}
