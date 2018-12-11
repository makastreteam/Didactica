using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HouseController : MonoBehaviour {

    public Text TimerText;
    public Text LevelText;
    public Text PointsText;
    public Text AttackText;
    public Text TurnText;
    public GameObject NumberButtons;
    public Slider PlayerHealthBar;
    public Slider EnemyHealthBar;

    public GameObject Player;
    public GameObject Enemy;

    public Sprite[] enemyBars = new Sprite[13];
    public GameObject enemyBar;

    PlayerController playerController;
    EnemyController enemyController;

    Stats gameStats;
    TransicionEscena _TransicionEscena;

    public float TimeLeft;
    bool cambioEscena;

    void Start()
    {
        cambioEscena = false;
        _TransicionEscena = GameObject.FindGameObjectWithTag("TransicionEscena").GetComponent<TransicionEscena>();

        gameStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        enemyBar.GetComponent<Image>().sprite = enemyBars[gameStats.GetLevel()];

        playerController = Player.GetComponent<PlayerController>();
        enemyController = Enemy.GetComponent<EnemyController>();
    }

    void Update()
    {
        updateStats();

        if(gameStats.GetPlayerHealth() <= 0)
        {
            //_TransicionEscena.CambiarEscenaTransicion("Map");
            SceneManager.LoadScene("Score", LoadSceneMode.Single);
        }

        if(enemyController.GetHealth() <= 0 && cambioEscena == false)
        {
            cambioEscena = true;
            StartCoroutine(EnemyDeathAnim());
        }

        if(playerController.GetTurn() == false)
        {
            NumberButtons.SetActive(false);
            //TurnText.text = "ENEMIGO";
        }
        else
        {
            NumberButtons.SetActive(true);
            //TurnText.text = "TU TURNO";
        }
    }

    //Actualizar puntiacion, movimientos y temporizador
    void updateStats()
    {
        TimeLeft -= Time.deltaTime;
        TimerText.text = Mathf.Round(TimeLeft).ToString();
        /*
        LevelText.text = "Nivel: " + gameStats.GetLevel().ToString();
        PointsText.text = "Puntos: " + gameStats.GetPoints().ToString();
        AttackText.text = "Proximo ataque: " + enemyController.GetAttackIndex().ToString();
        */
        PlayerHealthBar.value = gameStats.GetPlayerHealth();
        EnemyHealthBar.value = enemyController.GetHealth();
    }

    IEnumerator EnemyDeathAnim()
    {
        yield return new WaitForSeconds(3f);
        LevelManager.GetLevelManager.NextLevel();
        gameStats.SetLevel(gameStats.GetLevel() + 1);
        _TransicionEscena.CambiarEscenaTransicion("Map");
    }
}
