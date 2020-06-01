using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    #region Singlton

    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Try create another instance of game manager!");
        }

    }
    #endregion

    [SerializeField] private GameObject player;
    [SerializeField] private Transform emptyBodyFolder;
    [SerializeField] private AnimationCurve rotateCurve;

    //prefabs
    [SerializeField] private GameObject emptyBody;
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private GameObject potionPrefab;
    
    //info for spawn manager

    [SerializeField] private List<Transform> enemySpawnPoints;
    [SerializeField] private Transform enemiesFolder;
    [SerializeField] private Transform potionFolder;
    private Queue<GameObject> enemiesPool   = new Queue<GameObject>();
    private Queue<GameObject> potionPool    = new Queue<GameObject>();
    private int potionPoolCounter;
    private int enemyPoolCounter;
    private int currentSpawnInWave;
    private int maxSpawnInWave;
    private List<Coroutine> waveCoroutin = new List<Coroutine>();

    private float rotateStep    = 90;
    private float rotateTime    = 0.5f;
    private bool isRotate       = false;
    private bool isGameProgress = false;
    private GameObject[] billboardSprites;
    private List<GameObject> deathBodys = new List<GameObject>();

    public GameObject Player    => player;
    public bool IsGameProgress { set { isGameProgress = value; } get { return isGameProgress; } }


    void Start()
    {
        ActivateBillboardsEffect();
        //StartSpawnManager();
    }

    private void Update()
    {
        if(SceneController.instance.IsGameProgress)
            RotateWorld();
    }

    // -------------------- Spawn logic ---------------------
    //To DO pool objects code repeated 
    public void StartSpawnManager(int countEnemySpawn, int skeletonHP, int skeletonAttack)
    {
        ResetSpawnSkeleton();
        // max enemies to spawn in current wave
        maxSpawnInWave = countEnemySpawn;
        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            // start coroutine where enemies randomly spawn while need
            Coroutine waveSpawner = StartCoroutine(SpawnEnemyOnPoint(spawnPoint, skeletonHP, skeletonAttack));
            waveCoroutin.Add(waveSpawner);
        }
    }

    public void ResetSpawnSkeleton()
    {
        // reset all coroutins
        foreach (Coroutine waveSpawner in waveCoroutin)
        {
            if (waveSpawner != null)
                StopCoroutine(waveSpawner);
        }
        currentSpawnInWave = 0;
    }

    IEnumerator SpawnEnemyOnPoint(Transform spawnPoint, int skeletonHP, int skeletonAttack)
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 0.5f));
        // if need spawn skeleton then spawn
        while (currentSpawnInWave < maxSpawnInWave)
        {
            //show fvx spawn point
            spawnPoint.GetComponent<Spawn>().ShowSpawnVFX();
            SpawnSkeleton(spawnPoint.transform.position, skeletonHP, skeletonAttack);
            yield return new WaitForSeconds(Random.Range(5.0f, 8.5f));
        }
    }

    private void SpawnSkeleton(Vector3 spawnPosition, int skeletonHP, int skeletonAttack)
    {
        GameObject skeleton = enemiesPool.Count != 0 ? GetSkeleton() : CreateSkeleton();
        skeleton.transform.position = spawnPosition;
        skeleton.GetComponent<EnemyCombat>().SetSkeletornParams(skeletonHP, skeletonAttack);
        currentSpawnInWave++;
    }

    private GameObject CreateSkeleton()
    {
        GameObject skeleton = Instantiate(skeletonPrefab, enemiesFolder);
        skeleton.name = "Skeleeton_" + enemyPoolCounter;
        ActivateBillboardsEffectInChilder(skeleton);
        enemyPoolCounter++;

        return skeleton;
    }

    private GameObject GetSkeleton()
    {
        GameObject skeleton     = enemiesPool.Dequeue();
        skeleton.SetActive(true);
        Character skeletonChar      = skeleton.GetComponent<Character>();
        AudioSource skeletonAudio   = skeleton.GetComponent<AudioSource>();
        skeletonChar.IsPause    = false;
        skeletonChar.IsDead     = false;
        skeletonAudio.Play();
        return skeleton;
    }


    public GameObject SpawnPotion(Transform spawnPlace)
    {
        GameObject potion = potionPool.Count != 0 ? GetPotion() : CreatePotion();
        potion.GetComponent<HealthPotion>().SpawnPlace = spawnPlace;
        potion.transform.position = spawnPlace.position;
        return potion;
    }

    private GameObject CreatePotion()
    {
        GameObject potion = Instantiate(potionPrefab, potionFolder);
        potion.name = "Potion_" + enemyPoolCounter;
        ActivateBillboardsEffectInChilder(potion);
        potionPoolCounter++;

        return potion;
    }

    private GameObject GetPotion()
    {
        GameObject potion = potionPool.Dequeue();
        potion.SetActive(true);
        return potion;
    }


    public void OnEnemieDie(GameObject enemieDestoryObject)
    {
        enemieDestoryObject.SetActive(false);
        enemieDestoryObject.transform.position = Vector3.zero;
        enemiesPool.Enqueue(enemieDestoryObject);
    }

    public void CreateDeathBody(Sprite deathSprite, Vector3 diePos, float xScale)
    {
        GameObject enemyBody = Instantiate(emptyBody, diePos, Quaternion.identity, emptyBodyFolder);
        enemyBody.name = "deadBody_"+deathBodys.Count;
        enemyBody.GetComponent<SpriteRenderer>().sprite = deathSprite;
        enemyBody.AddComponent<Billboards>();
        enemyBody.transform.localScale = new Vector3(xScale,1,1);
        deathBodys.Add(enemyBody);
    }

    // -------------------- Rotate logic --------------------
    private void RotateWorld()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotatePlayer(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotatePlayer(false);
        }
    }

    private void RotatePlayer(bool isClockWise)
    {
        if (isRotate)
            return;

        float angle = isClockWise ? rotateStep : rotateStep * -1;
        Vector3 eulerAngleStep = new Vector3(0, angle, 0);

        Quaternion startRotatetion = player.transform.rotation;
        Quaternion finalRotation = Quaternion.Euler(player.transform.eulerAngles + eulerAngleStep);

        StartCoroutine(RotateOverTime(startRotatetion, finalRotation, rotateTime));
    }

    IEnumerator RotateOverTime(Quaternion a, Quaternion b, float rTime)
    {
        isRotate = true;
        float t     = 0.0f;
        float step  = 1.0f / rTime;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * step;
            float moveStep = rotateCurve.Evaluate(t);
            player.transform.rotation = Quaternion.Slerp(a, b, moveStep);

            yield return null;
        }
        isRotate = false;
    }


    private void ActivateBillboardsEffectInChilder(GameObject gameObject)
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "Billboard")
            {
                child.gameObject.AddComponent<Billboards>();
            }
        }
    }

    private void ActivateBillboardsEffect()
    {
        billboardSprites = GameObject.FindGameObjectsWithTag("Billboard");
        foreach (GameObject spriteObject in billboardSprites)
        {
            spriteObject.AddComponent<Billboards>();
        }
    }

    // ------------------------------------------------------------ 
}
