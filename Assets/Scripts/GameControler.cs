using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 
====================================
Script responsavel por fazer todo o controle do jogo!
====================================

*/


public class GameControler : MonoBehaviour
{
    [SerializeField]
    private int[] questions = new int[8]; //Vetor para armazenar o ID das respostas corretas.

    [SerializeField]
    private GameObject pacoca;

    private Animator pacocaAnim;

    [SerializeField]
    private GameObject tutorial;

    public int QuantPlay = 0;

    [SerializeField]
    private GameObject finalScreen;

    [SerializeField]
    private int currentStage = -1; //Definir o estágio atual. Como o primeiro estágio é o ID 0, inicializei com -1 para evitar que pule para o 1 no começo.

    private TimeController timer; //Referencia ao script do tempo vinculado ao OBJ do GameController

    [SerializeField]
    private GameObject adelaide; //Referencia ao Objeto responsável pelas animações da Adelaide

    [SerializeField]
    private GameObject objetos; //Referencia ao Objeto pai onde todos os items do jogo estão.

    [SerializeField]
    private GameObject fundoPreto;

    private void Awake()
    {
        tutorial.SetActive(true);
        ShuffleQuestions();
        timer = gameObject.GetComponent<TimeController>();
    }

    private void Start()
    {
        //pacocaAnim = pacoca.GetComponent<Animator>();
        //gameObject.GetComponent<AudioController>().PlayAudio(questions[currentStage]);

    }

    public void CloseTutoAndStart() //FECHA TUTORIAL E INICIA GAME
    {
        tutorial.SetActive(false);
        objetos.SetActive(true);
        timer.StartTimer();
        StartTurn();
    }

    private void StartTurn() //INICIA TURNO
    {
        currentStage++;
        gameObject.GetComponent<AudioController>().PlayAudio(questions[currentStage]);
        adelaide.GetComponent<Animator>().SetTrigger("Comecou");
    }

    private void CorrectAnswer(GameObject clickedObj)
    {

        clickedObj.GetComponent<Animator>().enabled = true;
        clickedObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        clickedObj.GetComponent<Animator>().SetTrigger("Exit");        
        StartCoroutine("DestroyItem", clickedObj);
        
        if (currentStage != 7)
        {
            adelaide.GetComponent<Animator>().SetTrigger("Achou");
            StartTurn();
        }
        else
        {

            StartCoroutine("EndGame");
        }

    }

    IEnumerator EndGame()
    {
        timer.StopTimer();
        objetos.SetActive(false);
        fundoPreto.SetActive(false);
        adelaide.GetComponent<Animator>().SetTrigger("Ganhou");
        pacoca.SetActive(true);

        yield return new WaitForSeconds(3f);

        finalScreen.SetActive(true);
    }

    IEnumerator WrongAnswer() //RESPOSTA INCORRETA
    {
        
        adelaide.GetComponent<Animator>().SetTrigger("NaoAchou");
        timer.WrongAnswer();

        yield return new WaitForSeconds(1.5f);

    }


    public void CheckAnswer(GameObject clickedObj)
    {

        int id = clickedObj.GetComponent<ObjectController>().GetMyId();

        if (id != -1)
        {

            QuantPlay++;
            if(id == questions[currentStage])
            {
                CorrectAnswer(clickedObj);
            }
            else
            {
                StartCoroutine(WrongAnswer());
            }

        }
        else
        {
            gameObject.GetComponent<AudioController>().PlayAudio(questions[currentStage]);
        }

    }

    IEnumerator DestroyItem(GameObject clickedObj)
    {
        yield return new WaitForSeconds(1);
        Destroy(clickedObj);
    }

    private void ShuffleQuestions()     //EMBARALHAR IDS
    {
        int jatem = 0;

        for (int i = 0; i < questions.Length; i++)
        {
            int randomizeArray = Random.Range(0, 23);

            if (i == 0)
            {
                questions[i] = randomizeArray;
            }

            for(int j=0; j < i; j++)
            {
                if(questions[j] == randomizeArray)
                {
                    jatem++;
                }
 
            }

            if(jatem != 0)
            {
                i--;
            }
            else
            {
                questions[i] = randomizeArray;
            }

            jatem = 0;
        }
    }


    public void Lose()
    {
        timer.StopTimer();
        objetos.SetActive(false);
        fundoPreto.SetActive(false);
        adelaide.GetComponent<Animator>().SetTrigger("NaoAchou");
        finalScreen.SetActive(true);
    }

}
