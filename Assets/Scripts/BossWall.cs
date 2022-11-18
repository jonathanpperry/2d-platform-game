using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour
{ 
    void Update()
    {
        if (FindObjectOfType<BossHealthManager>())
        {
            return;
        }
        Destroy(gameObject);
    }
}
