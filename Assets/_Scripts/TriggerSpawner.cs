using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
    private SpawnerManager spawnerManager;
    [SerializeField]
    Transform End;

    private GameObject instantiatedPlatform;
    private GameObject previousPlatform;

    public float timeToDestroy;


    private void Start()
    {
        spawnerManager = GameObject.Find("SpawnerManager").GetComponent<SpawnerManager>();
        timeToDestroy = spawnerManager.timeToDestroy;
        if (gameObject.transform.parent.name == "Floor" ) {
            Destroy(gameObject.transform.parent.gameObject, timeToDestroy);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int floor = Random.Range(0, spawnerManager.platform.Length);
            //Debug.Log(floor);
            //Debug.Log(spawnerManager.platform[floor].name);
            instantiatedPlatform = Instantiate(spawnerManager.platform[floor], End.position, Quaternion.identity);
            previousPlatform = instantiatedPlatform;
            //Debug.Log("platform " + instantiatedPlatform);
            Destroy(instantiatedPlatform,timeToDestroy);
        }
    }
}
