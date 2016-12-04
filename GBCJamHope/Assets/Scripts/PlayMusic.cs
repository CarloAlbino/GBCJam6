using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour{

    public AudioSource m_audioSource;
    bool isPlaying = false;

    public void Activate()
    {
        if (!isPlaying)
        {
            Debug.Log("Playing Music");
            m_audioSource.Play();
            isPlaying = true;
        }
    }
}
