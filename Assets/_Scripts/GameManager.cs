using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameManager : Singleton<GameManager>
{
    public bool playing = true; 

    public Transform startPosition;
    public TextMeshProUGUI objectiveText; 
    public bool AllowInput { get { return !paused && playing; } }
    public bool paused;

    public GameObject player;
    public GameObject pause;

    public MainMenu menu;

    public float timeInRange = 0f;

    public CanvasGroup endTitleGroup;

    public TextMeshProUGUI tutorialText;

    public bool GameOver;

    public Tannoy[] tannoy;
    public Train trainPlatform1;
    public Train trainPlatform2;

    public GameObject manSighting1;
    public LightFlicker lightFlicker1;

    public AudioSource comotionSource;
    public AudioSource pidgeonSource;

    public GameObject runPastAnimator;

    public IEnumerator Start()
    {
        playing = false;
        yield return new WaitForSeconds(1f);
        ScreenFader.Instance.Fade(0, 2f);
        yield return new WaitForSeconds(2f);
        playing = true;

        manSighting1.gameObject.SetActive(false);
        runPastAnimator.gameObject.SetActive(false);

        StartCoroutine(GameRoutine());
    }

    public IEnumerator GameRoutine()
    {
        yield return new WaitForSeconds(15f);

        PlayAnnouncement(tannoy[0].universityClip);

        yield return new WaitForSeconds(5f);

        trainPlatform2.TrainStop();

        yield return new WaitForSeconds(15f);

        lightFlicker1.on = false;

        yield return new WaitForSeconds(1f);

        manSighting1.SetActive(true);

        trainPlatform2.TrainStart();

        yield return new WaitForSeconds(8f);

        lightFlicker1.on = true;

        yield return new WaitForSeconds(1f);

        lightFlicker1.flickering = true;

        yield return new WaitForSeconds(3.8f);

        lightFlicker1.source.PlayOneShot(lightFlicker1.flickerOut);

        yield return new WaitForSeconds(1.2f);

        lightFlicker1.flickering = false;
        lightFlicker1.on = false;

        yield return new WaitForSeconds(0.5f);

        manSighting1.SetActive(false);

        yield return new WaitForSeconds(2f);

        lightFlicker1.on = true;

        yield return new WaitForSeconds(1f);

        lightFlicker1.flickering = true;

        PlayAnnouncement(tannoy[0].delayClip);

        yield return new WaitForSeconds(40f);

        comotionSource.Play();

        yield return new WaitForSeconds(5f);

        lightFlicker1.flickering = false;

        yield return new WaitForSeconds(1f);

        lightFlicker1.on = false;

        PlayAnnouncement(tannoy[0].delayClip);

        yield return new WaitForSeconds(3f);

        lightFlicker1.on = true;
        manSighting1.SetActive(true);

        trainPlatform2.TrainPassNoStop();

        yield return new WaitForSeconds(8f);

        lightFlicker1.flickering = true;

        lightFlicker1.source.PlayOneShot(lightFlicker1.flickerOut);

        yield return new WaitForSeconds(1.2f);

        lightFlicker1.flickering = false;
        lightFlicker1.on = false;

        manSighting1.SetActive(false);

        yield return new WaitForSeconds(5f);

        lightFlicker1.on = true;

        yield return new WaitForSeconds(10f);

        pidgeonSource.Play();


        yield return new WaitForSeconds(15f);

        runPastAnimator.gameObject.SetActive(true);
    }

    public void PlayAnnouncement(AudioClip clip)
    {
        foreach (Tannoy t in tannoy)
            t.PlayAnnouncement(clip);
    }

    public void Pause()
    {
        if (playing)
        {
            if (!paused)
            {
                paused = true;
                Time.timeScale = 0f;
            }
            else
            {
                paused = false;
                Time.timeScale = 1f;
            }

            player.SetActive(!paused);
            pause.SetActive(paused);
        }
    }

}
