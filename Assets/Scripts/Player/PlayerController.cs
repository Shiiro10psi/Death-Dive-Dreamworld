using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerStateManager playerState;
    PlayerSoundsManager sounds;
    PlayerInput input;
    PauseMenu pauseMenu;

    Animator animator;


    float xmove = 0f;
    float ymove = 0f;
    [SerializeField] float maxSpeed = 20f;

    float jumpBuffer = 0f;
    bool waterJump = false;

    [SerializeField] float moveSpeed = 5f, jumpStrength = 5f, waterMoveSpeed = 10f;
    [SerializeField] float jumpBufferTime = .1f;

    float lastY = 0f;
    float escapeJumpTime = .5f;
    float escapeJumpTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerState = GetComponent<PlayerStateManager>();
        sounds = GetComponent<PlayerSoundsManager>();
        input = GetComponent<PlayerInput>();
        pauseMenu = FindObjectOfType<PauseMenu>();

        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        if (input.currentActionMap == input.actions.FindActionMap("Player_Land"))
        {

            if (xmove == 0 && playerState.isOnGround())
            {
                rb.drag = 3f;
            }
            if (xmove != 0 || jumpBuffer > 0)
            {
                rb.drag = 0.5f;
            }

            if (playerState.IsAlive())
            {
                rb.AddForce(new Vector2((xmove * Time.deltaTime * moveSpeed), 0), ForceMode2D.Impulse);

                if (jumpBuffer > 0 || waterJump)
                {
                    if (playerState.CanJump() || waterJump)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0); //Nullify vertical movement to ensure jumps are always the same strength.
                        if (!waterJump)rb.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
                        if (waterJump) rb.AddForce(new Vector2(0, jumpStrength / 1.5f), ForceMode2D.Impulse);
                        sounds.PlayJumpSound();
                        jumpBuffer = 0;
                    }

                    jumpBuffer -= Time.deltaTime;
                }
            }

            waterJump = false;

            if (lastY > rb.position.y)
            {
                rb.gravityScale = 1.5f;
            }
            if (lastY <= rb.position.y)
            {
                rb.gravityScale = 1f;
            }

            lastY = rb.position.y;

            if (!playerState.isOnGround() && (rb.velocity.x < .01f && rb.velocity.x > -.01f) && (rb.velocity.y < .01f && rb.velocity.y > -.01f) && playerState.IsAlive())
            {
                escapeJumpTimer += Time.fixedDeltaTime;
                if (escapeJumpTimer >= escapeJumpTime)
                {
                    rb.AddForce(new Vector2(0, jumpStrength / 2f), ForceMode2D.Impulse);
                    escapeJumpTimer = 0f;
                }
            }
        }

        if (input.currentActionMap == input.actions.FindActionMap("Player_Water"))
        {
            waterJump = true;
            if (playerState.IsAlive())
            {
                rb.AddForce(new Vector2((xmove * Time.deltaTime * moveSpeed), (ymove * Time.deltaTime * waterMoveSpeed)), ForceMode2D.Impulse);
            }
        }

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
    }

    private void OnMove(InputValue value)
    {
        if (input.currentActionMap == input.actions.FindActionMap("Player_Land"))
        {
            xmove = value.Get<Vector2>().x;
        }
        if (input.currentActionMap == input.actions.FindActionMap("Player_Water"))
        {
            Vector2 v = value.Get<Vector2>();
            xmove = v.x;
            ymove = v.y;
        }

        if (value.Get<Vector2>().x != 0)
            animator.SetFloat("LastX", value.Get<Vector2>().x);
    }

    private void OnJump(InputValue value)
    {
        jumpBuffer = jumpBufferTime;
    }

    private void OnPause(InputValue value)
    {
        pauseMenu.Open();
    }
}
