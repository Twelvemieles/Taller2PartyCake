using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathEnemy : MonoBehaviour {
    [SerializeField]
    protected float debugDrawRadious = 1.0f;
	
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadious);
    }
	
}
