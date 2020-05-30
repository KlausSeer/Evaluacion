using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    public float currHealth;
    public Slider slider;
    bool live;

    float map(float x, float a, float b, float c, float d)
    {
        return ((x - a) / (b - a)) * ((d - c) + c);
    }

    public void TakeDamage(int damage)
    {
        if(currHealth - damage > 0)
            currHealth -= damage;
        else
        {
            currHealth = 0;
            live = false;
        }
        UpdateLife();
    }

    public void UpdateLife()
    {
        slider.value = map(currHealth, 0, maxHealth, 0, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        live = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
