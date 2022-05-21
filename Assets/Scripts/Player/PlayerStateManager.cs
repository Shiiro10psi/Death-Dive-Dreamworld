using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerSoundsManager sounds;
    DeathVolumeEffects deathEffects;

    [SerializeField] Transform GroundPoint;
    [SerializeField] LayerMask GroundLayer;

     public int CurrentLayer { get { return m_CurrentLayer; }  set { m_CurrentLayer = value; } }
    [SerializeField] int m_CurrentLayer = 0;
    [SerializeField] List<RespawnPoint> respawnPoints = new List<RespawnPoint>();

    bool dead = false;
    bool canJump = true;

    bool onGround = false;
    float coyoteTimer = 0f;
    [SerializeField] float coyoteTime = .1f;

    [SerializeField] GameObject deadBody;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sounds = GetComponent<PlayerSoundsManager>();
        deathEffects = FindObjectOfType<DeathVolumeEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCircle(GroundPoint.position, .1f, GroundLayer);
        if (onGround)
        {
            coyoteTimer = coyoteTime;
        }
        if (!onGround)
        {
            coyoteTimer -= Time.deltaTime;
        }


        if (coyoteTimer > 0)
        {
            canJump = true;
        }
        if (coyoteTimer <= 0)
        {
            canJump = false;
        }
    }

    public bool IsAlive()
    {
        return !dead;
    }

    public bool CanJump()
    {
        if (!onGround && canJump)
            Debug.Log("Coyote Jump");
        coyoteTimer = 0f;
        return canJump;
    }

    public void SetRespawnPoint(RespawnPoint r)
    {
        if (respawnPoints.Count < 10)
        {
            for (int i = 0; i < 10; i++)
                respawnPoints.Add(r);
        }

        respawnPoints[r.DreamLayer].Deactivate();
        respawnPoints[r.DreamLayer] = r;
        respawnPoints[r.DreamLayer].Activate();
    }

    public bool isOnGround()
    {
        return onGround;
    }

    public void SpikeDeath()
    {
        if (!dead)
        {
            dead = true;

            sounds.PlayHurtSound();
            sounds.PlayDeathSound();

            StartCoroutine(Death());
        }
    }

    public void Respawn()
    {
        Vector3 deathposition = transform.position;
        Quaternion deathrotation = transform.rotation;

        transform.position = respawnPoints[CurrentLayer - 1].transform.position;
        CurrentLayer -= 1;
        rb.rotation = 0;
        rb.freezeRotation = true;
        dead = false;

        Instantiate(deadBody, deathposition, deathrotation);
    }

    public IEnumerator Death()
    {
        rb.freezeRotation = false;
        deathEffects.Play();


        yield return new WaitForSeconds(2);
        
        Respawn();
    }
}
