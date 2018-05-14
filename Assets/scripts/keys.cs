using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class keys : MonoBehaviour {
        
    [SerializeField]
    private bool active;
    [SerializeField]
    private Mananger mananger;
    [SerializeField]
    private int turn;
    private Renderer colorKey;

    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
        }
    }

    // Use this for initialization
    void Awake () {
        mananger = GameObject.Find("Camera").GetComponent<Mananger>();
        colorKey = gameObject.GetComponent<Renderer>();
        colorKey.material.SetColor("_Color", Color.red);

	}
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update () {

        if (active == true)
        {
            colorKey.material.SetColor("_Color", Color.green);
            
        }
        else
        {
            colorKey.material.SetColor("_Color", Color.red);
            
        }

   

    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (active == true)
        {
            
            playerController jugador = other.gameObject.GetComponent<playerController>();
            if (jugador != null)
            {
                gameObject.GetComponent<AudioSource>().Play();
                TakedKey();
            }
        }
    }
  
   
     public void TakedKey()
     {
        
        Destroy(gameObject);
        mananger.KeyCounter();
      
     }
}
