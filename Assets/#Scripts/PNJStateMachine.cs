using UnityEngine;

public class PNJStateMachine : MonoBehaviour
{
    //Note: attach to PNJs
    //Enum of pnj states
    public enum state
    {
        asleep,
        awake,
        scared
    }

    //Game objects that need to be dragged down in the Inspector
    [SerializeField] Sprite[] animations;
    [SerializeField] GameObject[] emotions;
    
    //Properties
    private state currentState { get; set; }
    private GameObject currentEmotion { get; set; }

    private void Start()
    {
        currentState = state.asleep;
        gameObject.GetComponent<SpriteRenderer>().sprite = animations[(int)currentState];
        ChangeEmotion(); //this automatically sets currentEmotion to asleep
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //State asleep
        if (other.tag == "Player" && currentState == state.asleep)
        {
            SoundManager.Instance.PlaySound(currentState);
            Debug.Log("zzz...zzz...zzz");
        }

        //State asleep > awake
        if (other.tag == "Light" && currentState == state.asleep)
        {
            currentState = state.awake;
            //Update graphics
            ChangeEmotion();
            gameObject.GetComponent<SpriteRenderer>().sprite = animations[(int)currentState];
            //Play sound
            Debug.Log("I'm awake!");
            SoundManager.Instance.PlaySound(currentState);
        }

        //State awake > scared
        if (other.tag == "Player" && other.GetComponent<PlayerLightInteraction>().GetPlayerIsVisible() && currentState == state.awake)
        {
            currentState = state.scared;
            GameState.Instance.AddScaredPeople(); //Update counter of scared people
            //Update graphics
            ChangeEmotion();
            //mySpriteRenderer.sprite = animations[(int)currentState];  //No change in animation needed
            //Play sound
            Debug.Log("There's someone there!");
            SoundManager.Instance.PlaySound(currentState);
        }

    }

    //Function for changing the emotion bubble of PNJ
    private void ChangeEmotion()
    {
        if (currentEmotion != null)
        {
            Destroy(currentEmotion);
            
        }
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x -0.3f, pos.y + 1, 0);
        currentEmotion = Instantiate(emotions[(int)currentState], pos, new Quaternion());
    }
}
