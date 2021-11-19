using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource game;
    [SerializeField] private AudioSource gameMenu;
    [SerializeField] private AudioSource musicBonus;
    [SerializeField] private AudioSource musicJump;
    [SerializeField] private AudioSource musicDie;


    public void PlayGame()
    {
        gameMenu.Stop();
        game.Play();
    }

    public void PlayMenu()
    {
        gameMenu.Play();
        game.Stop();
    }

    public void JumpMusic()
    {
        musicJump.Play();
    }

    public void BonusMusic()
    {
        musicBonus.Play();
    }

    public void DieMusic()
    {
        musicDie.Play();
        game.Stop();
        gameMenu.Stop();
    }
}
