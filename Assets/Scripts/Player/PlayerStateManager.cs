using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Transform GroundPoint;
    [SerializeField] LayerMask GroundLayer;

    int CurrentLayer = 0;
    [SerializeField] List<RespawnPoint> respawnPoints = new List<RespawnPoint>();

    bool dead = false;
    bool canJump = true;

    bool onGround = false;
    float coyoteTimer = 0f;
    [SerializeField] float coyoteTime = .1f;
    

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

        respawnPoints[r.DreamLayer] = r;
    }
}
