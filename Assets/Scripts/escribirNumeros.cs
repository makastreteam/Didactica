using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class escribirNumeros : MonoBehaviour {

    private string textoimprimir;
    public Text resultado;
    private int contador=0;

    public void colocar0()
    {
        interrogacion();
        if (contador != 2)
        {
            textoimprimir = "0";
            resultado.text = resultado.text + textoimprimir;
            contador++;
        }
    }
    public void colocar1()
    {
        interrogacion();
        if (contador != 2)
        {
            textoimprimir = "1";
            resultado.text = resultado.text + textoimprimir;
            contador++;
        }
    }
    public void colocar2()
    {
        interrogacion();
        if (contador != 2)
        {
            textoimprimir = "2";
            resultado.text = resultado.text + textoimprimir;
            contador++;
        }
    }
    public void colocar3()
    {
        interrogacion();
        if (contador != 2)
        {
            textoimprimir = "3";
            resultado.text = resultado.text + textoimprimir;
            contador++;
        }
    }
    public void colocar4()
    {
        interrogacion();
        if (contador != 2)
        {
            textoimprimir = "4";
            resultado.text = resultado.text + textoimprimir;
            contador++;
        }
    }
    public void colocar5()
    {
        interrogacion();
        if (contador != 2)
        {
            textoimprimir = "5";
            resultado.text = resultado.text + textoimprimir;
            contador++;
        }
    }
    public void colocar6()
    {
        interrogacion();
        if (contador != 2)
        {
            textoimprimir = "6";
            resultado.text = resultado.text + textoimprimir;
            contador++;
        }
    }
    public void colocar7()
    {
        interrogacion();
        if (contador != 2)
        {
            textoimprimir = "7";
            resultado.text = resultado.text + textoimprimir;
            contador++;
        }
    }
    public void colocar8()
    {
        interrogacion();
        if (contador != 2)
        {
            textoimprimir = "8";
            resultado.text = resultado.text + textoimprimir;
            contador++;
        }
    }
    public void colocar9()
    {
        interrogacion();
        if (contador !=2 ) {
            textoimprimir = "9";
            resultado.text = resultado.text + textoimprimir;
            contador++;
        }
    }


    void interrogacion()
    {
        if (resultado.text == "?")
        {
            contador = 0;
            resultado.text = "";
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
