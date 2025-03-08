using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TurnCounting : MonoBehaviour
{
    //�̱�������
    public static TurnCounting Instance;

    [SerializeField]
    LevelUpEffect levelUpEffect;
    public int turnCount;
    public int limitTurn;
    private int firstLimitTurn;
    public int goalScore;
    private int firstGoalScore;
    private int increaseMultiplier = 2;
    private GameObject bossHpBar;
    private GameObject hpBar;
    private GameObject monsterSpawner;
    private GameObject boardInventory;
    private int lastBoss;
    private bool isMonsterAlive;

    private int level = 1;

    [SerializeField] private TextMeshProUGUI limitTurnText;
    [SerializeField] private TextMeshProUGUI goalScoreText;

    private void Awake()
    {
        // �̱��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ���� �� �������� �ʵ��� ����
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // �ߺ� ����
        }

        firstLimitTurn = limitTurn;
        firstGoalScore = goalScore;
        bossHpBar = GameObject.Find("BossHpBar");
        hpBar = GameObject.Find("HpBar");
        monsterSpawner = GameObject.Find("MonsterManager");

        UpdateText();
    }

    // testcode
    /*private void Update()
    {
        if(Input.GetKeyDown("x")){
            levelUpEffect.CrackerShoot(level);
            //level++;
        }
    }*/

    // ���� �ε�� �� ���� �ʱ�ȭ
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignUIElements(); // �ؽ�Ʈ�������� ��� �Ҵ�
        ResetVariables(); // ������ �⺻������ �ʱ�ȭ
        UpdateText();
    }

    // ���� �ʱ�ȭ �޼���
    private void ResetVariables()
    {
        turnCount = 0;
        level = 1;
        limitTurn = firstLimitTurn;
        goalScore = firstGoalScore;
        increaseMultiplier = 2;
        bossHpBar = GameObject.Find("BossHpBar");
        hpBar = GameObject.Find("HpBar");
        monsterSpawner = GameObject.Find("MonsterManager");
        boardInventory = GameObject.Find("BoardInventory");
        lastBoss = 0;
        isMonsterAlive = true;
    }

    private void AssignUIElements()
    {
        limitTurnText = GameObject.Find("TurnText")?.GetComponent<TextMeshProUGUI>();
        goalScoreText = GameObject.Find("GoalText")?.GetComponent<TextMeshProUGUI>();
    }

    public void CheckTrunAndGoal()
    {
        UpdateText();
        
        if (turnCount >= limitTurn)
        {
            if(BoardCheck.score < goalScore)
            {
                //game over
                BoardCheck.gameover = true;
            }
            else
            {
                //����
                limitTurn += firstLimitTurn;
                lastBoss += goalScore;
                goalScore += firstGoalScore * increaseMultiplier;
                if (increaseMultiplier < 30)
                {
                    increaseMultiplier += 1;
                }

                levelUpEffect.CrackerShoot(level);
                level++;
                SoundManager.Instance.PlayLevelUpSound();

                

            }
        }
    }

    //�ؽ�Ʈ ����
    private void UpdateText()
    {
        limitTurnText.text = "Turn : " + turnCount + " / " + limitTurn;
        goalScoreText.text = "Goal : " + goalScore;
        if(BoardCheck.score >= goalScore)
        {
            /*if(isMonsterAlive == true)
            {
                boardInventory.transform.Translate(178, 0, 0);
            }*/
            if (monsterSpawner.GetComponent<MonsterSpawner>().CheckMonster())
            {
                monsterSpawner.GetComponent<MonsterSpawner>().KillMonster();
            }
            bossHpBar.SetActive(false);
            hpBar.SetActive(false);
            isMonsterAlive = false;
        }
        else
        {
            /*if(isMonsterAlive == false)
            {
                boardInventory.transform.Translate(-178, 0, 0);
            }*/

            monsterSpawner.GetComponent<MonsterSpawner>().SpawnMonster();
            bossHpBar.SetActive(true);
            hpBar.SetActive(true);
            isMonsterAlive = true;
            
            bossHpBar.GetComponent<HpBarControll>().SetHp(goalScore - BoardCheck.score, goalScore - lastBoss);
            hpBar.GetComponent<HpBarControll>().SetHp(limitTurn - turnCount, firstLimitTurn);
        }
            

    }
}
