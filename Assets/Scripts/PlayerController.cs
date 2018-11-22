﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public GameObject[] Position;
    public GameObject Enemy;
    public GameObject Model;

    EnemyController enemyController;

    int indexPosition;

    bool turn;
    int damage;

    Animator anim;

    void Awake()
    {
        this.transform.position = new Vector3(Position[0].transform.position.x, this.gameObject.transform.position.y, 0);
        indexPosition = 0;
        turn = true;
    }

    void Start ()
    {
        damage = 10;
        anim = Model.GetComponent<Animator>();
        enemyController = Enemy.GetComponent<EnemyController>();
    }

    public void MovePosition(int num)
    {
        if(turn == true)
        {
            turn = false;
            indexPosition += num;

            if (indexPosition <= 0)
            {
                indexPosition = 0;
                StartCoroutine(Move(Position[0].transform.position.x));
            }
            else if (indexPosition >= Position.Length)
            {
                indexPosition = Position.Length - 1;
                StartCoroutine(Move(Position[Position.Length - 1].transform.position.x));
            }
            else
            {
                StartCoroutine(Move(Position[indexPosition].transform.position.x));
            }
        } 
    }

    IEnumerator Move(float target)
    {
        anim.SetBool("isWalking", true);

        float start = this.transform.position.x;
        float t = 0.0f;
        float duration = 2f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float xPosition = Mathf.Lerp(start, target, Mathf.SmoothStep(0.0f, 1.0f, t / duration));
            this.transform.position = new Vector3(xPosition, this.gameObject.transform.position.y, 0);
            yield return null;
        }

        if (enemyController.GetIndexPosition() == indexPosition)
        {
            enemyController.TakeDamage(damage);
        }

        enemyController.SetTurn(true);

        anim.SetBool("isWalking", false);
    }

    public int GetIndexPosition()
    {
        return indexPosition;
    }

    public void SetTurn(bool turn)
    {
        this.turn = turn;
    }

    public bool GetTurn()
    {
        return turn;
    }
}
