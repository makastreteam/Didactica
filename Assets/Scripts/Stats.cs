using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    int level;
    int points;
    int playerHealth;

	void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        level = 0;
        points = 0;
        playerHealth = 100;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetPoints(int points)
    {
        this.points = points;
    }

    public int GetPoints()
    {
        return points;
    }

    public void SetPlayerHealth(int amount)
    {
        this.playerHealth = amount;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }
}
