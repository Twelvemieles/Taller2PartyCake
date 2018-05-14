using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Botones : MonoBehaviour {

    public string actualScene;
    public string GameScene;
    
    public string HomeScene;
    public string InfoScene;



    public void BotonRepetir ()
    {
        SceneManager.LoadScene(actualScene);

    }


    public void BotonGame()
    {
        SceneManager.LoadScene(GameScene);
    }
    public void BotonInfo()
    {
        SceneManager.LoadScene(InfoScene);
    }



    public void BotonHome()
    {
        SceneManager.LoadScene(HomeScene);
    }




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
