using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXController : MonoBehaviour
{
    public FX[] FX;
 
    FX FindByName(string name)
    {
        foreach (FX item in FX)
        {
            if (item.name == name)
                return item;
        }
        return null;
    }

    public void PlayFX(string Effect, Vector3 pos, Quaternion Rot)
    {
        FX effect = FindByName(Effect);
        if (effect != null)
        {
            Instantiate(effect.Object, pos, Rot);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
