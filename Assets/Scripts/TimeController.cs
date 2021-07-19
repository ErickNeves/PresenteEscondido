using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 
====================================
Script responsavel por fazer todo o controle e tratamento do tempo
Também é o responsavel por atualizar o HUD de tempo.
====================================

*/

public class TimeController : MonoBehaviour
{

    [SerializeField]
    private float nowTime;
    [SerializeField]
    private float maxTime = 60f;

    [SerializeField]
    private int bonusTime = 5; //Ganhar + tempo
    [SerializeField]
    private int downTime = 5; //Perder tempo

    [SerializeField]
    private Image fillImg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        fillImg.fillAmount = (nowTime / maxTime);

        if (nowTime >= maxTime)
        {
            //CHAMAR TELA DE FALHA
            Debug.Log("MORREU!");
        }
    }

    IEnumerator TimeCount()
    {


        while(nowTime <= maxTime)
        {
            yield return new WaitForSeconds(1);
            nowTime++;
            
        }


    }

    public void StartTimer()
    {
        StartCoroutine("TimeCount");
    }

    public void StopTimer()
    {
        StopCoroutine("TimeCount");
    }

    //Reduzir o tempo de jogo caso erre
    public void WrongAnswer()
    {
        nowTime += downTime;
    }

    //Dar mais tempo ao jogador caso precise.
    public void MoreTime()
    {
        nowTime -= bonusTime;
    }

}
