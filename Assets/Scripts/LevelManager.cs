using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    int currentLevel = 0;
    int nextLevel = 1;
    public bool levelsLoaded = false;
    public List<int> levels;
    public static LevelManager GetLevelManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        GetLevelManager = this.gameObject.GetComponent<LevelManager>();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetActiveScene().buildIndex)
        {
            //Nothing
        }
    }

    public void NextLevel()
    {
        if(currentLevel < levels.Count-1)
        {
            nextLevel = currentLevel + 1;
        }
        Debug.Log("Updating levels --> CURRENT: " + currentLevel + " NEXT: " + nextLevel);
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

    public void SetCurrentLevel(int updated)
    {
        currentLevel = updated;
    }

    public void EqualizeLevels()
    {
        currentLevel = nextLevel;
    }

    public int GetNextLevel()
    {
        return levels[nextLevel];
    }

    public void SetNextLevel(int updated)
    {
        nextLevel = updated;
    }
}
