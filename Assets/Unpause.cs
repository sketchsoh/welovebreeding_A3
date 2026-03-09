using UnityEngine;
using UnityEngine.SceneManagement;

public class Unpause : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnPause()
    {
        SceneManager.UnloadSceneAsync("Pause");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
