using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroBehaviour : MonoBehaviour
{
    public Animator Animator;
    public Button PlayButton;
    public AudioSource PlaySfx;
    private static readonly int OnPlay = Animator.StringToHash("OnPlay");

    [UsedImplicitly]
    public void OnIntroEnded() => SceneManager.LoadScene(1);

    private void OnEnable()
    {
        PlayButton.onClick.AddListener(PlayGame);
    }
    
    private void OnDisable()
    {
        PlayButton.onClick.RemoveListener(PlayGame);
    }

    private void PlayGame()
    {
        PlaySfx.Play();
        Animator.SetTrigger(OnPlay);
    }
}