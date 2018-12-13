using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour {
    TransicionEscena _TransicionEscena;
    public GameObject curar;
    public int nivel=0;
    LevelManager LM;
    Stats gameStats;

    private void Start()
    {
        _TransicionEscena = GameObject.FindGameObjectWithTag("TransicionEscena").GetComponent<TransicionEscena>();
        LM = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        gameStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
    }

    public void Play()
    {
        
        _TransicionEscena.CambiarEscenaTransicion("Game_Alquimia");
        gameStats.Curado = true;
    }

    public void Update()
    {
        if (LM.GetCLevel() != LM.GetNLevel()&& gameStats.Curado==false) {
            if(LM.GetNLevel() != 1)
            {
                curar.SetActive(true);
            }
            else
            {
                curar.SetActive(false);
            }
        }

        if (gameStats.Curado == true)
        {
            curar.SetActive(false);
        }
    }

}
