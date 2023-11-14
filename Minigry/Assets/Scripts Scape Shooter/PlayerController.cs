using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public enum MovementInputType
    {
        PointerBased, ButtonBased
    }
    [SerializeField]
    private MovementInputType movementInputType;
    [SerializeField]
    private KeyCode up = KeyCode.UpArrow, down = KeyCode.DownArrow, left = KeyCode.LeftArrow, right = KeyCode.RightArrow;
    [SerializeField]
    private VButton uVB, dVB, lVB, rVB;
    [SerializeField]
    private float Speed = 10f;
    [SerializeField]
    private Vector2 minPos, maxPos;
    private Vector2 pos;

    [Header("Laser")]
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private Vector2 laserSpeed = new Vector2(0f, 1f);
    [SerializeField]
    private Vector3 spawnOffset;
    [SerializeField]
    private float laserFireRate = 0.2f;
    [SerializeField]
    private KeyCode laserKey = KeyCode.Mouse1;

    private ObjectPool laserPool;
    [SerializeField]
    private int laserPoolsize = 30;

    [Header("Missile")]
    [SerializeField]
    private GameObject missile;
    [SerializeField]
    private Vector2 missileSpeed = new Vector2(0f, 0.5f);
    [SerializeField]
    private Vector3 spawnOffsetMissile;
    [SerializeField]
    private KeyCode missileKey = KeyCode.Mouse1;

    private ObjectPool missilePool;
    [SerializeField]
    private int missilePoolsize = 30;

    // Start is called before the first frame update
    void Start()
    {
        
        Instance = this;
        laserPool = new ObjectPool(laser, laserPoolsize, "PlayerLaserPool");
        missilePool = new ObjectPool(missile, missilePoolsize, "PlayerMissilePool"); 
    }
    public void ReleaseLaser(GameObject laser)
    {
        laserPool.ReturnInstance(laser);
    }
    public void ReleaseMissile(GameObject missile)
    {
        missilePool.ReturnInstance(missile);
    }

    private void Fire()
    {
        if (GameStatsManager.Instance.CheckIfCanShootLaser(1))
        {
            GameObject laserInstance = laserPool.GetInstance();
            laserInstance.transform.position = transform.position + spawnOffset;
            laserInstance.GetComponent<Rigidbody2D>().AddForce(laserSpeed, ForceMode2D.Impulse);
            GameStatsManager.Instance.ShootLasersByAmount(1);
        }
    }

    private void MissileFire()
    {
        if (GameStatsManager.Instance.CheckIfCanShootMissiles(1))
        {
            GameObject missileInstance = missilePool.GetInstance();
            missileInstance.transform.position = transform.position + spawnOffsetMissile;
            missileInstance.GetComponent<Rigidbody2D>().AddForce(missileSpeed, ForceMode2D.Impulse);
            GameStatsManager.Instance.ShootMissilesByAmount(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Instance = this;
        if (movementInputType == MovementInputType.ButtonBased)
        {
 #if UNITY_STANDALONE || UNITY_WEBGL
            if(Input.GetKey(up))
            {
                transform.Translate(Speed * Vector2.up * Time.deltaTime);
            }
            else if (Input.GetKey(down))
            {
                transform.Translate(Speed * Vector2.down * Time.deltaTime);
            }
            if (Input.GetKey(left))
            {
                transform.Translate(Speed * Vector2.left * Time.deltaTime);
            }
            else if (Input.GetKey(right))
            {
                transform.Translate(Speed * Vector2.right * Time.deltaTime);
            }
#endif

#if UNITY_ANDROID || UNITY_IOS
            if (uVB != null && dVB != null && lVB != null && rVB != null)
            {
                if (uVB.value)
                {
                    transform.Translate(Speed * Vector2.up * Time.deltaTime);
                }
                else if (dVB.value)
                {
                    transform.Translate(Speed * Vector2.down * Time.deltaTime);
                }
                if (lVB.value)
                {
                    transform.Translate(Speed * Vector2.left * Time.deltaTime);
                }
                else if (rVB.value)
                {
                    transform.Translate(Speed * Vector2.right * Time.deltaTime);
                }
            }
#endif
        }
        else
        {
            Vector3 rawPos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(rawPos);
            if (Input.GetKey(KeyCode.Mouse0))
                transform.position = Vector3.Lerp(transform.position, worldPos, Speed * Time.deltaTime);
        }
#if UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetKeyDown(laserKey))
            InvokeRepeating("Fire", 0.001f, laserFireRate);

        if (Input.GetKeyUp(laserKey))
            CancelInvoke("Fire");

        if (Input.GetKeyDown(missileKey))
            MissileFire();
#endif
        pos.x = Mathf.Clamp(transform.position.x, minPos.x, maxPos.x);
        pos.y = Mathf.Clamp(transform.position.y, minPos.y, maxPos.y);

        transform.position = pos;

    }
}
