using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float attackRange;
    public LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(this.transform.position, attackRange, enemyLayer);
        foreach (Collider2D item in enemies)
        {
            item.GetComponent<Enemy>().OnAttackConnected();
        }
    }

}
