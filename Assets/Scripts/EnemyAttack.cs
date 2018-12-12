using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    float t = 0.0f;

    // Update is called once per frame
    void Update ()
    {
        t += Time.deltaTime;

        if (t >= 2)
        {
            Destroy(this.gameObject);
        }
	}

}
