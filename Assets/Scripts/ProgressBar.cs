using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    public int minimum;
    public int maximum;
    public int current;
    public Color newColor;
    [SerializeField] Image mask;
    [SerializeField] Image fill;
    // Start is called before the first frame update
    void Start()
    {
        maximum = HealthManager.instance.maxHealth;
        current = HealthManager.instance.currentHealth;
        newColor = new Color(0.34f, 0.73f, 0.38f);
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        current = HealthManager.instance.currentHealth;
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        if(mask.fillAmount>0.4f)
        {
            mask.color = newColor;
        }
        else if(mask.fillAmount>0.2f)
        {
            mask.color = new Color(0.9725491f, 0.8745099f, 0.06666667f);
        }
        else
        {
            mask.color = Color.red;
        }
        mask.fillAmount = fillAmount;
    }
}
