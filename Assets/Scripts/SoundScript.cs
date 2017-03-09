using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static SoundScript Instance;
    public AudioClip jumpSound;
    public AudioClip coinSound;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SoundsScript!");
        }
        else
        {
            Instance = this;
        }
    }

    public void MakeJumpSound()
    {
        MakeSound(jumpSound);
    }

    public void MakeCoinSound()
    {
        MakeSound(coinSound);
    }

    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
