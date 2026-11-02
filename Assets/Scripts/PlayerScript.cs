using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //These are the player's Variables, the raw info that defines them
    
    //The Rigidbody2D is a component that gives the player physics, and is what we use to move
    public Rigidbody2D RB;

    //TextMeshPro is a component that draws text on the screen.
    //We use this one to show our score.
    public TextMeshPro ScoreText;
    
    //This will control how fast the player moves
    public float Speed = 5;

    public float JumpForce;

    public bool IsGround = false;
    private bool jump;
    
    //This is how many points we currently have
    public int Score = 0;

    //buffs and debuffs codes

    private float OriginalSpeed;

    public float BuffDuration = 5f;

    //private Coroutine SpeedEffect; Coroutine I look it up This should store the current effect needs to cheack this and learn it

    private float BuffTimer = 0;

    private bool Has_Speed_Effect = false;

    //jumps

    private float OriginalJumpForce;

    public float Jump_Buff_Duration = 5f;


    private float Jump_buff_Timer = 0;
    private bool Has_Jump_Effect = false;

    public bool Sword = false;

    public bool Gravity_Change = false;

    public GameObject Sword1;

    public static string LastLevel;

    public Player_Sound PlayerSoundController;


    //Start automatically gets triggered once when the objects turns on/the game starts
    void Start()
    {
        //During setup we call UpdateScore to make sure our score text looks correct
        UpdateScore();

        OriginalSpeed = Speed; // This should stores the original speed

        OriginalJumpForce = JumpForce;

        Sword1.SetActive(false);
    }

    //Update is a lot like Start, but it automatically gets triggered once per frame
    //Most of an object's code will be called from Update--it controls things that happen in real time
    void Update()
    {
        //The code below controls the character's movement
        //First we make a variable that we'll use to record how we want to move
        Vector2 vel = new Vector2(RB.linearVelocity.x, RB.linearVelocity.y);
        
        //Then we use if statement to figure out what that variable should look like
        
        //If I hold the right arrow key, the player should move right. . .
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            vel.x = Speed;
        }
        //If I hold the left arrow, the player should move left. . .
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            vel.x = -Speed;
        }
        //If I hold the up arrow, the player should move up. . .
       /* 
        if (Keyboard.current.upArrowKey.isPressed)
        {
            vel.y = Speed;
        }
       */
        //If I hold the down arrow, the player should move down. . .
        if (Keyboard.current.downArrowKey.isPressed)
        {
            vel.y = -Speed;
        }
        
        //Finally, I take that variable and I feed it to the component in charge of movement
        RB.linearVelocity = vel;

        if (Keyboard.current.upArrowKey.wasPressedThisFrame && IsGround == true)
        {
            PlayerSoundController.SoundJump();
            RB.linearVelocity = new Vector2(RB.linearVelocity.x, JumpForce);
            IsGround = false;
        }

        if (Has_Speed_Effect == true)
        {
            BuffTimer -= Time.deltaTime;

            if (BuffTimer <= 0)
            {
                Speed = OriginalSpeed;

                Has_Speed_Effect = false;
            }
        }

        if (Has_Jump_Effect == true)
        {
            Jump_buff_Timer -= Time.deltaTime;

            if (Jump_buff_Timer <= 0)
            {
                JumpForce = OriginalJumpForce;

                Has_Jump_Effect = false;
            }
        }
        if (Sword == true)
        {
            if(Keyboard.current.xKey.isPressed)
            {
                Sword1.SetActive(true);
            }
            else
            {
                Sword1.SetActive(false);
            }
        }
    }

    //This gets called whenever you bump into another object, like a wall or coin.
    private void OnCollisionEnter2D(Collision2D other)
    {
        //This checks to see if the thing you bumped into had the Hazard tag
        //If it does...
        if (other.gameObject.CompareTag("Hazard"))
        {
            //Run your 'you lose' function!
            Die();
        }
        
        //This checks to see if the thing you bumped into has the CoinScript script on it
        CoinScript coin = other.gameObject.GetComponent<CoinScript>();
        //If it does, run the code block belows
        if (coin != null)
        {
            //Tell the coin that you bumped into them so they can self destruct or whatever
            coin.GetBumped();
            //Make your score variable go up by one. . .
            Score++;
            //And then update the game's score text
            UpdateScore();
        }

        if (other.gameObject.CompareTag("Win"))
        {
            Win();
        }

        if (other.gameObject.CompareTag("Win1"))
        {
            Win1();
        }

        if (other.gameObject.CompareTag("Win2"))
        {
            Win2();
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            Ground();
        }

        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            SpeedBoost();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("SlowDebuff"))
        {
            SlowDebuff();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("JumpBoost"))
        {
            JumpBoost();
            Destroy(other.gameObject);
        }

       if (other.gameObject.CompareTag("JumpDeBuff"))
        {
            JumpDeBuff();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("SwordBuff"))
        {
            SwordBuff();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("GravityBuff"))
        {
            GravityBuff();
            Destroy(other.gameObject);
        }
        /*
        if(Sword == true)
        {
                Destroy(other.gameObject);
        }
        */
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            IsGround = false;
        }
    }

    //This function updates the game's score text to show how many points you have
    //Even if your 'score' variable goes up, if you don't update the text the player doesn't know
    public void UpdateScore()
    {
        ScoreText.text = "Score: " + Score;
    }

    //If this function is called, the player character dies. The game goes to a 'Game Over' screen.
    public void Die()
    {
        LastLevel = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Game Over");
    }

    public void Win()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Win1()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void Win2()
    {
        SceneManager.LoadScene("Win_The_Game");
    }


    public void Ground()
    {
        IsGround = true;
    }

    public void SpeedBoost()
    {
        Speed = OriginalSpeed * 2;

        BuffTimer = BuffDuration;

        Has_Speed_Effect = true;
    }

    public void SlowDebuff()
    { 
        Speed = OriginalSpeed / 2;

        BuffTimer = BuffDuration;

        Has_Speed_Effect = true;
    }

    public void JumpBoost()
    { 
        JumpForce = OriginalJumpForce * 2;

        Jump_buff_Timer = Jump_Buff_Duration;

        Has_Jump_Effect = true;
    }

    public void JumpDeBuff()
    {
        JumpForce = OriginalJumpForce / 2;

        Jump_buff_Timer = Jump_Buff_Duration;

        Has_Jump_Effect = true;
    }

    public void SwordBuff()
    {
        Sword = true;
    }    

    public void GravityBuff()
    {
        Gravity_Change = true;
        RB.gravityScale = -1;
    }
}
