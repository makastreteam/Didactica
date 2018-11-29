using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public GameObject[] Position;
    public GameObject Player;
    public GameObject AttackImage;
    public GameObject Model;

    PlayerController playerController;
    Stats gameStats;
    int indexPosition;
    int target;

    bool turn;
    int health;
    int damage;
    int takeDamage;

    Animator anim;

    void Awake()
    {
        this.transform.position = new Vector3(Position[Position.Length - 1].transform.position.x, this.gameObject.transform.position.y, 0);
        indexPosition = Position.Length - 1;
        target = 1;
        turn = false;
    }

	void Start ()
    {
        gameStats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        anim = Model.GetComponent<Animator>();

        health = 100;
        damage = 34;
        takeDamage = 5;
        playerController = Player.GetComponent<PlayerController>();
    }
	
	void Update ()
    {
        if (turn == true && health > 0)
        {
            turn = false;

            MovePosition();
        }

        if(health <= 0)
        {
            anim.SetTrigger("death");
        }
	}

    //Se mueve a la nueva posicion. Recorre el vector de lado a lado
    void MovePosition()
    {
        //Calcula la nueva posicion y que no coincida con la del jugador
        do
        {
            NewPosition();
        } while (playerController.GetIndexPosition() == indexPosition || indexPosition < 0 || indexPosition > Position.Length - 1);

        //Comprueba si tiene que rotar
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

    //Calcular nueva posicion
    void NewPosition()
    {
        if (indexPosition >= target)
        {
            if (Random.Range(0, 100) <= 80 || indexPosition == (Position.Length - 1))
            {
                indexPosition -= Random.Range(1, 5);
            }
            else
            {
                indexPosition += Random.Range(1, 5);
            }

            if (indexPosition <= target)
            {
                target = Position.Length - 3;
            }
        }
        else
        {
            if (Random.Range(0, 100) <= 80 || indexPosition == 0)
            {
                indexPosition += Random.Range(1, 5);
            }
            else
            {
                indexPosition -= Random.Range(1, 5);
            }

            if (indexPosition >= target)
            {
                target = 3;
            }
        }
    }

    void attack()
    {
        if(indexPosition - playerController.GetIndexPosition() <= 2 && indexPosition - playerController.GetIndexPosition() >= -2)
        {
            StartCoroutine(Attack());      
        }
        else
        {
            playerController.SetTurn(true);
        }
    }

    //Ataque Dragon
    IEnumerator Attack()
    {
        float t = 0.0f;
        float duration = 2f;

        while (t < duration)
        {
            t += Time.deltaTime;
            AttackImage.SetActive(true);
            yield return null;
        }

        AttackImage.SetActive(false);
        gameStats.SetPlayerHealth(gameStats.GetPlayerHealth() - damage);
        playerController.SetTurn(true);

    }

    //Movimiento suave
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

        if(takeDamage <= 1)
        {
            attack();
        }
        else
        {
            playerController.SetTurn(true);
        }

        takeDamage--;
        if (takeDamage < 1)
        {
            takeDamage = 5;
        }

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
            if (this.gameObject.transform.rotation.y >= 0)
            {
                StartCoroutine(Rotate(-180));
            }
        }

        if (Position[indexPosition].transform.position.x < this.gameObject.transform.position.x)
        {
            if (this.gameObject.transform.rotation.y < 0)
            {
                StartCoroutine(Rotate(180));
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

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int amount)
    {
        anim.SetTrigger("getHit");
        this.health -= amount;

        if(health <= 0)
        {
            anim.SetTrigger("death");
        }
    }

    public int GetAttackIndex()
    {
        return takeDamage;
    }
}
