using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAvisado : MonoBehaviour {


    [SerializeField]
    private RootScript rootScript;
    // Use this for initialization

    private void Start()
    {
        //rootScript = gameObject.GetComponent<RootScript>();
    }
    public void GoToAdvertido(Vector3 targetVector)
    {
        
        rootScript.Personaje.SetDestination(targetVector);
    }
}
