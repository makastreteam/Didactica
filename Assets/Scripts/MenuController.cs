using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    TransicionEscena _TransicionEscena;
    GameObject gameStats;
    GameObject levelManager;

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            if(GameObject.Find("LevelManager") != null && GameObject.Find("Stats") != null)
            {
                levelManager = GameObject.FindGameObjectWithTag("LevelManager");
                gameStats = GameObject.FindGameObjectWithTag("Stats");

                Destroy(levelManager);
                Destroy(gameStats);
            } 
        }
    }

    void Start()
    {
        _TransicionEscena = GameObject.FindGameObjectWithTag("TransicionEscena").GetComponent<TransicionEscena>();
    }

	public void Play()
    {
        _TransicionEscena.CambiarEscenaTransicion("Map");
    }

    public void Menu()
    {
        _TransicionEscena.CambiarEscenaTransicion("Menu");
        //SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
