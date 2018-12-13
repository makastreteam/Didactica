using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovePoints: MonoBehaviour{

	public GameObject[] Points;
    LevelManager LM;
    int index = 0;
    float minDist = 0.01f;
    bool walking = false;
    [Range(0.5f, 10)]
    public float speed = 1;
    Stats gameStats;

    bool start = true;
    GameObject levelManager;
    TransicionEscena _TransicionEscena;
    string[] escenasNivel = { "Level01", "Level02", "Level03", "Level04", "Level05", "Level06", "Level07",
                              "Level08", "Level09", "Level10", "Level11", "Level12"};

    public Sprite[] imgConsejos;
    public Image derecha;

    private void Start()
    {
        _TransicionEscena = GameObject.FindGameObjectWithTag("TransicionEscena").GetComponent<TransicionEscena>();
        LM = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        gameStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();

        if (!LM.levelsLoaded)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i].name.Contains("Level"))
                {
                    LM.levels.Add(i);
                }
            }
        }
        LM.levelsLoaded = true;

        this.transform.position = Points[LM.GetCurrentLevel()].transform.position;
        //StartCoroutine(MoveToNextLevel());
        Debug.Log("asdasd " + LM.GetCLevel());
        derecha.sprite = imgConsejos[LM.GetCLevel()];

    }

    public void Move(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position,speed * Time.deltaTime);
    }

    public void CallMoveToNextLevel()
    {
        if (!walking)
        {
            //LM.NextLevel();

            if (LM.GetCurrentLevel() != LM.GetNextLevel())
            {
                StartCoroutine(MoveToNextLevel());
            }
            else
                Debug.Log("No has superado el nivel");
        }

    }

    public IEnumerator MoveToNextLevel()
    {
        bool ended = false;
        walking = true;
        int start = LM.GetCurrentLevel(), end = LM.GetNextLevel();
        //Debug.Log("Start " + start + " End" + end);


        transform.position = Points[start].transform.position;
        while (!ended)
        {
            float dist = Vector3.Distance(transform.position, Points[start].transform.position);
            if (dist > minDist)
            {
                Move(Points[start].transform);
            }
            else
            {
                if (start == end) ended = true;
                else start++;
            }

            LM.EqualizeLevels();
            yield return null;
        }
        walking = false;
        //Debug.Log("FINAL -> Start " + start + " End" + end);

        cargarNivel();
    }

    void cargarNivel()
    {
        StartCoroutine(Transicion());
    }

    IEnumerator Transicion()
    {
        yield return new WaitForSeconds(1);
        //_TransicionEscena.CambiarEscenaTransicion("Game");
        _TransicionEscena.CambiarEscenaTransicion(escenasNivel[LM.GetCLevel()-1]);
        gameStats.Curado = false;
    }
}
