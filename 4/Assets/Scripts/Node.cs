using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int index;
    public int index2 = -1;
    public Node[] Adj;
    public Node Pair;
    public bool Connected;
    RectTransform trans;

    int Opposite(int index)
    {
        return (index + 2) % 4;
    }

    void Check()
    {
            Node n = Adj[index];
            if (n != null)
            {
                n.Connect(this);
            }
        //else
        //{
        //    Node n = Adj[index];
        //    if (n != null)
        //    {
        //        if(n.index2 != -1)
        //        {
        //            Connect2(Pair, true);
        //        }
        //    }
        //}
    }

    void Connect2(Node me, bool first)
    {
        if(first)
        {
            Node n = Adj[index2];
            if (n!= null)
            {
                if(n.index2 == -1)
                {
                    if (n.index == Opposite(index2) && !n.Connected)
                    {
                        n.Connected = true;
                        Connected = true;
                        n.Pair = me;
                    }
                }
                else
                {
                    if (n.index == Opposite(index2))
                    {
                        n.Connected = true;
                        n.Pair = me;
                        n.Connect2(me, true);
                    }
                    else if (n.index2 == Opposite(index2))
                    {
                        n.Connected = true;
                        n.Pair = me;
                        n.Connect2(me, false);
                    }
                }
            }
        }
        else
        {
            Node n = Adj[index];
            if (n != null)
            {
                if (n.index2 == -1)
                {
                    if (n.index == Opposite(index) && !n.Connected)
                    {
                        n.Connected = true;
                        Connected = true;
                        n.Pair = me;
                    }
                }
                else
                {
                    if (n.index == Opposite(index))
                    {
                        n.Connected = true;
                        n.Pair = me;
                        n.Connect2(me, true);
                    }
                    else if (n.index2 == Opposite(index))
                    {
                        n.Connected = true;
                        n.Pair = me;
                        n.Connect2(me, false);
                    }
                }
            }
        }
    }

    void Connect(Node me)
    {
        if(index2 == -1)
        {
            if(index == Opposite(me.index))
            {
                Connected = true;
                me.Connected = true;
                Pair = me;
                me.Pair = this;
            }
           
        }
        else
        {
            if(index == Opposite(me.index))
            {
                Connected = true;
                me.Connected = true;
                Pair = me;
                me.Pair = this;
                Connect2(me, true);
            }
            else if(index2 == Opposite(me.index))
            {
                Connected = true;
                me.Connected = true;
                Pair = me;
                me.Pair = this;
                Connect2(me, false);
            }
        }
    }

    void Rotate()
    {
        if(index2 == -1)
        {
            float rotation = 90 * (index -1);
            trans.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, -rotation));
        }
        else if(index == Opposite(index2))
        {
            float rotation = 90 * (index - 1);
            trans.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, -rotation));
        }
        else
        {
            float rotation = 90 * (index - 3);
            trans.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, -rotation));
        }
    }

    public void Click()
    {
        if (index2 != -1)
        {
            index2++;
            index2 %= 4;
        }
        if(Pair != null)
        {
            if(index2 == -1)
            {
                if(Pair.index == Opposite(index))
                {

                    Pair.Connected = false;
                    Pair = null;
                }
            }
        }
        Connected = false;   
        index++;
        index %= 4;
        Rotate();
        Check();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<RectTransform>();
        Rotate();
        Check();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
