using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Node[] nodes;
    public GameObject Panel;

    bool Victory()
    {
        foreach (Node node in nodes)
        {
            if (!node.Connected)
                return false;
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Victory())
        {
            Panel.SetActive(true);
        }
    }
}
