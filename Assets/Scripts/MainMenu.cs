using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //public AudioClip[] hoverbtnSFX;
    public AudioClip[] startbtnSFX;

    public Image Fade;

    private void Start()
    {
        
    }
    public void StartGame()
    {
        Fade.gameObject.SetActive(true);
        SoundManager.Instance.PlayRandomSFXClip(startbtnSFX, transform, true, 1f);
        //SoundManager.Instance.TransitionMusicClip(MusicType.Game, 1f);
        LMotion.Create(0f, 1f, 0.5f)
            .WithOnComplete(SwitchToGame)
            .BindToColorA(Fade);
    }
    
    public void Options()
    {
        Fade.gameObject.SetActive(true);
        SoundManager.Instance.PlayRandomSFXClip(startbtnSFX, transform, true, 1f);
        LMotion.Create(0f, 1f, 0.5f)
            .WithOnComplete(SwitchToGame2)
            .BindToColorA(Fade);
    }

    private void SwitchToGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    
    private void SwitchToGame2()
    {
        SceneManager.LoadScene("Options");
    } 
    
    
    
    // public void QuitGame()
    // {
    //     Debug.Log("Quit Game");
    //     Application.Quit();
    // }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
        //Hover();
    //}
    
    // private void Hover()
    // {
    //     SoundManager.Instance.PlayRandomSFXClip(hoverbtnSFX, transform, true, 1f);
    // }
    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     throw new System.NotImplementedException();
    // }
}