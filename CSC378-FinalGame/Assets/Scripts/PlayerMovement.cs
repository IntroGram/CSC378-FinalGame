using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float swingForce = 4f;
    public float speed = 1f;
    public float jumpSpeed = 3f;
    public Vector2 ropeHook;
    public bool isSwinging;
    public bool groundCheck;
    public LayerMask notPlayer;

    public AudioSource jump;
    public AudioSource hit;
    private float audioDelay = 0f;
    private float audioDelaySet = 0.1f;

   // public GameObject GameController;

    private SpriteRenderer playerSprite;
    private Rigidbody2D rBody;
    private bool isJumping;
    private Animator animator;
    private float jumpInput;
    private float horizontalInput;
    private float jumpDelay = 0;

    private bool touchingWall;

    public int initialHealth = 100;
    public Slider healthBar;
    private float health;
    private bool isDead = false;

    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = initialHealth;
        healthBar.value = health;
        AudioSource[] aSources = GetComponents<AudioSource>();
        jump = aSources[0];
        hit = aSources[1];
        jump.volume = 0.3f;
        hit.volume = 0.2f;
    }

    void Update()
    {
        jumpInput = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal");
        var halfHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        groundCheck = Physics2D.Raycast(new Vector2(transform.position.x+0.22f, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.25f, notPlayer);
        if(!groundCheck){
            groundCheck = Physics2D.Raycast(new Vector2(transform.position.x-0.22f, transform.position.y - halfHeight - 0.04f), Vector2.down, 0.25f, notPlayer);
        }
        //Debug.DrawLine(new Vector3(transform.position.x+0.22f, transform.position.y - halfHeight - 0.04f, 0), new Vector3(transform.position.x+0.22f, transform.position.y - halfHeight - 0.04f - 0.30f, 0), Color.white);
        //Debug.DrawLine(new Vector3(transform.position.x-0.22f, transform.position.y - halfHeight - 0.04f, 0), new Vector3(transform.position.x-s0.22f, transform.position.y - halfHeight - 0.04f - 0.30f, 0), Color.white);
        if(jumpDelay != 0){
            jumpDelay -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            playerSprite.flipX = horizontalInput > 0f;
            if (isSwinging)
            {
                animator.SetBool("IsSwinging", true);

                // Get normalized direction vector from player to the hook point
                var playerToHookDirection = (ropeHook - (Vector2) transform.position).normalized;

                // Inverse the direction to get a perpendicular direction
                Vector2 perpendicularDirection;
                if (horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2) transform.position - perpendicularDirection*-2f;
                    Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2) transform.position + perpendicularDirection*2f;
                    Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
                }

                var force = perpendicularDirection * swingForce;
                rBody.AddForce(force, ForceMode2D.Force);
            }
            else
            {
                animator.SetBool("IsSwinging", false);
                /*touchingWall = Physics2D.OverlapCircle(this.transform.position, 1.5f, LayerMask.GetMask("Wall"));
                if (touchingWall && Input.GetButtonDown ("Jump")) 
                {
                    wallJump();
                }*/
                if (groundCheck)
                {
                    var groundForce = speed*2f;
                    rBody.AddForce(new Vector2((horizontalInput*groundForce - rBody.velocity.x)*groundForce, 0));
                    rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y);
                }else{
                    var groundForce = speed*1.7f;
                    rBody.AddForce(new Vector2((horizontalInput*groundForce - rBody.velocity.x)*groundForce, 0));
                }
            }
        }
        else
        {
            animator.SetBool("IsSwinging", false);
            animator.SetFloat("Speed", 0f);
        }

        if (!isSwinging)
        {
            // if (!groundCheck) return;
            touchingWall = Physics2D.OverlapCircle(this.transform.position, 0.6f, LayerMask.GetMask("Wall"));
            if (touchingWall && Input.GetButtonDown ("Jump") && jumpDelay == 0) 
            {
                wallJump();
                //jumpDelay = 0.25f;
            }else if(groundCheck){
                isJumping = jumpInput > 0f;
                if (isJumping)
                {
                    if(audioDelay <= 0){
                        jump.Play();
                        audioDelay = audioDelaySet;
                    }else{
                        audioDelay -= Time.deltaTime;
                    }
                    rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
                }
            }
        }
        animator.SetBool("Jumping", rBody.velocity.y > 0.05 && isJumping && !isSwinging); // Might be a problem for double jumping?
        animator.SetBool("Falling", rBody.velocity.y < -0.05 && !isSwinging);
    }

/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && Mathf.Abs(rBody.velocity.y) <= 0.05)
        {
            groundCheck = true;
            //wallJumping = false;
            //wallJumpNum = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || Mathf.Abs(rBody.velocity.y) <= 0.05)
        {
            groundCheck = true;
        }
    }*/

    void OnDrawGizmosSelected(){ 
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position , 0.6f);
    }

    /*
    public void receiveDamage(float dmgTaken){
        health -= dmgTaken;
        healthBar.value = health;
        if (health <= 0)
        {
            dead = true;
        }
    }
    */

    private void wallJump() 
    {
        int facing = (playerSprite.flipX) ? 1 : -1 ;
        rBody.velocity = new Vector3(10f*facing, 8f, 0);
        jump.Play();
    }

    public void receiveDamage(float dmgTaken){
        health -= dmgTaken;
        healthBar.value = health;
        hit.Play();
        if (health <= 0)
        {
            isDead = true;
        }
    }

    public bool returnDead(){
        return isDead;
    }
}