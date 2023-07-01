using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<Sound> sounds;

    private void Awake()
    {
        foreach(Sound sound in sounds)
        {
            sound.soundSource = gameObject.AddComponent<AudioSource>();
            sound.soundSource.clip = sound.clip;

            sound.soundSource.volume = sound.volume;
            sound.soundSource.priority = sound.priority;
            sound.soundSource.loop = sound.isLoop;
        }
    }


    public void PlaySound(string soundName)
    {
        Sound desiredSound = sounds.Find(sound => sound.name == soundName);
        if (desiredSound != null)
        {
            desiredSound.soundSource.Play();
        }
    }

    public void StopSound(string soundName)
    {
        Sound desiredSound = sounds.Find(sound => sound.name == soundName);
        if (desiredSound != null)
        {
            desiredSound.soundSource.Stop();
        }
    }
}
