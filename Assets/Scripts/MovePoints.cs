using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// State in which the enemy goes through its patrol points.
/// When transitioning back to this state, the enemy continues its patrol route where he left of.
/// </summary>
public class MovePoints: MonoBehaviour{

	public GameObject[] Points;

    int index = 0;
    float minDist = 0.01f;
    float speed = 1;
    bool start = true;

    public void Move(Transform target)
    {
        //transform.LookAt(new Vector3(Points[index].transform.position.x, transform.position.y, Points[index].transform.position.z));
        transform.position =Vector3.MoveTowards(transform.position, target.position,speed * Time.deltaTime);
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

    void Update ()
    {
        if(start == true)
        {
            float dist = Vector3.Distance(transform.position, Points[index].transform.position);

            if (dist > minDist)
            {
                Move(Points[index].transform);
                //StartCoroutine(RotateTowards(Points[index].transform));
            }
            else
            {
                if (index + 1 == Points.Length)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
        }
	}

	public void OnStateEnter ()
    {
        start = true;
	}

	public void OnStateLeave ()
    {
        index = 1;
        start = false;
    }

}
