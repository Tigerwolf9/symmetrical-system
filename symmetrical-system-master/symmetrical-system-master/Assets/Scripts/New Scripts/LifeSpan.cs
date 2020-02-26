using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSpan : MonoBehaviour
{
    float timer;
    float lifeSpanMin, lifeSpanMax;
    float life;

    void Start()
    {
        lifeSpanMin = GetComponent<Stats>().lifeSpanMin;
        lifeSpanMax = GetComponent<Stats>().lifeSpanMax;
        life = Random.Range(lifeSpanMin, lifeSpanMax);
    }
    // Update is called once per frame
    void Update()
    {
        Death();
        
    }
    void Death()
    {
        
        if (timer >= life)
        {
            Destroy(gameObject);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
            GetComponent<Stats>().health = life - timer;
        }
    }
}
