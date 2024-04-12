using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isHidden;
    private Animator animator;
    private SpriteRenderer spriteRenderer;    

    // The time the player has before they're exposed & killed. 
    public float maxTimer = 100f;
    // How much the timer will increase and decrease based on how much time has passed.
    public float timerDecreaseRate = 1f;
    public float timerIncreaseRate = 2f;
    // The player's current time. 
    private float currentTimer;

    // Define possible states for the player. 
    // Here we are using an enum. 
    public enum PlayerState
    {
        Idle,
        Walking,
        Inspect
    }

    public PlayerState state;




    void Start()

    {

        currentTimer = maxTimer;

    }



    void Update()

    {

        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * moveInput * moveSpeed * Time.deltaTime);

        if (moveInput != 0)
        {
            state = PlayerState.Walking;
           /// animator.SetInteger("State", (int)state);
            // Flip the sprite based on the direction of movement

            // RIGHT (original scale):
            if (moveInput > 0) 
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); 
            }
            // LEFT (flip the sprite):
            else if (moveInput < 0) 
            {
                transform.localScale = new Vector3(1f, 1f, 1f); 
            }
        }
        else
        {
            state = PlayerState.Idle;
            /// animator.SetInteger("State", (int)state);
        }



        // Timer update based on player's visibility

        if (isHidden)

        {

            currentTimer -= timerDecreaseRate * Time.deltaTime;

        }

        else

        {

            currentTimer += timerIncreaseRate * Time.deltaTime;

        }


        // Ensures that currentTimer remains within the range of 0f to maxTimer.
        // If the currentTime falls below 0f, it will be set to 0f.
        // If it exceeds maxTime, it will be set to maxTime. This is important
        // to ensure the time does not fall below 0. 
        currentTimer = Mathf.Clamp(currentTimer, 0f, maxTimer);

        if (currentTimer == 0)
        {
            Die(); 
        }


    }


        void OnTriggerEnter2D(Collider2D other)

    {

        if (other.CompareTag("Hide"))

        {

            isHidden = true;

        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Get the tag of the collided object
        string tag = collision.gameObject.tag;

        if (Input.GetKeyDown(KeyCode.E)) {
            SceneManager.LoadScene(tag);
        }
        if(Input.GetKeyDown(KeyCode.P)) {
            SceneManager.LoadScene(tag);
            isHidden = false; 
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (tag.Equals("Note"))
            {
                // Pull up note
            }
            else if (tag.Equals("Interactable"))
            {
                // Trigger dialogue 
            }
        }
    }

    void Die()
    {
        // In the death scene, user will be able to see an animation of themselves
        // dying, then be prompted to click play to play again from the beginning. 
        SceneManager.LoadScene("Death");
    }

void OnTriggerExit2D(Collider2D other)

    {
        // Stops hiding the player if exiting a hide object. 
        if (other.CompareTag("Hide"))

        {

            isHidden = false;

        }

    }

}