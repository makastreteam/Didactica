using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class calculadora : MonoBehaviour {

    public Text resultado;
    public Text lblvalor1;
    public Text lblvalor2;
    public Text operacion;
    public Image potion1;
    public Image potion2;
    public Image potion3;
    public Image potion4;

    private string textoimprimir;
    string valor1;
    string valor2;
    string operacionActual;
    double rta;
    int nivel = 0;
    int curacion;
    Stats gameStats;
    TransicionEscena _TransicionEscena;
    float tiempo;
    float alpha;

    private void Start()
    {
        gameStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        _TransicionEscena = GameObject.FindGameObjectWithTag("TransicionEscena").GetComponent<TransicionEscena>();
        curacion = gameStats.GetPlayerHealth();

        if (gameStats.GetLevel() >= 10)
        {
            operacion.text = "x";
            lblvalor1.text = Random.Range(0, 11).ToString();
            lblvalor2.text = Random.Range(0, 11).ToString();
        }
        else
        {
            lblvalor1.text = Random.Range(0, 99).ToString();
            lblvalor2.text = Random.Range(0, 99).ToString();
        }

    }

    private void Update()
    {
        tiempo += Time.deltaTime;
        
        if (nivel == 1 && tiempo < 2f)
        {
            potion4.color = new Color(1, 1, 1, alpha);
            alpha -= 0.75f * tiempo;
        }
        else if ((nivel == 2 && tiempo < 2f))
        {
            potion3.color = new Color(1, 1, 1, alpha);
            alpha -= 0.75f * tiempo;
        }
        else if ((nivel == 3 && tiempo < 2f))
        {
            potion2.color = new Color(1, 1, 1, alpha);
            alpha -= 0.75f * tiempo;
        }

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
        Debug.Log("Next Level");
        resultado.text = "?";
        int operador = Random.Range(0, 3);
        lblvalor1.text = Random.Range(0, 99).ToString();
        lblvalor2.text = Random.Range(0, 99).ToString();

        if (gameStats.GetLevel()>=10) {
            operacion.text = "x";
            lblvalor1.text = Random.Range(0, 11).ToString();
            lblvalor2.text = Random.Range(0, 11).ToString();
        }
        else
        {
            if (int.Parse(lblvalor1.text) < int.Parse(lblvalor2.text))
            {
                operador = Random.Range(0, 2);
            }
            if (0 == operador)
            {
                operacion.text = "+";
            }
            else if (1 == operador)
            {
                operacion.text = "x";
                lblvalor1.text = Random.Range(0, 11).ToString();
                lblvalor2.text = Random.Range(0, 11).ToString();
            }
            else if (2 == operador)
            {
                operacion.text = "-";
            }
        }

        if (nivel == 3)
        {
            StartCoroutine("MostrarVida");
            gameStats.SetPlayerHealth(curacion);

        }
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

                    StartCoroutine("NextLevel");
                    tiempo = 0;
                    alpha = 1.0f;
                
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
