using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class Interactable : MonoBehaviour
{
    //Note: Attached to Switch prefab
    //Game objects that need to be dragged down in the Inspector
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private Tilemap lightArea;
    [SerializeField] private Light2DBase[] light2D;

    //Properties
    private bool isOn { get; set; }

    //Function for switching the lights
    private void SwitchLight()
    {
        lightArea.GetComponent<TilemapCollider2D>().enabled = isOn;
        for (int i = 0; i<light2D.Length; i++)
        {
            light2D[i].enabled = isOn;
        }
    }

private void Start()
    {
        //Initiate
        message.enabled = false;

        //Randomly decide the status of the light
        int index = Random.Range(0, 100);
        if (index > 80) 
        {
            isOn = true;
        } 
        else if (index <= 80) 
        {
            isOn = false;
        }

        //Switch off/on the light
        SwitchLight();
    }

    private void Update()
    {
        if(message.enabled && Input.GetKeyDown(KeyCode.Space))
        {
            //Change the status of light
            isOn = !isOn;
            //Switch off/on the light
            SwitchLight();
        }
    }

    //Show message when the player enters collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            message.enabled = true;
        }
    }

    //Hide message when the player exits collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            message.enabled = false;
        }
    }

}
