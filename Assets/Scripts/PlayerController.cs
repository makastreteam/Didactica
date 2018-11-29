using System.Collections;
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

            CheckRotation();

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
            StartCoroutine(HitAnim());
        }
        else
        {
            enemyController.SetTurn(true);
            anim.SetBool("isWalking", false);
        }
    }

    IEnumerator HitAnim()
    {
        yield return new WaitForSeconds(1f);
        enemyController.SetTurn(true);
        anim.SetBool("isWalking", false);
    }

    //Comprobar si tenemos que girar
    void CheckRotation()
    {
        if (indexPosition <= 0)
        {
            indexPosition = 0;
        }
        else if (indexPosition >= Position.Length)
        {
            indexPosition = Position.Length - 1;
        }

        if (Position[indexPosition].transform.position.x > this.gameObject.transform.position.x)
        {
            if (this.gameObject.transform.rotation.y < 0)
            {
                StartCoroutine(Rotate(180));
            }
        }

        if (Position[indexPosition].transform.position.x < this.gameObject.transform.position.x)
        {
            if (this.gameObject.transform.rotation.y >= 0)
            {
                StartCoroutine(Rotate(-180));
            }
        }
    }

    //Rotacion
    IEnumerator Rotate(float grados)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + grados;
        float t = 0.0f;
        float duracion = 0.5f;

        while (t < duracion)
        {
            t += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duracion) % 360.0f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
            yield return null;
        }
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
