using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoPatrulla : MonoBehaviour
{
    [SerializeField]
    private RootScript rootScript;
    [SerializeField]
    List<pathEnemy> puntosPatrullar;
    int posicionpatrulla;
    bool moviendo;
    bool esperando;
    bool adelantepatrulla;
    float temporizador;
    bool patrullaesperando;
    float tiempoEspera = 3f;
    float probabilidadCambio = 0.2f;
    // Use this for initialization
    
    void Start ()
    {
        posicionpatrulla = 0;
        FijarDireccion();
        //rootScript = gameObject.GetComponent<RootScript>();
    }
	
	// Update is called once per frame
	public void Patrullando () {
        if (rootScript.Estado == 0)
        {
            if (moviendo && rootScript.Personaje.remainingDistance <= 1.0f)
            {
                //ve si está cerca del destino
                moviendo = false;

                if (patrullaesperando)
                {
                    esperando = true;
                    temporizador = 0f;
                }
                else
                {
                    CambiarDestino();
                    FijarDireccion();
                }
            }

            if (esperando)
            {
                temporizador += Time.deltaTime;
                if (temporizador >= tiempoEspera)
                {
                    esperando = false;
                    CambiarDestino();
                    FijarDireccion();
                }
            }
        }
    }
    private void FijarDireccion()
    {
        if (puntosPatrullar != null)
        {
            Vector3 targetVector = puntosPatrullar[posicionpatrulla].transform.position;
            rootScript.Personaje.SetDestination(targetVector);
            moviendo = true;

        }
    }
    private void CambiarDestino()
    {
        //con una probabilidad decide si ir hacia adelante o hacia atrás, si va 
        //hacia adelante, recorre una posición en la lista
        if (Random.Range(0f, 1f) <= probabilidadCambio)
        {
            adelantepatrulla = !adelantepatrulla;
        }
        if (adelantepatrulla)
        {
            /**
            posicionpatrulla++;
            if (posicionpatrulla >= puntosPatrullar.Count)
            {
                posicionpatrulla = 0;
            */

            //hace lo mismo pero más barato
            posicionpatrulla = (posicionpatrulla + 1) % puntosPatrullar.Count;
        }
        else
        {
            if (--posicionpatrulla < 0)
            {
                posicionpatrulla = puntosPatrullar.Count - 1;
            }
        }
    }
}
