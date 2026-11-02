using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

public class Up_Down : MonoBehaviour
{
    public float Up_down = 3f;
    //This should be the amplitud of the up and dowm of the object

    public float velocity = 2f;
    //should be the rate of the up and dowm

    private float position_and_Inicial;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position_and_Inicial = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float New_y = position_and_Inicial + Mathf.PingPong(Time.time * velocity, Up_down);

        transform.position = new Vector3(transform.position.x, New_y, transform.position.z);

    }

    private void update()
    {
    }
}



