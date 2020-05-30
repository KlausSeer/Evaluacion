using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tronco : Enemy
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartAttack();
        }
    }
}
