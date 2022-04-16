using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int stage;
    public Animator stageAnim;
    public Animator clearAnim;
    public Animator fadeAnim;
    public Transform playerPos;

    public string[] enemyObjs; // string�� �����ؾ� �ϱ� ������ ����
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public ObjectManager objectManager;

    public Text scoreText;
    public Image[] lifeImage;
    public Image[] boomImage;
    public GameObject gameOverSet;

    // ������ ���� �ҷ����� ���� �ʿ��� ������
    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[]{ "EnemyL", "EnemyM", "EnemyS", "EnemyB"};
        StageStart();
    }

    public void StageStart()
    {
        // #. Stage UI Load
        stageAnim.SetTrigger("On");
        stageAnim.GetComponent<Text>().text = "Stage " + stage + "\nStart";

        // #. Enemy Spawn File Read
        ReadSpawnFile();

        // #. Fade In
        fadeAnim.SetTrigger("In");
    }
    public void StageEnd()
    {
        // #. Clear UI Load
        clearAnim.SetTrigger("On");
        clearAnim.GetComponent<Text>().text = "Stage " + stage + "\nClear!!";

        // #. Fade Out
        fadeAnim.SetTrigger("Out");

        // #. Player Repos
        playerPos.transform.position = playerPos.position;

        // #. Stage Increase
        stage++;

        if (stage > 2)
            Invoke("GameOver", 6);
        else
            Invoke("StageStart", 3f);


    }

    void ReadSpawnFile()
    {
        // #1. ���� �ʱ�ȭ
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // #2. ������ ���� �о���̱�
        TextAsset textFile = Resources.Load("Stage " + stage.ToString()) as TextAsset; // �ؽ�Ʈ ������ �ƴ� �� �ֱ� ������ as�� ���� ����. �ؽ�Ʈ ������ �ƴ϶�� null ó��.
        StringReader stringReader = new StringReader(textFile.text);


        // ���� ���� �б� ������.
        while(stringReader != null)
        {
            string line = stringReader.ReadLine(); // �ؽ�Ʈ �����͸� �� �پ� �о����.
            Debug.Log(line);

            if (line == null)
                break;

            // # 3. ������ ������ ����
            // �����ڸ� ����, Split�� �̿��ϸ� �迭�� ������ (1 S 1) -> �����̴� �� �߿��� ù��° ���̹Ƿ� [0]
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);

            spawnList.Add(spawnData);
        }

        // # 4. �ؽ�Ʈ ���� ���� �ݱ�
        stringReader.Close();

        // #. ù��° ���� ������ ����
        nextSpawnDelay = spawnList[0].delay;
        
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        // UI update
        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch(spawnList[spawnIndex].type)
        {
            case "L":
                enemyIndex = 0;
                break;

            case "M":
                enemyIndex = 1;
                break;

            case "S":
                enemyIndex = 2;
                break;

            case "B":
                enemyIndex = 3;
                break;
        }

        int enemyPoint = spawnList[spawnIndex].point;

        // ������Ʈ Ǯ ���
        GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);

        // ��ġ�� ���������� ����
        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        

        // �÷��̾� ������ ������Ʈ �Ŵ��� ���� �ѱ��
        enemyLogic.player = player;
        enemyLogic.objectManager = objectManager;
        enemyLogic.gameManager = this;

        if (enemyPoint == 5 || enemyPoint == 6) // #. ������ ����
        {
            enemy.transform.Rotate(Vector3.back * 90);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }

        else if (enemyPoint == 7 || enemyPoint == 8) // #. ���� ����
        {
            enemy.transform.Rotate(Vector3.forward * 90);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }

        else // #. ���� ����
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * -1);
        }

        //#. ������ �ε��� ����
        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        //#. ���� ������ ������ ����
        nextSpawnDelay = spawnList[spawnIndex].delay;


    }

    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }

        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateBoomIcon(int boom)
    {
        for (int index = 0; index < 3; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 0);
        }

        for (int index = 0; index < boom; index++)
        {
            boomImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

        Player playerLogic = player.GetComponent<Player>();
        playerLogic.isHit = false;
    }

    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("Explosion");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }

    public void GameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    
}
