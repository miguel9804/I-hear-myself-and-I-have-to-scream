using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterTime : MonoBehaviour
{
    public float maxtime = 6f;
    float time;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= maxtime)
        {
            Destroy(this.gameObject);
        }
    }
}
