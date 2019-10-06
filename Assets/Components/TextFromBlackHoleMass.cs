﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextFromBlackHoleMass : MonoBehaviour
{
    protected Text text;
    public BlackHole blackHole;

    protected void Awake() {
        text = GetComponent<Text>();
        blackHole = GameObject.FindGameObjectWithTag("BlackHole").GetComponent<BlackHole>();
    }

    protected void Update()
    {
        text.text = blackHole.Mass.ToString("0");
    }
}
