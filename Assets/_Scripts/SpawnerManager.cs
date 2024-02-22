using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] platform;
    //public GameObject platform2;
    //public GameObject platform3;
    //public GameObject platform4;

    public float timeToDestroy;
    public PlayerMouvements playerMouvements;
    bool decreaseTime;



    void Start()
    {
        decreaseTime = playerMouvements.increaseSpeed;
        StartCoroutine(PlatformControl());
    }

    private IEnumerator PlatformControl()
    {
        yield return new WaitForSeconds(0.005f);
        if (decreaseTime == true)
        {
            timeToDestroy = timeToDestroy - Time.deltaTime / 6;
            StartCoroutine(PlatformControl());
        }
        if (timeToDestroy <= 5f) { 
            decreaseTime = false;
        }
        //Debug.Log(timeToDestroy);
        //Debug.Log("decrease: " + decreaseTime);
    }
}
