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

    PlayerController playerController;
    EnemyController enemyController;

    Stats gameStats;

    public float TimeLeft;

    void Start()
    {
        gameStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();

        playerController = Player.GetComponent<PlayerController>();
        enemyController = Enemy.GetComponent<EnemyController>();
    }

    void Update()
    {
        updateStats();

        if(gameStats.GetPlayerHealth() <= 0)
        {
            SceneManager.LoadScene("Score", LoadSceneMode.Single);
        }

        if(enemyController.GetHealth() <= 0)
        {
            StartCoroutine(EnemyDeathAnim());
        }

        if(playerController.GetTurn() == false)
        {
            NumberButtons.SetActive(false);
            TurnText.text = "ENEMIGO";
        }
        else
        {
            NumberButtons.SetActive(true);
            TurnText.text = "TU TURNO";
        }
    }

    //Actualizar puntiacion, movimientos y temporizador
    void updateStats()
    {
        TimeLeft -= Time.deltaTime;
        TimerText.text = Mathf.Round(TimeLeft).ToString();

        LevelText.text = "Nivel: " + gameStats.GetLevel().ToString();
        PointsText.text = "Puntos: " + gameStats.GetPoints().ToString();
        AttackText.text = "Proximo ataque: " + enemyController.GetAttackIndex().ToString();

        PlayerHealthBar.value = gameStats.GetPlayerHealth();
        EnemyHealthBar.value = enemyController.GetHealth();
    }

    IEnumerator EnemyDeathAnim()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("LevelTransition", LoadSceneMode.Single);
    }
}
