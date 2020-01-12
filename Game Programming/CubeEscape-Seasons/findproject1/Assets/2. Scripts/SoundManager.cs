using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager sInstansce;
    public float sfxVolumn = 1.0f;
    public float bgmVolumn = 1.0f;
    public bool isSfxMute = false;

    public static SoundManager Instance
    {
        get
        {
            if (sInstansce == null)
            {
                GameObject newGameObject = new GameObject("_GameManager");
                sInstansce = newGameObject.AddComponent<SoundManager>();
            }

            return sInstansce;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    public void SoundVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void PlaySfx(AudioClip sfx)
    {
        if (isSfxMute) return;

        GameObject soundObj = new GameObject("Sfx");

        AudioSource _audioSource = soundObj.AddComponent<AudioSource>();
        _audioSource.clip = sfx;
        _audioSource.minDistance = 10.0f;
        _audioSource.maxDistance = 30.0f;
        _audioSource.volume = sfxVolumn;

        _audioSource.Play();

        Destroy(soundObj, sfx.length);
    }
}
