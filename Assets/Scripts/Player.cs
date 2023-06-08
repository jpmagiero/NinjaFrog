using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float jumpForce;
    public bool isJumping;
    public bool doubleJump;

    private Rigidbody2D rig;
    private Animator animator;

    bool isBlowing;


    // Start is called before the first frame update
    void Start()
    {
        //Get the object component
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        //Add Movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        
        if(Input.GetAxis("Horizontal") > 0f)
        {
            animator.SetBool("walk", true);
            //Rotate player
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)
        {
            animator.SetBool("walk", true);
            //Rotate player
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(Input.GetAxis("Horizontal") == 0f)
        {
            animator.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isBlowing)
        {   
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                 animator.SetBool("jump", true);
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    //Collision Detect
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Layer 8 is the ground
        if(collision.gameObject.layer == 8)
        {
            isJumping = false;
            animator.SetBool("jump", false);
        }

        //Detect Spike Collision
         if(collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }

         if(collision.gameObject.tag == "Saw")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }

     void OnTriggerStay2D(Collider2D collider)
    {
        //Verify player collider on Fan
        if(collider.gameObject.layer == 11)
        {
            isBlowing = true;
        }
    }

     void OnTriggerExit2D(Collider2D collider)
    {
        //Verify player collider
        if(collider.gameObject.layer == 11)
        {
            isBlowing = false;
        }
    }
}