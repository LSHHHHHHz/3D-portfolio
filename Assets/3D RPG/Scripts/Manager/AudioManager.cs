using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("#BGM")]
    public AudioClip[] BGMClips;
    public float BGMVolume;
    AudioSource[] BGMPlayers;

    [Header("#SFX")]
    public AudioClip[] SFXClip;
    public float SFXVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { DragonSound, DragonDie, BaseAttack, LaunchSkill2, GetHitSkill3, ShootSKill3, BuyItem, Cansel, EquipItem, GetHit, Heal, Jump, Walk, ClosePopup,
        LevelUP, GovlinDie, OpenPopup, DropSound, ClickSlot, CunsumePortion, DragonFling, DragonRotation}


    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        //배경음 초기화
        BGMPlayers = new AudioSource[BGMClips.Length];
        for (int i = 0; i < BGMClips.Length; i++)
        {
            GameObject bgmObject = new GameObject("BgmPlayer" + i);
            bgmObject.transform.parent = transform;
            BGMPlayers[i] = bgmObject.AddComponent<AudioSource>();
            BGMPlayers[i].playOnAwake = false;
            BGMPlayers[i].volume = BGMVolume;
            BGMPlayers[i].loop = true;
            BGMPlayers[i].clip = BGMClips[i];
        }

        //효과음 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = SFXVolume;
        }

    }
    public void PlayBGM(int bgmIndex, bool isPlay)
    {
        if (isPlay)
        {
            BGMPlayers[bgmIndex].Play();
        }
        else
        {
            BGMPlayers[bgmIndex].Stop();
        }
    }
    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }
            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = SFXClip[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
    private Coroutine walkingSoundCoroutine;

    public void PlayWalkingSound()
    {
        if (walkingSoundCoroutine != null)
        {
            StopCoroutine(walkingSoundCoroutine);
        }
        walkingSoundCoroutine = StartCoroutine(PlayWalkingSoundRoutine());
    }

    private IEnumerator PlayWalkingSoundRoutine()
    {
        while (true)
        {
            PlaySfx(Sfx.Walk);

            yield return new WaitForSeconds(1f);
        }
    }
    public void StopWalkingSound()
    {
        if (walkingSoundCoroutine != null)
        {
            StopCoroutine(walkingSoundCoroutine);
            walkingSoundCoroutine = null;
        }
    }
}
