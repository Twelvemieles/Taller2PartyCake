using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mananger : MonoBehaviour
{
    [SerializeField]
    private keys[] keysArray;
   
    private Queue<keys> keysCola = new Queue<keys>();
    
    private int keysGeted;
    public int levelKeys;
    [SerializeField]
    private Text keyText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text gaOvScore;
   
    [SerializeField]
    private Transform apuntador;
    [SerializeField]
    private Transform transporterEnd;
    [SerializeField]
    private GameObject gameOverText;
    [SerializeField]
    private GameObject canvasText;
    [SerializeField]
    private GameObject PerderText;
    [SerializeField]
    private float TiempoScore = 40;
    private bool gameOvered;

    public int KeysGeted
    {
        get
        {
            return keysGeted;
        }


    }

    public bool GameOvered
    {
        get
        {
            return gameOvered;
        }
    }



    // Use this for initialization
    void Awake()
    {
        foreach (keys i in keysArray)
        {
            keysCola.Enqueue(i);
        }
    }
    void Start()
    {
        canvasText.SetActive(true);
        gameOverText.SetActive(false);
        PerderText.SetActive(false);
        levelKeys = keysArray.Length;
        keysCola.Peek().Active = true;
        apuntador.position = new Vector3(keysCola.Peek().transform.position.x, apuntador.position.y, keysCola.Peek().transform.position.z);
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (TiempoScore <= 0)
        {
            TiempoScore = 0;
            Loose();
        }
        else
        {
            if (!gameOvered)
            {
                TiempoScore -= Time.deltaTime;
            }
        }
        
        UpdateTime();


    }
    public void KeyCounter()
    {
        keysGeted++;
        UpdateScore();
        keysCola.Dequeue();
        if (keysCola.Count > 0)
        {
            keys llave = keysCola.Peek();
            llave.Active = true;
            apuntador.position = new Vector3(keysCola.Peek().transform.position.x, apuntador.position.y, keysCola.Peek().transform.position.z);
        }
        else
        {
            apuntador.position = new Vector3(transporterEnd.position.x, apuntador.position.y, transporterEnd.position.z);
        }


    }
    void UpdateScore()
    {
        keyText.text = "Llaves :" + KeysGeted + "/" + levelKeys;
    }
    void UpdateTime()
    {
        timeText.text = "" + TiempoScore;
    }
    public void GameOver()
    {
        gameOverText.SetActive(true);
        canvasText.SetActive(false);
        gameOvered = true;
        gaOvScore.text = "Score:" + TiempoScore;
    }
    public void Loose()
    {
        
        PerderText.SetActive(true);
        canvasText.SetActive(false);
        
        gameOvered = true;
    }
}
