using UnityEngine;

public class PlayerLightInteraction : MonoBehaviour
{
    //Note: attached to player
    //Properties
    private bool isVisible { get; set; }
    private SpriteRenderer spriteRenderer { get; set; }

    //Methods
    public bool GetPlayerIsVisible()
    {
        return (isVisible);
    }

    private void Start()
    {
        isVisible = true;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    //Become invisible when entering lit area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Light")
        {
            Debug.Log("You've entered a light zone. You became invisible!");
            isVisible = false;

            //become transparent
            spriteRenderer.color = new Color(Color.black.r, Color.black.g, Color.black.b, 0.2f);
        }
    }

    //Become visible when exiting lit area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Light")
        {
            Debug.Log("You've left a light zone. You are now visible!");
            isVisible = true;

            //become visible
            spriteRenderer.color = new Color(Color.black.r, Color.black.g, Color.black.b, 1);
        }
    }

}
