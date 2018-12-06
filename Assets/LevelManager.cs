using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    int currentLevel = 0;
    int nextLevel = 1;
    public bool levelsLoaded = false;
    public List<int> levels;
    public static LevelManager GetLevelManager;

    private void Awake()
    {
        GetLevelManager = this.gameObject.GetComponent<LevelManager>();
    }

    void NextLevel()
    {
        currentLevel = nextLevel;
        nextLevel = currentLevel + 1;
    }

    void EndGame()
    {
        currentLevel = 0;
        nextLevel = 1;
    }

    public int GetCurrentLevel()
    {
        return levels[currentLevel];
    }

    public int GetNextLevel()
    {
        return levels[nextLevel];
    }
}
