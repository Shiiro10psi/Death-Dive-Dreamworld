using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerSoundsManager sounds;
    DeathVolumeEffects deathEffects;
    DrowningVolumeEffects drownEffects;
    PlayerInput input;
    PlayerStateUI ui;
    [SerializeField] Transform GroundPoint;
    [SerializeField] LayerMask GroundLayer;

     public int CurrentLayer { get { return m_CurrentLayer; }  set { m_CurrentLayer = value; } }
    [SerializeField] int m_CurrentLayer = 0;
    [SerializeField] List<RespawnPoint> respawnPoints = new List<RespawnPoint>();

    bool dead = false;
    bool canJump = true;
    bool inWater = false;

    bool onGround = false;
    float coyoteTimer = 0f;
    [SerializeField] float coyoteTime = .1f;

    int heartRate = 70;
    [SerializeField] int minHeartRate = 70;
    [SerializeField] int maxHeartRate = 150;

    [SerializeField] int SpikeHeartRate = 15, DrownHeartRate = 5;

    float Air = 30f;
    [SerializeField] float MaxAir = 30f;

    [SerializeField] GameObject deadBody;

    int keys = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sounds = GetComponent<PlayerSoundsManager>();
        deathEffects = FindObjectOfType<DeathVolumeEffects>();
        drownEffects = FindObjectOfType<DrowningVolumeEffects>();
        input = GetComponent<PlayerInput>();
        ui = FindObjectOfType<PlayerStateUI>();
    }

    private void Start()
    {
        heartRate = minHeartRate;
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

        if (inWater)
        {
            Air -= Time.deltaTime;
            if (Air <= 0)
            {
                DrownDeath();
            }
        }
        if (!inWater)
        {
            Air = Mathf.Clamp(Air + Time.deltaTime * 5f, 0f, MaxAir);
        }

        drownEffects.Inform(Air);
        ui.Inform(heartRate, keys);

        
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
            r.Activate();
        }

        if (r != respawnPoints[r.DreamLayer])
        {
            respawnPoints[r.DreamLayer].Deactivate();
            respawnPoints[r.DreamLayer] = r;
            respawnPoints[r.DreamLayer].Activate();
        }
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
            heartRate += SpikeHeartRate;

            if (heartRate < maxHeartRate)
                StartCoroutine(Death());
            if (heartRate >= maxHeartRate)
                StartCoroutine(GameOverAnim());
        }
    }
    public void DrownDeath()
    {
        if (!dead)
        {
            dead = true;
            
            
            heartRate += DrownHeartRate;

            if (heartRate < maxHeartRate)
                StartCoroutine(Death());
            if (heartRate >= maxHeartRate)
                StartCoroutine(GameOverAnim());
        }
    }

    public void Respawn()
    {
        Vector3 deathposition = transform.position;
        Quaternion deathrotation = transform.rotation;

        transform.position = respawnPoints[CurrentLayer - 1].transform.position;
        CurrentLayer -= 1;
        respawnPoints[CurrentLayer].Activate();
        rb.rotation = 0;
        rb.freezeRotation = true;
        Air = MaxAir;
        dead = false;

        Instantiate(deadBody, deathposition, deathrotation);
    }

    public IEnumerator Death()
    {
        rb.freezeRotation = false;
        deathEffects.Play();
        sounds.PlayGameOverSound();

        yield return new WaitForSeconds(2);
        
        Respawn();
    }
    public IEnumerator GameOverAnim()
    {
        rb.freezeRotation = false;
        deathEffects.Play();
        sounds.PlayGameOverSound();

        yield return new WaitForSeconds(2);

        GameOver();
    }

    void GameOver()
    {
        FindObjectOfType<GameOverMenu>().Open();
    }

    public void PickUpKey()
    {
        keys++;
    }

    public void UseKey()
    {
        keys--;
    }

    public bool HasKey()
    {
        if (keys > 0)
            return true;
        return false;
    }

    public void EnterWater()
    {
        rb.gravityScale = -.3f;
        input.SwitchCurrentActionMap("Player_Water");
        inWater = true;
        sounds.PlayWaterSound();
    }

    public void ExitWater()
    {
        rb.gravityScale = 1;
        input.SwitchCurrentActionMap("Player_Land");
        inWater = false;
        sounds.PlayWaterSound();
    }

}
