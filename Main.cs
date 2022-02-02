using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    //사운드 재생
    public AudioSource myFx;   
    public AudioClip[] clickFx;
    private int playing_count;

    //버튼
    public Canvas[] canvas;

    //페이드 아웃
    private float Fadetime;
    public static bool playing = false;
    public CanvasGroup canvas2;

    //비
    public ParticleSystem rainparticle;

    //컨트롤러 위치값
    //public Text InputText;

    //타이머
    private int count_time;
    private bool start_count;

    private void Awake()
    {        
        Playsound(clickFx[1], myFx);
        canvas2.alpha = 0;
        canvas[1].enabled = false;
        Fadetime = Time.deltaTime;

        playing_count = 0;
    }

    public static void Playsound(AudioClip clip, AudioSource audioPlayer)
    {
        audioPlayer.clip = clip;
        audioPlayer.loop = true;
        audioPlayer.Play();
    }

    public static void Playrain(AudioClip clip, AudioSource audioPlayer)
    {
        audioPlayer.clip = clip;
        audioPlayer.loop = true;
        audioPlayer.Play();
    }

    public IEnumerator ClickSound()
    {
        myFx.Stop();
        myFx.PlayOneShot(clickFx[0]);
        canvas[0].enabled = false;
        yield return new WaitForSeconds(clickFx[0].length);
        // 화면 전환 코드
        rainparticle.Play();
        Playrain(clickFx[2], myFx);
        canvas[1].enabled = true;
        Moving.turngoing();
    }

    public void Click()
    {
        StartCoroutine(ClickSound());        
    }

    public void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) == true && playing_count == 0)
        {
            Debug.Log("key_entered");
            playing_count++;
            playing = true;
            Click();
            start_count = true;
        }

        if(start_count == true)
        {
            count_time++;
        }

        if (playing == true && count_time > 120)
        {
            Fadetime = 0.8f;            
            canvas2.alpha += Fadetime * Time.deltaTime;
            if(canvas2.alpha == 1)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public static void Playing()
    {
        playing = true;        
    }

    private void OnMouseDown()
    {
        playing = true;
        Click();
        start_count = true;
    }
}
