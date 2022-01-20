using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private SpawnManager spawnManager;
    public float jumpForce = 10;
    public LayerMask groundLayer;
    public int jumpMaxCount = 2;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private Transform groundDetect;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameManager.onGameStarted += OnGameStarted;
        groundDetect = transform.Find("DetectGround");
        animator = GetComponent<Animator>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && gameManager.State == GameManager.GameState.RUNNING && jumpCount < jumpMaxCount)
        {
            jumpCount++;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    void OnGameStarted()
    {
        animator.SetBool("IsRunning", true);
    }

    void FixedUpdate()
    {
        bool touchingGround = Physics2D.OverlapCircle(groundDetect.position, 0.01f, groundLayer);

        if (touchingGround && !isGrounded)
        {
            jumpCount = 0;
            isGrounded = true;
            if (gameManager.State == GameManager.GameState.RUNNING)
            {
                animator.SetBool("IsRunning", true);
            }
        }
        else if (!touchingGround)
        {
            isGrounded = false;
            animator.SetBool("IsRunning", false);
        }

        if (transform.position.y < -spawnManager.maxYRange - spawnManager.maxYGap - 20)
        {
            gameManager.FinishGame();
        }
    }
}
