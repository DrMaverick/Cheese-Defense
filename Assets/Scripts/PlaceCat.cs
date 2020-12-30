using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCat : MonoBehaviour
{
    public GameObject catPrefab;
    private GameObject cat;
    private GameManagerBehavior gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool canPlaceCat()
    {
        int cost = catPrefab.GetComponent<CatData>().levels[0].cost;
        return cat == null && gameManager.Gold >= cost; //确保金币足够
    }

    // 1
    void OnMouseUp()
    {
        // 2
        if (canPlaceCat())
        {
            // 3
            cat = (GameObject)
                Instantiate(catPrefab, transform.position, Quaternion.identity);
            // 4
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);

            // todo: Deduct gold
            gameManager.Gold -= cat.GetComponent<CatData>().CurrentLevel.cost;
        }
        else if (canUpdateCat())
        {
            cat.GetComponent<CatData>().increaseLevel();
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.PlayOneShot(audioSource.clip);
            gameManager.Gold -= cat.GetComponent<CatData>().CurrentLevel.cost;
        }
    }
    private bool canUpdateCat()
    {
        if (cat != null)
        {
            CatData monsterData = cat.GetComponent<CatData>();
            CatLevel nextLevel = monsterData.getNextLevel();
            if (null != nextLevel) //更高的等级存在
            {
                return gameManager.Gold >= nextLevel.cost;
            }
        }

        return false;
    }
}
