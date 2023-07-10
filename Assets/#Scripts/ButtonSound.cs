using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    //Note: attach this class to buttons

    //Play sound when mouse hovers over button
    public void OnHoverPlaySound()
    {
        SoundManager.Instance.PlaySound(SoundManager.mySounds.hoover);
    }

    //Play sound when player clicks button
    public void OnClickPlaySound()
    {
        SoundManager.Instance.PlaySound(SoundManager.mySounds.click);
    }

}
