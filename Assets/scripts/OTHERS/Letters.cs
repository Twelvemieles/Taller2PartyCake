using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letters : MonoBehaviour {
    private Vector3 target;
	// Use this for initialization
	void Start () {
        target = new Vector3(0, 0, 0);

    }
	
	// Update is called once per frame
	void Update () {

       
            transform.RotateAround(target, Vector3.up, Time.deltaTime - 0.1f);

        
    }
}
