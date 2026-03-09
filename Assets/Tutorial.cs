using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> tutorialObjects;
    private int index = 0;
    public string levelName;
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickTutorial()
    {
        index++;
        if (index >= tutorialObjects.Count)
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            return;
        }
        tutorialObjects[index - 1].SetActive(false);
    }
}
