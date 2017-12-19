using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {

    public float lifeTime;
    float time = 0;
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > lifeTime)
            Destroy(gameObject);
	}
}
