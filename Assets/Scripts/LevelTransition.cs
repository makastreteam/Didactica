using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {

    public Text LevelText;
    public Text PointsText;

    Stats gameStats;

    void Start()
    {
        gameStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();

        gameStats.SetPoints(gameStats.GetPoints() + 100);

        LevelText.text = gameStats.GetLevel().ToString();
        PointsText.text = gameStats.GetPoints().ToString();

        gameStats.SetLevel(gameStats.GetLevel() + 1);

        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
