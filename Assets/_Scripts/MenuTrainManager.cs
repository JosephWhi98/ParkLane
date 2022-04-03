using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrainManager : MonoBehaviour
{
    public Train trainPlatform1;
    public Train trainPlatform2;

    float lastTrainTime;

    public void Start()
    {
        lastTrainTime = Time.time - 20f;
    }

    public void Update()
    {
        if (Time.time > lastTrainTime + 25f) 
        {
            int i = Random.Range(0, 100);

            if (i > 50)
                trainPlatform1.TrainPassNoStop();
            else
                trainPlatform2.TrainPassNoStop();

            lastTrainTime = Time.time; 
        }
    }
}
