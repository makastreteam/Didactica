using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovePoints: MonoBehaviour{

	public GameObject[] Points;
    LevelManager LM;
    int index = 0;
    float minDist = 0.01f;
    bool walking = false;
    [Range(0.5f, 10)]
    public float speed = 1;
    
    bool start = true;

    private void Start()
    {
        LM = LevelManager.GetLevelManager;
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
        StartCoroutine(MoveToNextLevel());
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
        Debug.Log("Start " + start + " End" + end);


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
        Debug.Log("FINAL -> Start " + start + " End" + end);
    }

}
