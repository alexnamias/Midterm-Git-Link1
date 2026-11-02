/*
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class admin : MonoBehaviour
{

    public Vector2 endpoint;
    public float moving_rate = 0f;


    private Vector2 begginpoint;
    private Vector2 actualobjective;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        begginpoint = transform.position;

        actualobjective = endpoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, actualobjective, moving_rate * Time.deltaTime);

        if (Vector2.Distance(transform.position, endpoint) < 0.001f)
        {
            actualobjective = begginpoint;
        }

        else if (Vector2.Distance(transform.position, begginpoint) < 0.001f)
        {
            actualobjective = endpoint;
        }

    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class admin : MonoBehaviour
{
    public float amplitud = 3f;
    public float velocidad = 2f;
    private float posicionYInicial;


    void Start()
    {
        posicionYInicial = transform.position.y;
    }

    private void Update()
    {
        float nuevaY = posicionYInicial + Mathf.PingPong(Time.time * velocidad, amplitud);

        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
    }
}
*/