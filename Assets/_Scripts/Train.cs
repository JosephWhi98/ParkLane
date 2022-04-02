using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public AudioSource source;
    public AudioClip noStopClip;
    public AudioClip StopClip;
    public AudioClip StartClip;


    public AudioSource idleLoop; 

    public Animator anim; 

    public void TrainPassNoStop()
    {
        StartCoroutine(TrainPassNoStopRoutine());
    }


    public IEnumerator TrainPassNoStopRoutine()
    {
        anim.Rebind();
        source.PlayOneShot(noStopClip);
        yield return new WaitForSeconds(6f);
        anim.SetTrigger("DontStop");
    }

    public void TrainStop()
    {
        StartCoroutine(TrainStopRoutine());
    }


    public IEnumerator TrainStopRoutine()
    {
        anim.Rebind();
        idleLoop.Play();
        source.PlayOneShot(StopClip);
        yield return null;
        anim.SetTrigger("Stop");
    }


    public void TrainStart()
    {
        StartCoroutine(TrainStartRoutine());
    }


    public IEnumerator TrainStartRoutine()
    {
        anim.SetTrigger("Go");

        yield return new WaitForSeconds(2f);

        source.PlayOneShot(StartClip);


        yield return new WaitForSeconds(4f);

        idleLoop.Stop();
    }
}
