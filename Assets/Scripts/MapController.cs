using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour {

	public void Play()
    {
        SceneManager.LoadScene("Game_Alquimia", LoadSceneMode.Single);
    }

}
