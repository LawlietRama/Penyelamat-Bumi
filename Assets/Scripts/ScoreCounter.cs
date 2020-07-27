using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static int[] score;
    private Text scoreCounterText;
    private string parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.name;
        scoreCounterText = GetComponent<Text>();
        score = new int[10];
    }

    // Update is called once per frame
    void Update()
    {
        if (parent == "Botol")
        {
            scoreCounterText.text = "" + score[0];
        }
        else if (parent == "Daun")
        {
            scoreCounterText.text = "" + score[1];
        }
        
    }
}
