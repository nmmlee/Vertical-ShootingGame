using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

    // 오브젝트 풀링? : 오브젝트가 담겨있는 배열에서 true,false로 생성하는 것.
    // Instantiate와 Destroy가 실행되는 동안에는 가비지 메모리가 많이 발생하고, 이를 없애기 위해 유니티 내에서 가비지 컬렉터가 실행되면 게임이 끊김.

    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;

    public GameObject enemyBPrefab;

    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;

    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;


    public GameObject bulletFollowerPrefab;

    public GameObject explosionPrefab;


    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;

    GameObject[] enemyB;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;

    GameObject[] bulletFollower;

    GameObject[] explosion;

    GameObject[] targetPool; // 내보낼 오브젝트들을 담은 공간. MakeObj함수 참조

    void Awake()
    {
        enemyL = new GameObject[100];
        enemyM = new GameObject[100];
        enemyS = new GameObject[100];

        enemyB = new GameObject[100];

        itemCoin = new GameObject[100];
        itemPower = new GameObject[100];
        itemBoom = new GameObject[100];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];

        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];

        bulletBossA = new GameObject[100];
        bulletBossB = new GameObject[1000];


        bulletFollower = new GameObject[100];
        explosion = new GameObject[100];

        // 로딩 시간은 = 장면 배치 + 오브젝트 풀 생성.
        Generate();
    }

    void Generate()
    {
        // #1. Enemy
        for(int index = 0; index < enemyL.Length; index++)
        {
            enemyL[index] = Instantiate(enemyLPrefab);
            enemyL[index].SetActive(false);
        }

        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPrefab);
            enemyM[index].SetActive(false);
        }

        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPrefab);
            enemyS[index].SetActive(false);
        }

        for (int index = 0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPrefab);
            enemyB[index].SetActive(false);
        }

        // #2. Item
        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPrefab);
            itemCoin[index].SetActive(false);
        }

        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPrefab);
            itemPower[index].SetActive(false);
        }

        for (int index = 0; index < itemBoom.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPrefab);
            itemBoom[index].SetActive(false);
        }

        // #3. Bullet
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[index].SetActive(false);
        }

        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            bulletEnemyA[index].SetActive(false);
        }

        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            bulletEnemyB[index].SetActive(false);
        }

        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPrefab);
            bulletBossA[index].SetActive(false);
        }

        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBPrefab);
            bulletBossB[index].SetActive(false);
        }

        for (int index = 0; index < bulletFollower.Length; index++)
        {
            bulletFollower[index] = Instantiate(bulletFollowerPrefab);
            bulletFollower[index].SetActive(false);
        }

        for(int index = 0; index < explosion.Length; index++)
        {
            explosion[index] = Instantiate(explosionPrefab);
            explosion[index].SetActive(false);
        }
    }

    // 풀에서 게임 오브젝트를 생성해야 하기 때문에 반환값은 GameObject
    public GameObject MakeObj(string type)
    {

        switch(type)
        {
            case "EnemyL":
                targetPool = enemyL;
                break;

            case "EnemyM":
                targetPool = enemyM;
                break;

            case "EnemyS":
                targetPool = enemyS;
                break;

            case "EnemyB":
                targetPool = enemyB;
                break;

            case "ItemCoin":
                targetPool = itemCoin;
                break;

            case "ItemPower":
                targetPool = itemPower;
                break;

            case "ItemBoom":
                targetPool = itemBoom;
                break;

            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;

            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;

            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;

            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;

            case "BulletBossA":
                targetPool = bulletBossA;
                break;

            case "BulletBossB":
                targetPool = bulletBossB;
                break;

            case "BulletFollower":
                targetPool = bulletFollower;
                break;

            case "Explosion":
                targetPool = explosion;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf) // activeSelf는 참인지 트루인지 찾음
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null; // 없으면 빈 것 제공 + 반환값이 존재해야 하기에 넣음.
    }
    
    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyL":
                targetPool = enemyL;
                break;

            case "EnemyM":
                targetPool = enemyM;
                break;

            case "EnemyS":
                targetPool = enemyS;
                break;

            case "EnemyB":
                targetPool = enemyB;
                break;

            case "ItemCoin":
                targetPool = itemCoin;
                break;

            case "ItemPower":
                targetPool = itemPower;
                break;

            case "ItemBoom":
                targetPool = itemBoom;
                break;

            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;

            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;

            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;

            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;

            case "BulletBossA":
                targetPool = bulletBossA;
                break;

            case "BulletBossB":
                targetPool = bulletBossB;
                break;

            case "BulletFollower":
                targetPool = bulletFollower;
                break;

            case "Explosion":
                targetPool = explosion;
                break;
        }

        return targetPool;
    }
}
