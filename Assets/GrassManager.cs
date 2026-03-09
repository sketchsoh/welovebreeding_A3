using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GrassManager : MonoBehaviour
{
    public string levelName;
    public GameObject grassPrefab;
    public GameObject grassStart;
    public GameObject waterEnd;

    public GameObject flowerUi1;
    public GameObject flowerUi2;
    public GameObject flowerUi3;

    public GameObject flower1;
    public GameObject flower2;
    public GameObject flower3;

    public GameObject water1;
    public GameObject water2;

    public GameObject sand;
    
    public List<Flower> flowerList;
    public List<GameObject> rocks;
    public Sprite flowerSprite;
    public Sprite flowerFlourishedSprite;
    public Sprite fertilizerSprite;
    public Sprite defaultFlowerSprite;
    public Sprite winGrassSprite;
    public TextMeshProUGUI waterText;

    public float minRange;
    public float maxRange;
    
    public int startingGrassCount;
    public int waterFlowerCount;
    private int grassCount;
    private bool stageEnd;
    private Queue<GameObject> grassQueue;
    private List<GameObject> grassList;
    private Dictionary<GameObject, bool> waterList;

    public AudioClip[] flowerSounds;
    public AudioClip[] waterSounds;
    public AudioClip[] winGrassSounds;
    
    void Start()
    {
        InitPool();
    }

    // Update is called once per frame
    void Update()
    {
        PlaceGrass();
    }

    void InitPool()
    {
        stageEnd = false;
        grassQueue = new Queue<GameObject>();
        grassCount = startingGrassCount;
        waterText.text = ":" + startingGrassCount;
        for (int i = 0; i < grassCount; i++)
        {
            GameObject grass = Instantiate(grassPrefab, transform, true);
            grassQueue.Enqueue(grass);
            grass.SetActive(false);
        }

        grassList = new List<GameObject>();
        grassList.Add(grassStart);
        
        flowerList = new List<Flower>
        {
            new Flower(flower1, flowerUi1),
            new Flower(flower2, flowerUi2),
            new Flower(flower3, flowerUi3),
        };

        waterList = new Dictionary<GameObject, bool>
        {
            { water1, false },
            { water2, false },
        };

        foreach (var flower in flowerList)
        {
            flower.touched = false;
            flower.flowerUi.GetComponent<Image>().sprite = defaultFlowerSprite;
            flower.flower.GetComponent<SpriteRenderer>().sprite = fertilizerSprite;
            flower.flower.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    void PlaceGrass()
    {
        if (stageEnd) return;
        if (grassQueue.Count == 0) return;
        if (Input.GetMouseButton(0))
        {
            
            
            bool tooClose = false;
            bool inRange = false;


            Vector2 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (GameObject grass in grassList)
            {
                if (!grass.activeSelf) continue; // Skip pooled/inactive grass
                float dist = Vector2.Distance(camPos, grass.transform.position);

                if (dist < minRange)
                {
                    tooClose = true;
                    break; // No need to check further
                }

                if (dist <= maxRange)
                {
                    inRange = true;
                }
            }

            if (tooClose || !inRange) return;
            GameObject g = grassQueue.Dequeue();
            g.transform.position = camPos;
            if (rocks.Count != 0)
            {
                foreach (var rock in rocks)
                {
                    if (rock.GetComponent<SpriteRenderer>().bounds.Contains(g.transform.position))
                    {
                        grassQueue.Enqueue(g);
                        return;
                    }
                }
            }

            g.SetActive(true);
            grassList.Add(g);
            if (sand != null)
            {
                if (sand.GetComponent<SpriteRenderer>().bounds
                    .Contains(g.GetComponent<SpriteRenderer>().bounds.center))
                {
                    GameObject extra = grassQueue.Dequeue();
                    Destroy(extra);
                }
            }
            SoundManager.Instance.PlayRandomSFXClip(winGrassSounds, transform);

            waterText.text = ":" + grassQueue.Count;
            if (!grassList.Contains(g))
                grassList.Add(g);
            CheckContact(g);
        } 
    }


    void CheckContact(GameObject g)
    {
        if (waterEnd.GetComponent<SpriteRenderer>().bounds.Contains(g.GetComponent<SpriteRenderer>().bounds.center) && !stageEnd)
        {
            stageEnd = true;
            Debug.Log("Win");
            StartCoroutine(WinSequence());
            return;
        }
        foreach (Flower f in flowerList)
        {
            if (f.touched) continue;
            float dist = Vector2.Distance(g.transform.position, f.flower.transform.position);
            if (dist < minRange)
            {
                Debug.Log("Flower touched!");
                f.touched = true;
                f.flowerUi.GetComponent<Image>().sprite = flowerSprite;
                f.flower.GetComponent<SpriteRenderer>().sprite = flowerFlourishedSprite;
                f.flower.GetComponent<SpriteRenderer>().sortingOrder = 5;
                SoundManager.Instance.PlayRandomSFXClip(flowerSounds, transform);
            }
        }

        foreach (var w in waterList.Keys.ToList())
        {
            if (waterList[w]) continue;
            float dist = Vector2.Distance(g.transform.position, w.transform.position);
            if (dist < minRange)
            {
                SoundManager.Instance.PlayRandomSFXClip(waterSounds, transform);
                waterList[w] = true;
                Debug.Log("Water touched!");
                int extraWater = waterFlowerCount;
                for (int i = 0; i < extraWater; i++)
                {
                    GameObject grass = Instantiate(grassPrefab, transform, true);
                    grassQueue.Enqueue(grass);
                    grass.SetActive(false);
                }
                waterText.text = ":" + grassQueue.Count;
            }
        }
    }
    
    IEnumerator WinSequence()
    {
        for (int i = grassList.Count - 1; i >= 0; i--)
        {
            if (!grassList[i].activeSelf) continue;
            grassList[i].GetComponent<SpriteRenderer>().sprite = winGrassSprite;
            yield return new WaitForSeconds(0.05f); // Adjust delay as needed
            SoundManager.Instance.PlayRandomSFXClip(winGrassSounds, transform);
        }

        SceneManager.LoadScene("Level Complete", LoadSceneMode.Additive);
    }

    public void ResetStage()
    {
        Debug.Log("Resetting stage");
        foreach (GameObject grass in grassList)
        {
            if (grass.CompareTag("Player")) continue;
            Destroy(grass);
        }
        InitPool();
    }

    public void PauseStage()
    {
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }
}

public class Flower
{
    public GameObject flower;
    public GameObject flowerUi;
    public bool touched;

    public Flower(GameObject flower, GameObject flowerUi)
    {
        this.flower = flower;
        this.flowerUi = flowerUi;
        touched = false;
    }
}
