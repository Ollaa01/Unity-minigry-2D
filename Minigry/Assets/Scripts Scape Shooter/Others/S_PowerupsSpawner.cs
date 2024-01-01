using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PowerupsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps = new GameObject[1];
    [SerializeField] [Range(0f, 1f)] private float probability = 0.5f;
    [SerializeField] private float timeToSpawn = 6f;

    private float timer = 0f;
    // Start is called before the first frame update
    void Update()
    {
        //Debug.Log("tu1");
        int n = Random.Range(0, powerUps.Length);
        //Debug.Log("powe " + n);
        timer += Time.deltaTime;
        //Debug.Log(timer);
        if (timer >= timeToSpawn)
        {
            ///Debug.Log("tu2");
            if (powerUps[n] != null)
            {
                //Debug.Log("tu3");
                if (Random.value <= probability)
                {
                    //Debug.Log("tu4");
                    Instantiate(powerUps[n].gameObject, transform);
                }
            }
            timer = 0f;
            //Debug.Log("tu5");
        }
    }

    // Update is called once per frame
    void Start()
    {
        
    }
}
