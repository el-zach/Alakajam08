﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    class DamageNumber
    {
        public GameObject go;
        public float timeOfCreation;
        Vector2 direction;

        public DamageNumber(int value, Vector3 pos)
        {
            Vector2 screenPos = UserInterface.main.WorldToScreenPoint(pos);
            Vector3 canvasPos = new Vector3(screenPos.x / Screen.width * 1920, screenPos.y / Screen.height * 1080, 0f);
            go = Instantiate(Instance.dmgNumber, screenPos, Quaternion.identity, canvas);
            UnityEngine.UI.Text txt = go.GetComponent<UnityEngine.UI.Text>();
            txt.text = value.ToString();
            if (value == 0) txt.color = Color.gray;
            txt.fontSize =Mathf.FloorToInt(txt.fontSize* Random.Range(0.9f, 1.2f));
            timeOfCreation = Time.time;
            direction = Random.insideUnitCircle;
        }

        public bool Update()
        {
            go.transform.Translate(direction * Time.deltaTime * Instance.numberSpeed);
            if (Time.time - timeOfCreation > Instance.numberTime)
            {
                Destroy(go);
                return true;
            }

            return false;
        }

    }

    public GameObject dmgNumber;
    static Camera main;
    static Transform canvas;
    public float numberTime = 0.6f;
    public float numberSpeed = 3f;
    public static UserInterface Instance;

    static List<DamageNumber> numbers = new List<DamageNumber>();

    private void Start()
    {
        main = Camera.main;
        canvas = transform;
        Instance = this;
        numbers = new List<DamageNumber>();
    }

    private void Update()
    {
        List<int> toRemove = new List<int>();
        for(int i =0; i < numbers.Count; i++)
        {
            if (numbers[i].Update())
                toRemove.Add(i);
        }
        for(int i = toRemove.Count-1; i >= 0; i--)
        {
            numbers.RemoveAt(i);
        }
    }

    public static void RenderDamageNumbers(int value, Vector3 pos)
    {
        numbers.Add(new DamageNumber(value, pos));
    }
}
