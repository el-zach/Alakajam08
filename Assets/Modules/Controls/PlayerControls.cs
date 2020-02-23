using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControls : MonoBehaviour
{
    [System.Serializable]
    public class InputEvent : UnityEvent<float> { }

    [System.Serializable]
    public class Axis {
        public string name;
        public float output;
        public float value;
        public float oldValue;
        public InputEvent OnDown, OnUp, OnPressed;

        public Axis(string _name)
        {
            name = _name;
        }

        public void Compute()
        {
            output = Input.GetAxis(name);
            value = Mathf.Abs( Input.GetAxisRaw(name) );
            if (value > 0f && oldValue == 0f)
                OnDown.Invoke(output);
            if (value < 1f && oldValue == 1f)
                OnUp.Invoke(output);
            if (value > 0f && oldValue > 0f)
                OnPressed.Invoke(output);
            oldValue = value;
        }

    }

    Movement move;

    public Axis horizontal, vertical, fire, fire2 = new Axis("Fire2");

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
        horizontal.OnDown.AddListener(move.SideBoost);
        vertical.OnDown.AddListener(move.StartBoost);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal.Compute();
        vertical.Compute();
        fire.Compute();
        fire2.Compute();
        move.inputDirection = Vector3.right * horizontal.output + Vector3.up * vertical.output;
        move.inputBoost = Input.GetAxis("Jump");
    }

}
