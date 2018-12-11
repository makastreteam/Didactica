using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour {
    TransicionEscena _TransicionEscena;

    private void Start()
    {
        _TransicionEscena = GameObject.FindGameObjectWithTag("TransicionEscena").GetComponent<TransicionEscena>();
    }

    public void Play()
    {
        _TransicionEscena.CambiarEscenaTransicion("Game_Alquimia");
    }

}
