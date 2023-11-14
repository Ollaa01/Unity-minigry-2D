using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject projectileToSpawn;
    [SerializeField] private float fireRate = 0.3f;
    [SerializeField] private Vector2 projectileSpawnOffSet = new Vector2(0f, -0.5f);
    [SerializeField] private Vector2 projectileSpeed = new Vector2(0f, -0.2f);
    [SerializeField] private bool move = true;
    [SerializeField] private int scoreToIncrease = 10;
    

    public bool isBoss = false;
    public int ScoreToIncrease { get { return scoreToIncrease; } }
    public enum EnemyType
    {
        Individual, Wave
    }

    [SerializeField] public EnemyType enemyType;
    private float minHeight = 0f, maxHeight = 0f;
    private float minWidth = 0f, maxWidth = 0f;
    float startHeight = 5.0f;
    private Vector2 minW, maxW;
    private Vector2 startH;
    private Vector2 vel1, vel2;
    [SerializeField] private  float smoothTime = 1f;
    private float maxSpeed = 10f;

    private void Fire()
    {
        Vector2 targetPos = new Vector2(transform.position.x + projectileSpawnOffSet.x, transform.position.y + projectileSpawnOffSet.y);
        GameObject proj = Instantiate(projectileToSpawn, targetPos, projectileToSpawn.transform.rotation) as GameObject;
        if (proj.GetComponent<Rigidbody2D>())
            proj.GetComponent<Rigidbody2D>().AddForce(projectileSpeed, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        smoothTime = Random.Range(0f, 1f);
        if (projectileToSpawn != null)
            InvokeRepeating("Fire", 0.01f, fireRate);
        if (enemyType == EnemyType.Individual)
        {
            minHeight = Screen.height * 0.7f;
            maxHeight = Screen.height * 0.8f;
            Vector2 minH = Camera.main.ScreenToWorldPoint(new Vector2(0f, minHeight));
            Vector2 maxH = Camera.main.ScreenToWorldPoint(new Vector2(0f, maxHeight));
            float height = Random.Range(minH.y, maxH.y);
            Debug.Log("xH " + maxHeight + "nH" + minHeight);

            //startHeight = Screen.height * 1.2f;
            

            minWidth = Screen.width * 0.2f;
            maxWidth = Screen.width * 0.8f;
            minW = Camera.main.ScreenToWorldPoint(new Vector2(minWidth, 0f));
            maxW = Camera.main.ScreenToWorldPoint(new Vector2(maxWidth, 0f));
            minW.y = height;
            maxW.y = height;
            float width = Random.Range(minW.y, maxW.y);
            Debug.Log("xW " + maxWidth + "nW" + minWidth);

            transform.position = new Vector2(width, startHeight);
            Debug.Log("width " + width + " heihght " + startHeight);
        }
    }

    private bool nearMin = false, nearMax = false, exec = false;
    // Update is called once per frame
    void Update()
    {
        if(enemyType == EnemyType.Individual && move)
        {
            if(!exec)
            {
                transform.position = Vector2.SmoothDamp(transform.position, minW, ref vel1, smoothTime, maxSpeed, Time.deltaTime);
                if (transform.position.x - 0.1f <= minW.x)
                {
                    nearMin = true;
                    nearMax = false;
                    exec = true;
                }
            } else
            {
                if (transform.position.x - 0.1f <= minW.x)
                {
                    nearMin = true;
                    nearMax = false;
                }
                if (transform.position.x + 0.1f >= maxW.x)
                {
                    nearMin = false;
                    nearMax = true;
                }
                if (nearMax)
                    transform.position = Vector2.SmoothDamp(transform.position, minW, ref vel1, smoothTime, maxSpeed, Time.deltaTime);
                else if (nearMin)
                    transform.position = Vector2.SmoothDamp(transform.position, maxW, ref vel1, smoothTime, maxSpeed, Time.deltaTime);
            }
        }
    }
}
