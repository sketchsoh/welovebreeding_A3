using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Complete : MonoBehaviour
{
    public TextMeshProUGUI flourishText;
    public List<GameObject> flowers;
    private int numberOfFlowers;
    private bool animating;
    private GrassManager gm;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GrassManager>();
        numberOfFlowers = 0;
        animating = true;
        foreach (var f in gm.flowerList)
        {
            if (f.touched) numberOfFlowers++;
        }
        
        StartCoroutine(AnimateFlowers());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    
    IEnumerator AnimateFlowers()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < numberOfFlowers; i++)
        {
            flowers[i].GetComponent<Image>().sprite = gm.flowerSprite;
            yield return new WaitForSeconds(0.5f);
        }

        animating = false;
    }

    public void NextStage()
    {
        if (gm.levelName == "1")
        {
            SceneManager.LoadScene("Level2HowTo", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
    }
}
