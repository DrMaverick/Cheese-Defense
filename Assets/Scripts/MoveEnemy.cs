using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] waypoints; //所有的路标
    private int currentWaypoint = 0; //敌人当前所在的路标
    private float lastWaypointSwitchTime; //敌人经过上一个路标的时刻
    public float speed = 1.0f;  //敌人的移动速度

    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // 1  从路标数组中，取出当前路段的开始路标和结束路标
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;

        // 2 计算出通过整个路段的距离
        float pathLength = Vector2.Distance(startPosition, endPosition);
        float totalTimeForPath = pathLength / speed; //计算出通过整个路段所需要的时间
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
        // 计算出当前时刻应该在的位置
        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

        // 3 检查敌人是否已经抵达结束路标
        if (gameObject.transform.position.Equals(endPosition))
        {
            //敌人尚未抵达最终的路标
            if (currentWaypoint < waypoints.Length - 2)
            {
                currentWaypoint++;
                lastWaypointSwitchTime = Time.time;
            }
            else  //敌人抵达了最终的路标
            {
                Destroy(gameObject);

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                GameManagerBehavior gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
                gameManager.Health -= 1;
            }
        }
    }

    public float distanceToGoal()
    {
        float distance = 0;
        distance += Vector3.Distance(
            gameObject.transform.position,
            waypoints[currentWaypoint + 1].transform.position);
        for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++)
        {
            Vector3 startPosition = waypoints[i].transform.position;
            Vector3 endPosition = waypoints[i + 1].transform.position;
            distance += Vector3.Distance(startPosition, endPosition);
        }
        return distance;
    }


}
