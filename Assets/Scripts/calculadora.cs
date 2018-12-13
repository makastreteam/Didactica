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
    public GameObject barraVida;
    public GameObject botones;
    public GameObject pergamino;
    public GameObject formula;
    public Slider barraVidaSlider;

    private string textoimprimir;
    string valor1;
    string valor2;
    string operacionActual;
    double rta;
    int nivel = 0;
    int curacion;
    int curacionTotal=-1;
    float segundos;
    Stats gameStats;
    TransicionEscena _TransicionEscena;
    float tiempo;
    float alpha;
    private bool rellenar = false;
    private AudioSource burbu;

    private void Start()
    {
        gameStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        _TransicionEscena = GameObject.FindGameObjectWithTag("TransicionEscena").GetComponent<TransicionEscena>();
        curacion = gameStats.GetPlayerHealth();
        barraVida.SetActive(false);
        burbu = GetComponent<AudioSource>();

        if (gameStats.GetLevel() >= 10)
        {
            operacion.text = "x";
            lblvalor1.text = Random.Range(0, 11).ToString();
            lblvalor2.text = Random.Range(0, 11).ToString();
        }
        else
        {
            lblvalor1.text = Random.Range(0, 11 * (gameStats.GetLevel() + 1)).ToString();
            lblvalor2.text = Random.Range(0, 11 * (gameStats.GetLevel() + 1)).ToString();
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
        }else if (nivel == 4 && tiempo < 2f) {
            pergamino.SetActive(false);
            formula.SetActive(false);
            botones.SetActive(false);
            potion1.color = new Color(1, 1, 1, alpha);
            potion2.color = new Color(1, 1, 1, alpha);
            potion3.color = new Color(1, 1, 1, alpha);
            potion4.color = new Color(1, 1, 1, alpha);
            alpha -= 0.75f * tiempo;
            rellenar = true;
        }

        if(rellenar==true && tiempo>2f)
        {
            barraVida.SetActive(true);
            if (tiempo > segundos)
            {
                if (curacionTotal >= 0 && gameStats.GetPlayerHealth() < 100)
                {

                  gameStats.SetPlayerHealth(curacion++);
                    barraVidaSlider.value =gameStats.GetPlayerHealth();
                    curacionTotal--;
                }
                segundos+=0.05f;
            }
        }
    }

    public void BorrarC()
    {

        resultado.text = "?";
    }

    IEnumerator MostrarVida()
    {
            yield return new WaitForSeconds (5);
            Debug.Log("Funciona");
        _TransicionEscena.CambiarEscenaTransicion("Map");
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Next Level");
        resultado.text = "?";
        int operador = Random.Range(0, 3);
        lblvalor1.text = Random.Range(0, 11*(gameStats.GetLevel()+1)).ToString();
        lblvalor2.text = Random.Range(0, 11 * (gameStats.GetLevel() + 1)).ToString();

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
            nivel = 4;
            potion4.enabled = false;
            potion3.enabled = false;
            potion2.enabled = false;
            tiempo = 0;
            alpha = 1.0f;
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
                if (nivel == 0)
                {
                    curacionTotal = 0;
                }
                nivel += 1;
                curacionTotal += 25;
                Debug.Log("vida++");

                    StartCoroutine("NextLevel");
                    tiempo = 0;
                    alpha = 1.0f;
                burbu.Play();
                
            }
            else{
                Debug.Log("Trabaja");
                StartCoroutine("MostrarVida");
                if (nivel == 0)
                {
                    potion3.enabled = false ;
                    potion2.enabled = false;
                    potion1.enabled = false;
                    nivel = 4;
                    tiempo = 0;
                    alpha = 1.0f;
                }
                else if (nivel == 1)
                {
                    potion4.enabled = false;
                    potion2.enabled = false;
                    potion1.enabled = false;
                    nivel = 4;
                    tiempo = 0;
                    alpha = 1.0f;
                }
                else if (nivel == 2)
                {
                    potion4.enabled = false;
                    potion3.enabled = false;
                    potion1.enabled = false;
                    nivel = 4;
                    tiempo = 0;
                    alpha = 1.0f;
                }
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
