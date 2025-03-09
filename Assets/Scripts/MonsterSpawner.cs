using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monster;
    private int monsterType;
    private int level;
    [SerializeField]
    private GameObject bossHpBar;
    [SerializeField]
    private GameObject hpBar;
    private int aliveMonster = -1;

    private void Awake()
    {
        SpawnMonsterByLevel();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool CheckMonster()
    {
        bool isAlive = false;
        foreach (GameObject mon in monster)
        {
            if(mon.activeSelf)
            {
                isAlive = true;
            }
        }
        return isAlive;
    }

    public void SpawnMonster(int type)
    {
        monster[type].SetActive(true);
    }

    public void KillMonster()
    {
        foreach (GameObject alive in monster)   
        {
            if(alive.activeSelf)
            {
                alive.SetActive(false);
            }
        }
        aliveMonster = -1;
    }

    public void SpawnMonsterByLevel()
    {
        monsterType = Random.Range(0, 4);
        SpawnMonster(monsterType);
        aliveMonster = monsterType;
    }

    public void UpdateMonster()
    {
        int goalScore = TurnCounting.Instance.goalScore;
        int lastBoss = TurnCounting.Instance.lastBoss;
        int limitTurn = TurnCounting.Instance.limitTurn;
        int turnCount = TurnCounting.Instance.turnCount;
        int firstLimitTurn = TurnCounting.Instance.firstLimitTurn;
        int turnScore = TurnCounting.Instance.turnScore;
        if (turnScore >= goalScore)
        {
            KillMonster();
            bossHpBar.SetActive(false);
            hpBar.SetActive(false);
        }
        else
        {
            if(monsterType == -1)
            {
                SpawnMonsterByLevel();
            }
            bossHpBar.SetActive(true);
            hpBar.SetActive(true);

            bossHpBar.GetComponent<HpBarControll>().SetHp(goalScore - turnScore, goalScore);
            hpBar.GetComponent<HpBarControll>().SetHp(limitTurn - turnCount, firstLimitTurn);
        }
    }
}
