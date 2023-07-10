using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //Note: Attached to MusicManager
    //Game objects that need to be dragged down in the Inspector
    [SerializeField]
    private AudioClip[] musics;

    //Properties
    private AudioSource audioSource { get; set; }

    //Enum of music names
    public enum myMusics
    {
        menu,
        gameplay,
        victory,
        gameOver
    }

    //Singleton
    public static MusicManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this); //This is needed so the music continues playing when another scene is loaded!
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    //Methods
    //...for playing music
    public void PlayMusic(myMusics music)
    {
        //kill previous clip
        StartCoroutine(FadeOut(audioSource));
        //change music clip
        audioSource.clip = musics[(int)music];
        audioSource.loop = true;
        //Play new clip
        audioSource.Play();
        StartCoroutine(FadeIn(audioSource));
        Debug.Log("Now playing" + audioSource.clip.name);
    }

    //..polymorph with a bool for the loop
    public void PlayMusic(myMusics music, bool isLoop)
    {
        //kill previous clip
        StartCoroutine(FadeOut(audioSource));
        //change music clip
        audioSource.clip = musics[(int)music];
        audioSource.loop = isLoop;
        //Play new clip
        audioSource.Play();
        StartCoroutine(FadeIn(audioSource));
        Debug.Log("Now playing" + audioSource.clip.name);
    }

    //Coroutines
    //...for decreasing volume when the music stops
    IEnumerator FadeOut(AudioSource audioSource)
    {
        for (float alpha = 1f; alpha >= 0f; alpha -= 0.1f)
        {
            audioSource.volume = alpha;
            if (alpha == 0) audioSource.Stop();
            yield return null;
        }
    }

    //...for increasing volume when the music starts
    IEnumerator FadeIn(AudioSource audioSource)
    {
        for (float alpha = 0f; alpha <= 1f; alpha += 0.1f)
        {
            audioSource.volume = alpha;
            yield return null;
        }
    }

}
