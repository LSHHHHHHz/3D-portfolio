using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("#BGM")]
    public AudioClip BGMClip;
    public float BGMVolume;
    AudioSource BGMPlayer;

    [Header("#SFX")]
    public AudioClip[] SFXClip;
    public float SFXVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { BasicAttack, ConsumePortion, GetItem, KeyboardTyping, InventoryClick, ClashSkill, WindSkill, NPCTalk, Grunting1, Grunting2, Walk, Cancell, OKSound, Acquisition, FireSkill, SwordSound }


    private void Awake()
    {
        instance = this;
        Init();
        PlayerBGM(true);
    }

    void Init()
    {
        //배경음 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        BGMPlayer = bgmObject.AddComponent<AudioSource>();
        BGMPlayer.playOnAwake = false;
        BGMPlayer.volume = BGMVolume;
        BGMPlayer.loop = true;
        BGMPlayer.clip = BGMClip;

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
    public void PlayerBGM(bool isPlay)
    {
        if (isPlay)
        {
            BGMPlayer.Play();
        }
        else
        {
            BGMPlayer.Stop();
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
