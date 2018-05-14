using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class followCamera : MonoBehaviour {

    public GameObject target = null;
    private bool orbitY = true;
    private bool presionado = false;
    public float numberfloat = 0;
    private float rotator;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    /**public void Rotar ()
    {
        if (presionado == false)
        {
            presionado = true;
        }
        if (presionado == true)
        {
            presionado = false;
        }

    }
   public void NoRotar()
    {
        presionado = false;
        
    }*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            presionado = true;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            presionado = false;

        }
        if (presionado)
        {
            rotator += 0.4f;
        }
        else
        {
            rotator -= 0.2f;
        }
     
        
            
        
        if(rotator > numberfloat)
        {
            rotator = numberfloat;
        }
        if (rotator < -0.2f)
        {
            rotator = -0.2f;
        }
        if (target != null)
        {
            transform.LookAt(target.transform);

            if (orbitY)
            {
                transform.RotateAround(target.transform.position,Vector3.up,Time.deltaTime+rotator);
                
            }


        }
    }
}
