using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrchester : MonoBehaviour
{
    public List<Enemy> enemies;

    int countN;
    float countT;
    public int N;
    public float T;
    public int R;

    Enemy FindEnemyWithTag(string[] tags)
    {
        foreach (string tag in tags)
        {
            foreach (Enemy item in enemies)
            {
                foreach (EnemyAttack attack in item.attacks)
                {
                    foreach (string react in attack.reactionTags)
                    {

                        if (react == tag)
                            return item;
                    }
                }
            }
        }
        return null;
    }

    void Attack(Notification aNotification)
    {
        StartCoroutine(AttackRoutine(aNotification));
    }

    IEnumerator AttackRoutine(Notification aNotification)
    {
        Enemy Target = aNotification.sender.GetComponent<Enemy>();
        NotificationCenter.DefaultCenter().PostNotification(aNotification.sender, "PauseAttack");
        yield return new WaitForSeconds(Target.GetComponent<Enemy>().lastAttack.attackDuration);
        NotificationCenter.DefaultCenter().PostNotification(aNotification.sender, "ResumeAttack");
        if (RandomGenerator.RandomNum(R))
        {
            if (countN >= N && countT >= T)
            {
                countN = 0;
                countT = 0.0f;
                string[] attacks = Target.lastAttack.attackTags;
                Enemy Next = FindEnemyWithTag(attacks);
                Debug.Log("El sistema ha originado un ataque de orquesta entre enemigo " + Target.name + " y " + Next.name);
                if (!Next.StartAttack())
                {
                    Debug.Log("No se pudo realizar ataque de Orquesta");
                    countN = N;
                    countT = T + 0.01f;
                }
                    
            }
            else
            {
                countN++;
                if (countN == N)
                    Debug.Log("El número mínimo de ataques simples para desbloquear un ataque orquesta ha sido alcanzado.");
            }

        }
        

    }

    // Start is called before the first frame update
    void Start()
    {
        countN = N;
        countT = T + 0.01f;
        NotificationCenter.DefaultCenter().AddObserver(this, "Attack");
    }

    // Update is called once per frame
    void Update()
    {
        countT += 0.01f;
        if(countT >= T  && countT <= T + 0.01f)
            Debug.Log("La ventana de tiempo de bloqueo de ataque en orquesta ha terminado.");
    }
}
