using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinalCredits : MonoBehaviour
{

    [SerializeField] private float speedSuperFast;
    [SerializeField] private float speed;
    [SerializeField] private float durationTimeTxtOnScreen;

    [SerializeField] private GameObject obj_txt;
    [SerializeField] private GameObject lastMessage;


    private float posY;
    private bool canDone;

    [SerializeField] private AudioClip aud_finalCredits;

    void Start()
    {
        posY = obj_txt.transform.position.y;
        lastMessage.SetActive(false);

        AudioManager.instance.PlaySong(aud_finalCredits, true);
    }


    void Update()
    {
        if (Input.GetButton("Jump") || Input.GetKey(KeyCode.Mouse0))
        {
            obj_txt.transform.position = new Vector3(0f, obj_txt.transform.position.y + speedSuperFast * Time.deltaTime, 0f);
        }
        else
        {
            obj_txt.transform.position = new Vector3(0f, obj_txt.transform.position.y + speed * Time.deltaTime, 0f);
        }

        if (obj_txt.transform.position.y >= (posY * -1))
        {
            if (!canDone)
            {
                canDone = true;
                StartCoroutine(BackToMenu());
            }
        }
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(0.2f);
        lastMessage.SetActive(true);
        yield return new WaitForSeconds(durationTimeTxtOnScreen);
        /*SceneController.instance.NextLevel(0);*/
    }

}
