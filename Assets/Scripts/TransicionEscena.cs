using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionEscena : MonoBehaviour {

    public GameObject canvas;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void CambiarEscenaTransicion(string nombreEscena)
    {
        canvas.SetActive(false);
        StartCoroutine(changeSceneGame(nombreEscena));
    }

    IEnumerator changeSceneGame(string nombreEscena)
    {
        anim.SetTrigger("changeScene");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nombreEscena, LoadSceneMode.Single);
    }
}
