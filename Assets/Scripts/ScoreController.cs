using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text LevelText;
    public Text PointsText;

    Stats gameStats;

    void Start()
    {
        gameStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();

        LevelText.text = "Nivel: " + gameStats.GetLevel().ToString();
        PointsText.text = "Puntos: " + gameStats.GetPoints().ToString();

        gameStats.SetLevel(1);
        gameStats.SetPoints(0);
        gameStats.SetPlayerHealth(100);
    }
}
