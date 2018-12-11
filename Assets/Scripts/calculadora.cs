using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class calculadora : MonoBehaviour {

    public Text resultado;
    public Text lblvalor1;
    public Text lblvalor2;
    public Text operacion;
    private string textoimprimir;
    string valor1;
    string valor2;
    string operacionActual;
    double rta;
    int nivel = 0;
    int curacion;
    Stats gameStats;
    TransicionEscena _TransicionEscena;

    private void Start()
    {
        gameStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        _TransicionEscena = GameObject.FindGameObjectWithTag("TransicionEscena").GetComponent<TransicionEscena>();
        curacion = gameStats.GetPlayerHealth();
    }

    public void BorrarC()
    {

        resultado.text = "?";
    }

    IEnumerator MostrarVida()
    {
            yield return new WaitForSeconds (2);
            Debug.Log("Funciona");
        _TransicionEscena.CambiarEscenaTransicion("Map");
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Funciona");
    }

    public void igual()
    {
        operacionActual = operacion.text;
        valor1 = lblvalor1.text;
        valor2 = lblvalor2.text;
        if (resultado.text != "?")
        {
            if (resultado.text == operaciones(valor1, valor2)) {
                nivel += 1;
                curacion += 25;
                Debug.Log("vida++");
                if (nivel == 3)
                {
                    StartCoroutine("MostrarVida");
                    gameStats.SetPlayerHealth(curacion);
                }
                else
                {
                    StartCoroutine("NextLevel");
                }
                
            }
            else{
                Debug.Log("Trabaja");
                StartCoroutine("MostrarVida");
            }
        }
    }

    public string operaciones(string n1, string n2)
    {
        string respuesta = "";
        if ("+" == operacion.text)
        {

            respuesta = (int.Parse(n1) + int.Parse(n2)).ToString();
        }
        else if ("-"==operacion.text){
            respuesta = (int.Parse(n1) - int.Parse(n2)).ToString();
        }
        else
        {
            respuesta = (int.Parse(n1) * int.Parse(n2)).ToString();
        }
        return respuesta;
    }
}
