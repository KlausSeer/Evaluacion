using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed;
    public float duration;
    float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        float x = transform.localScale.x * Mathf.Sign(speed);
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        time += Time.deltaTime;
        if(time > duration)
        {
            Destroy(this.gameObject);
        }
    }
}
