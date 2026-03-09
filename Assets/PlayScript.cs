using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayScript : MonoBehaviour
{
    public AudioClip[] btnSFX;
    public Image Fade;
    
    public void LevelOne()
    {
        Fade.gameObject.SetActive(true);
        SoundManager.Instance.PlayRandomSFXClip(btnSFX, transform, true, 1f);
        LMotion.Create(0f, 1f, 0.5f)
            .WithOnComplete(SwitchToGame)
            .BindToColorA(Fade);
    }
    
    public void LevelTwo()
    {
        Fade.gameObject.SetActive(true);
        SoundManager.Instance.PlayRandomSFXClip(btnSFX, transform, true, 1f);
        LMotion.Create(0f, 1f, 0.5f)
            .WithOnComplete(SwitchToGame2)
            .BindToColorA(Fade);
    }
    
    public void Back()
    {
        Fade.gameObject.SetActive(true);
        SoundManager.Instance.PlayRandomSFXClip(btnSFX, transform, true, 1f);
        LMotion.Create(0f, 1f, 0.5f)
            .WithOnComplete(SwitchToGame3)
            .BindToColorA(Fade);
    }
    
    
    
    private void SwitchToGame()
    {
        SceneManager.LoadScene("Level1HowTo", LoadSceneMode.Single);
    }
    
    private void SwitchToGame2()
    {
        SceneManager.LoadScene("Level2HowTo", LoadSceneMode.Single);
    }
    
    private void SwitchToGame3()
    {
        SceneManager.LoadScene("Main Menu");
    } 
    
}
