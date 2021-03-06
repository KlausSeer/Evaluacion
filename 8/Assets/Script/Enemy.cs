﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField()]
    HealthManager HM;
    // Start is called before the first frame update
    void Start()
    {
        HM = GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    List<Enemy> Filter(List<Enemy> enemies, List<string> filters)
    {
        List<Enemy> filter = new List<Enemy>();

        bool filtered;
        if(filters != null)
        {
            foreach (Enemy enemy in enemies)
            {
                filtered = false;
                foreach (string str in filters)
                {
                    if (enemy.name == str)
                    {
                        filtered = true;
                        break;
                    }
                }
                if (!filtered)
                    filter.Add(enemy);
            }
        }
        else
        {
            filter = enemies;
        }
            
        return filter;
    }

    Enemy FindNearest(List<Enemy> enemies, List<string> filters)
    {
        List<Enemy> filtered = Filter(enemies, filters);
        filtered.Sort(
            delegate(Enemy a, Enemy b)
            {
                float dA = Vector2.Distance(this.transform.position, a.transform.position);
                float dB = Vector2.Distance(this.transform.position, b.transform.position);

                if (dA <= dB)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });
        return filtered[0];
    }

    void Thunder()
    {
        HM.TakeDamage(10);
        FindObjectOfType<FXController>().PlayFX("Thunder", new Vector3(this.transform.position.x, this.transform.position.y + 3), Quaternion.identity);
    }

    IEnumerator InterLink(int n)
    {
        Thunder();
        List<string> names = new List<string>();
        names.Add(this.name);
        Enemy Next = FindNearest(new List<Enemy>(GameObject.FindObjectsOfType<Enemy>()), names);
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(Next.ChainStart(names, n));
    }

    public IEnumerator ChainStart(List<string> names, int n)
    {
        Thunder();
        if (RandomGenerator.RandomNum(10))
        {
            names.Add(this.name);
            Enemy Next = FindNearest(new List<Enemy>(GameObject.FindObjectsOfType<Enemy>()), names);
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(Next.ChainAttack(names, n));
        }
    }

    public IEnumerator ChainAttack(List<string> names, int n)
    {
        if (names.Count < n)
        {
            Thunder();
            if (RandomGenerator.RandomNum(10))
            {
                names.Add(this.name);
                Enemy Next = FindNearest(new List<Enemy>(GameObject.FindObjectsOfType<Enemy>()), names);
                yield return new WaitForSeconds(0.4f);
                StartCoroutine(Next.ChainAttack(names, n));
            }
        }
    }

    //void Interlink()
    //{
    //    Thunder();
    //    List<string> names = new List<string>();
    //    names.Add(this.name);
    //    Enemy Next = FindNearest(new List<Enemy>(GameObject.FindObjectsOfType<Enemy>()), names);
    //    Next.ChainStart(names, 3);
    //}


    //public void ChainStart(List<string> names, int n)
    //{
    //    Thunder();
    //    if (RandomGenerator.Random(10))
    //    {
    //        names.Add(this.name);
    //        Enemy Next = FindNearest(new List<Enemy>(GameObject.FindObjectsOfType<Enemy>()), names);
    //        Next.ChainAttack(names, n);
    //    }

    //}



    //public void ChainAttack(List<string> names, int n)
    //{
    //    if (names.Count < n)
    //    {
    //        Thunder();
    //        if (RandomGenerator.Random(10))
    //        {
    //            names.Add(this.name);

    //            Enemy Next = FindNearest(new List<Enemy>(GameObject.FindObjectsOfType<Enemy>()), names);
    //            Next.ChainAttack(names, n);
    //        }
    //    }
    //}

    public void OnAttackConnected()
    {
        HM.TakeDamage(10);
        if (RandomGenerator.RandomNum(10))
        {
            this.StartCoroutine(InterLink(3));
            //InterLink(3);
        }
    }
}
