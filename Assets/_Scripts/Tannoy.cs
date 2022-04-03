using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tannoy : MonoBehaviour
{
    public AudioSource source;

    public AudioClip preAnnouncementClip;

    public AudioClip notStopping;
    public AudioClip servicesResuming;
    public AudioClip delayClip;
    public AudioClip sunderlandClip;
    public AudioClip universityClip;


    public void PlayAnnouncement(AudioClip clip)
    {
        StartCoroutine(PlayClipRoutine(clip));
    }

    public IEnumerator PlayClipRoutine(AudioClip clip)
    {
        source.PlayOneShot(preAnnouncementClip);
        yield return new WaitForSeconds(0.5f);
        source.PlayOneShot(clip);
    }
}
