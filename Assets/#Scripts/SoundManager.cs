using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Enum of all the sounds
    public enum mySounds
    {
        //UiSounds
        hoover,
        click,
        sleep,
        awake1,
        awake2,
        scared1,
        scared2,
        scared3
    }

    //Game objects that need to be dragged down in the Inspector
    [SerializeField] private AudioClip[] sounds;

    //Properties
    private int counterAwake { get; set; }
    private int counterScared { get; set; }

    //Singleton
    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        counterAwake = 0;
        counterScared = 0;
    }

    //Methods
    //...for playing a sound
    public void PlaySound(mySounds sound)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sounds[(int)sound];
        audioSource.PlayOneShot(audioSource.clip);
        Debug.Log("Playing " + audioSource.clip.name);
    }

    //...polymorphy, for playing a sound related to PNJ state
    public void PlaySound(PNJStateMachine.state state)
    {
        switch (state)
        {
            case PNJStateMachine.state.asleep:
                PlaySound(mySounds.sleep);
                break;

            case PNJStateMachine.state.awake:
                if(counterAwake >= 1)
                {
                    PlaySound(mySounds.awake1);
                    counterAwake = 0;
                }
                else if (counterAwake == 0)
                {
                    PlaySound(mySounds.awake2);
                    counterAwake += 1;
                }
                break;

            case PNJStateMachine.state.scared:
                if (counterScared >= 2)
                {
                    PlaySound(mySounds.scared1);
                    counterScared = 0;
                }
                else if (counterScared == 1)
                {
                    PlaySound(mySounds.scared2);
                    counterScared += 1;
                }
                else if (counterScared == 0)
                {
                    PlaySound(mySounds.scared3);
                    counterScared += 1;
                }
                break;

            default:
                break;
        }
    }
}
