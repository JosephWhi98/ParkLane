using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

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
    public Animator killPlayerAnimator;

    public AudioSource finaleStingSource;
    public AudioSource stingSource;

    public bool gotTicket;
    public TicketMachine ticketMachine;

    public ScreenDisplay[] screens;
    public Tannoy[] tannoy;
    public Train trainPlatform1;
    public Train trainPlatform2;

    public GameObject manSighting1;
    public Animator manSighting1Animator; 
    public LightFlicker lightFlicker1;

    public AudioSource comotionSource;

    public AudioSource comotion2Source;
    public AudioSource pidgeonSource;

    public GameObject runPastAnimator;

    public Collider wetFloorSignSpookTrigger;
    public Collider wetFloorSignEndTrigger;
    public AudioSource wetFloorSignSource;
    public GameObject wetFloorSign1;
    public GameObject wetFloorSign2;

    public GameObject ticketMachineHand;

    public Collider ticketHandPrintTrigger;

    public IEnumerator Start()
    {
        manSighting1.gameObject.SetActive(false);
        runPastAnimator.gameObject.SetActive(false);

        SetText("3m");

        playing = false;
        yield return new WaitForSeconds(1f);
        ScreenFader.Instance.Fade(0, 2f);
        yield return new WaitForSeconds(2f);
        playing = true;

        StartCoroutine(GameRoutine());
    }

    public IEnumerator GameRoutine()
    {
        float getTicketTime = Time.time + 15f; 

        while (!gotTicket)
        {
            yield return null;

            if (Time.time > getTicketTime)
            {
                getTicketTime = Time.time + 30f;
                SubtitlesManager.Instance.ShowSubtitle("I should buy a ticket.", 4f);
            }
        }

        yield return new WaitForSeconds(15f);

        PlayAnnouncement(tannoy[0].universityClip);

        yield return new WaitForSeconds(5f);

        trainPlatform2.TrainStop();

        SetText("2m");

        yield return new WaitForSeconds(15f);

        lightFlicker1.on = false;

        yield return new WaitForSeconds(1f);

        manSighting1.SetActive(true);

        trainPlatform2.TrainStart();

        yield return new WaitForSeconds(8f);

        lightFlicker1.on = true;

        while (!Helpers.LineOfSight(Camera.main.transform, manSighting1.transform, 30f))
            yield return null;

        yield return new WaitForSeconds(1f);

        lightFlicker1.flickering = true;

        yield return new WaitForSeconds(3.8f);

        lightFlicker1.source.PlayOneShot(lightFlicker1.flickerOut);

        yield return new WaitForSeconds(1.2f);

        lightFlicker1.flickering = false;
        lightFlicker1.on = false;
        stingSource.Play();

        yield return new WaitForSeconds(0.5f);

        manSighting1.SetActive(false);

        yield return new WaitForSeconds(2f);

        lightFlicker1.on = true;

        yield return new WaitForSeconds(1f);

        lightFlicker1.flickering = true;

        PlayAnnouncement(tannoy[0].delayClip);

        SetText("DELAYED");

        yield return new WaitForSeconds(45f);

        comotionSource.Play();

        yield return new WaitForSeconds(5f);

        lightFlicker1.flickering = false;

        yield return new WaitForSeconds(1f);

        lightFlicker1.on = false;

        PlayAnnouncement(tannoy[0].notStopping);

        yield return new WaitForSeconds(3f);

        lightFlicker1.on = true;
        manSighting1.SetActive(true);
        manSighting1Animator.SetTrigger("Sighting 2");

        while (!Helpers.LineOfSight(Camera.main.transform, manSighting1.transform, 30f))
            yield return null;

        stingSource.Play();

        trainPlatform2.TrainPassNoStop();

        yield return new WaitForSeconds(5f);

        manSighting1Animator.SetTrigger("Sighting 2 Seen");

        yield return new WaitForSeconds(3f);

        lightFlicker1.flickering = true;

        lightFlicker1.source.PlayOneShot(lightFlicker1.flickerOut);

        yield return new WaitForSeconds(1.2f);

        lightFlicker1.flickering = false;
        lightFlicker1.on = false;

        manSighting1.SetActive(false);

        yield return new WaitForSeconds(5f);

        lightFlicker1.on = true;

        yield return new WaitForSeconds(25f);

        comotion2Source.Play();
        pidgeonSource.Play();

        yield return new WaitForSeconds(30f);

        runPastAnimator.gameObject.SetActive(true);


        yield return new WaitForSeconds(4f);

        PlayAnnouncement(tannoy[0].universityClip);

        yield return new WaitForSeconds(5f);

        trainPlatform2.TrainStop();

        yield return new WaitForSeconds(15f);

        trainPlatform2.TrainStart();

        yield return new WaitForSeconds(10f);

        PlayAnnouncement(tannoy[0].servicesResuming);

        yield return new WaitForSeconds(15f);

        while (!wetFloorSignSpookTrigger.bounds.Contains(player.transform.position))
        {
            yield return null; 
        }

        wetFloorSignSource.Play();
        wetFloorSign1.SetActive(false);
        wetFloorSign2.SetActive(true);

        while (!wetFloorSignEndTrigger.bounds.Contains(player.transform.position))
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        ticketMachine.PurchaseTicket();
        ticketMachineHand.gameObject.SetActive(true);

        while (!ticketHandPrintTrigger.bounds.Contains(player.transform.position) || !Helpers.LineOfSight(Camera.main.transform, ticketMachineHand.transform, 30f))
            yield return null;

        stingSource.Play(); 

        yield return new WaitForSeconds(25f);

        PlayAnnouncement(tannoy[0].sunderlandClip);

        yield return new WaitForSeconds(5f);

        trainPlatform1.TrainStop();

        yield return new WaitForSeconds(2f);

        GameOver = true;
        killPlayerAnimator.SetTrigger("KillPlayer");
        finaleStingSource.Play();

        yield return new WaitForSeconds(2.42f);

        ScreenFader.Instance.Fade(1,0);
        endTitleGroup.alpha = 1;

        yield return new WaitForSeconds(0.8f);
            
        AudioManager.Instance.SnapAudioClose(5f);

        yield return new WaitForSeconds(2f);

        float t = 0;
        while (endTitleGroup.alpha > 0)
        {
            t += Time.deltaTime;
            endTitleGroup.alpha = Mathf.Lerp(1, 0, t/3);
        }

        yield return new WaitForSeconds(4f);

        //Return to main menu
        SceneManager.LoadScene("Menu");
    }

    public void SetText(string text)
    {
        foreach (ScreenDisplay s in screens)
            s.SetText(text);
    }


    public void PlayAnnouncement(AudioClip clip)
    {
        foreach (Tannoy t in tannoy)
            t.PlayAnnouncement(clip);
    }

    public void Pause()
    {
        if (playing && !GameOver)
        {
            if (!paused)
            {
                paused = true;
                //Time.timeScale = 0f;
            }
            else
            {
                paused = false;
                //Time.timeScale = 1f;
            }

            player.SetActive(!paused);
            pause.SetActive(paused);
        }
    }

}
