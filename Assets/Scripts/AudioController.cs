using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
====================================
Script responsavel por fazer todo o controle e tratamento do audio
Armazenar as referencias dos sons e os executar conforme o seu ID, que é definido pela 
sua posição no vetor!
====================================

*/


public class AudioController : MonoBehaviour
{
    

    [SerializeField]
    private AudioClip[] audios = new AudioClip[24];

    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayAudio(int id)
    {
        Debug.Log("ID PASSADO: " + id);
        audioSource.PlayOneShot(audios[id]);
    }
}
