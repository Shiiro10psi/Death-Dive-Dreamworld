using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerStateManager playerState;

    float xmove = 0f;
    [SerializeField] float maxSpeed = 20f;

    float jumpBuffer = 0f;

    [SerializeField] float moveSpeed = 5f, jumpStrength = 5f;
    [SerializeField] float jumpBufferTime = .1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerStateManager>();
    }

    private void FixedUpdate()
    {
        if (playerState.IsAlive())
        {
            rb.AddForce(new Vector2((xmove * Time.deltaTime * moveSpeed), 0), ForceMode2D.Impulse);

            if (jumpBuffer > 0)
            {
                if (playerState.CanJump())
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0); //Nullify vertical movement to ensure jumps are always the same strength.
                    rb.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);

                    jumpBuffer = 0;
                }

                jumpBuffer -= Time.deltaTime;
            }
        }
        
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);

        if (xmove == 0 && playerState.isOnGround())
        {
            rb.drag = 3f;
        }
        if (xmove != 0)
        {
            rb.drag = 0.5f;
        }
    }

    private void OnMove(InputValue value)
    {
        xmove = value.Get<Vector2>().x;
    }

    private void OnJump(InputValue value)
    {
        jumpBuffer = jumpBufferTime;
    }
}
