using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootScript: MonoBehaviour {

    [SerializeField]
    private EstadoPatrulla patrullaScript;
    [SerializeField]
    private EstadoSiguiendo siguiendoScript;
    [SerializeField]
    private EstadoAvisado avisadoScript;
  
    private Vector3 target;

   
    private int estado;
    /*
     * 0 = patrullando
     * 1 = siguiendo
     * 3 = advertido
     */

    [SerializeField]
    private NavMeshAgent personaje;

    public int Estado
    {
        get
        {
            return estado;
        }

        set
        {
            estado = value;
        }
    }

    public Vector3 Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    public NavMeshAgent Personaje
    {
        get
        {
            return personaje;
        }

        set
        {
            personaje = value;
        }
    }

    // Use this for initialization
    void Start () {
        Estado = 0;
       // Personaje = gameObject.GetComponent<NavMeshAgent>();
       // avisadoScript = gameObject.GetComponent<EstadoAvisado>();
        //siguiendoScript = gameObject.GetComponent<EstadoSiguiendo>();
        //patrullaScript = gameObject.GetComponent<EstadoPatrulla>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (Estado)
        {
            case 0:
                patrullaScript.Patrullando();
                break;
            case 1:
                siguiendoScript.Siguiendo();
                break;
            case 3:
                Advertido();
                break;
        }
          
	}
   public void Advertido()
    {
        avisadoScript.GoToAdvertido(Target);
    }
}


