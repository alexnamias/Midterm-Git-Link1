using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Sound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource AS;
    public AudioClip JumpSound;

    public void SoundJump()
    {
        AS.PlayOneShot(JumpSound);
    }
}