﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    Movement move;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        move.inputDirection = Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical");
        move.input = Input.GetAxis("Jump");
    }
}