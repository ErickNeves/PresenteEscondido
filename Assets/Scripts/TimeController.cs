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

    private GameControler gameControl;

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

    private bool Lose = false;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = gameObject.GetComponent<GameControler>();
    }

    // Update is called once per frame
    void Update()
    {

        fillImg.fillAmount = (nowTime / maxTime);

        if (nowTime >= maxTime && !Lose)
        {
            Lose = true;
            gameControl.Lose();
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

    public float TakeTime()
    {
        return nowTime;
    }

    public float TakeMaxTime()
    {
        return maxTime;
    }

}
