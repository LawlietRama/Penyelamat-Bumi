using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public static int score;
    private Text scoreCounterText;

    // Start is called before the first frame update
    void Start()
    {
        scoreCounterText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreCounterText.text = "" + score;
    }
}
