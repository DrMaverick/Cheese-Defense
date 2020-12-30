using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CatLevel
{
    public int cost; //召唤猫所消耗的金币
    public GameObject visualization;    //猫在某个特定等级的视觉效果
    public GameObject bullet;
    public float fireRate;
}
public class CatData : MonoBehaviour
{
    public List<CatLevel> levels;
    private CatLevel currentLevel;
    // 1
    public CatLevel CurrentLevel
    {
        //  2
        get
        {
            return currentLevel;
        }
        //  3
        set
        {
            currentLevel = value;
            int currentLevelIndex = levels.IndexOf(currentLevel);

            //根据currentLevelIndex的值，来决定猫的形态
            GameObject levelVisualization = levels[currentLevelIndex].visualization;
            for (int i = 0; i < levels.Count; i++)
            {
                if (levelVisualization != null)
                {
                    if (i == currentLevelIndex)
                    {
                        levels[i].visualization.SetActive(true);
                    }
                    else
                    {
                        levels[i].visualization.SetActive(false);
                    }
                }
            }
        }
    }

    void OnEnable()
    {
        CurrentLevel = levels[0];
    }
    public CatLevel getNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
        }
        else
        {
            return null;
        }
    }
    public void increaseLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        if (currentLevelIndex < levels.Count - 1)
        {
            CurrentLevel = levels[currentLevelIndex + 1];
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
