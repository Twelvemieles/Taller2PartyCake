using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoSiguiendo : MonoBehaviour {
    [SerializeField]
    private FieldOfView vigilante;

    [SerializeField]
    private RootScript rootScript;

    private void Start()
    {
       // rootScript = gameObject.GetComponent<RootScript>();
        //vigilante = gameObject.GetComponent<FieldOfView>();
    }
    public void Siguiendo()
    {
        Vector3 targetVector = vigilante.VisibleTargets[0].position;
        rootScript.Personaje.SetDestination(targetVector);
    }
}
