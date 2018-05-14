using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour {
    public Camera cam;
    public NavMeshAgent agent;
    [SerializeField]
    private Transform teleporterB, teleporterA, teleporterEnd;
    
    [SerializeField]
    private Mananger mananger;
  
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!mananger.GameOvered)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //move the agent
                    agent.SetDestination(hit.point);
                }
            }
        }

	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "teleporterB")
        {
            
            if (mananger.KeysGeted < mananger.levelKeys)
            {

                agent.Warp(new Vector3(teleporterB.position.x, teleporterB.position.y, teleporterB.position.z));
               
            }
            else
            {
                agent.Warp(new Vector3(teleporterEnd.position.x, teleporterEnd.position.y, teleporterEnd.position.z));
             
            }

        }
        if (other.tag == "teleporterA")
        {
         
            agent.Warp(new Vector3(teleporterA.position.x, teleporterA.position.y , teleporterA.position.z));
         
        }

        if (other.tag == "enemy")
        {
            mananger.Loose();
        }
        if (other.tag == "end")
        {
            mananger.GameOver();
        }


    }
}
