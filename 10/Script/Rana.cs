using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rana : Enemy
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartAttack();
        }
    }
}
