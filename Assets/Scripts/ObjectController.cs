using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
====================================
Script responsavel por fazer todo o controle e tratamento dos items
Também é o responsavel por sua queda, tal como seu retorno ao topo.
====================================

*/


public class ObjectController : MonoBehaviour
{

    #region Declaracoes

    [SerializeField]
    private GameControler gameControler;

    private Rigidbody2D myBody;
    private Transform myTransform;

    [SerializeField]
    private Vector2 downForce = new Vector2(0, -100); //Definir a velocidade da queda do Objeto
    private Vector3 initialPosition; //Armazenar a pos inicial do objeto com base ao colocado no cenário

    [SerializeField]
    private int myId; //ID desse objeto


    [SerializeField]
    private Sprite mySpriteHover; //Imagem Hover do Obj, definido pelo Inspector
    private Sprite mySprite; //Imagem Padrão do Obj, pego ao iniciar.

    //private SpriteRenderer myRender;  SEM USO NO MOMENTO

    #endregion

    private void Awake()
    {
        //Pega a pos inicial do eixo X, define um ponto de respawn padrão em Y e armazena.
        initialPosition = new Vector3(gameObject.GetComponent<Transform>().position.x, 5.5f,0f);
    }

    void Start()
    {
        myBody = gameObject.GetComponent<Rigidbody2D>();
        myTransform = gameObject.GetComponent<Transform>();
        mySprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        GoDown();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int GetMyId()
    {
        return myId;
    }


    #region Movimentacao

    private void GoDown()
    {
        if(myId != -1)
            myBody.AddForce(downForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            myTransform.SetPositionAndRotation(initialPosition, new Quaternion(0, 0, 0, 0));
        }
    }

    #endregion  


    #region InteracaoMouse

    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = mySpriteHover;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = mySprite;
    }

    private void OnMouseUpAsButton()
    {
        gameControler.CheckAnswer(gameObject);
    }

    #endregion


}
