using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovePoints: MonoBehaviour{

	public GameObject[] Points;
    LevelManager LM;
    int index = 0;
    float minDist = 0.01f;

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

    IEnumerator RotateTowards(Transform target)
    {
        Vector3 direction = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y,  transform.position.z);
        direction.z = 0;
        Quaternion toRotation = Quaternion.FromToRotation(transform.rotation.eulerAngles, direction);
        float timer = 0.4f;
        while ((timer -= Time.deltaTime) > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), speed * Time.deltaTime);

            yield return null;
        }
    }

    public IEnumerator MoveToNextLevel()
    {
        bool ended = false;
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


            yield return null;
        }

        Debug.Log("FINAL -> Start " + start + " End" + end);
    }

}
