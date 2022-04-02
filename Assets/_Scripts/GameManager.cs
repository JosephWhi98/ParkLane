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

    public Tannoy tannoy;

    public IEnumerator Start()
    {
        playing = false;
        yield return new WaitForSeconds(1f);
        ScreenFader.Instance.Fade(0, 2f);
        yield return new WaitForSeconds(2f);
        playing = true;

        StartCoroutine(GameRoutine());
    }

    public IEnumerator GameRoutine()
    {
        yield return new WaitForSeconds(5f);

        tannoy.PlayAnnouncement(tannoy.delayClip);

        yield return new WaitForSeconds(10f);

        tannoy.PlayAnnouncement(tannoy.sunderlandClip);

        yield return new WaitForSeconds(10f);

        tannoy.PlayAnnouncement(tannoy.universityClip);
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
