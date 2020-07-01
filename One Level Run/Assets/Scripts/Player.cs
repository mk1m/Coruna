using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    private Animator animator;
    private Rigidbody2D rb;
    public float moveSpeed = 40f;
    private float dirX;
    private bool facingRight = true;
    private Vector3 localScale;
    public float jumpForce = 400f;

    float fJumpPressedRemember = 0;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;

    float fGroundedRemember = 0;
    [SerializeField]
    float fGroundedRememberTime = 0.25f;


    [Header("VFX//Sound")]
    public int health = 200;
    [SerializeField] private GameObject deathVFX;
    public float durationOfExplosion;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] [Range(0, 1)] float jumpSoundVolume = 0.7f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;

        // if jump button is pressed, add force upwards for set amount
        // allows player to automatically jump even if not grounded
        // timer to jump if not grounded (0.25 seconds)
        fGroundedRemember -= Time.deltaTime;
        if (rb.velocity.y == 0)
        {
            fGroundedRemember = fGroundedRememberTime;
        }

        fJumpPressedRemember -= Time.deltaTime;
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;
        }

        if (fJumpPressedRemember > 0 && (fGroundedRemember > 0))
        {
            fJumpPressedRemember = 0;
            fGroundedRemember = 0;
            AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position, jumpSoundVolume);
            rb.AddForce(Vector2.up * jumpForce);
        }
            
        // if there is no vertical velocity, then the player is running
        // Grid is sloped minuscule amount; needed to set min and max not equal to 0
        if (Mathf.Abs(dirX) > 0 && rb.velocity.y <= 0.05 && rb.velocity.y >= -0.05)
        {
            animator.SetBool("isRunning", true);
        }
        else animator.SetBool("isRunning", false);

        // vertical , jump
        // conditiions to check if player is jumping or falling
        if (rb.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }

        if (rb.velocity.y > 0.05)
        {
            animator.SetBool("isJumping", true);
        }

        if (rb.velocity.y < -0.05)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }        
    }

    // called every fixed framerate
    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, rb.velocity.y);
    }

    // chaning direction
    private void LateUpdate()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    // not used
    public void Die()
    {
        FindObjectOfType<Level>().LoadEnd();
        Destroy(gameObject);

        //video effects for player death
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
    }

    // not used
    public int GetHealth()
    {
        return health;
    }

}
