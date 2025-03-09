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
    private int nextMonster = 2;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int CheckMonster()
    {
        return aliveMonster;
    }
    public int CheckNextMonster()
    {
        return nextMonster;
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
                if(aliveMonster == 1)
                {
                    TurnCounting.Instance.limitTurn /= 2;
                    TurnCounting.Instance.goalScore /= 10;
                }
            }
        }
        aliveMonster = -1;
        nextMonster = Random.Range(0, 4);
    }

    public void SpawnMonsterByLevel()
    {
        SpawnMonster(nextMonster);
        if (aliveMonster == 3)
        {
            aliveMonster = Random.Range(0, 3);
        }
        aliveMonster = nextMonster;
    }

    public void UpdateMonster()
    {
        int goalScore = TurnCounting.Instance.goalScore;
        int limitTurn = TurnCounting.Instance.limitTurn;
        int turnCount = TurnCounting.Instance.turnCount;
        int turnScore = TurnCounting.Instance.turnScore;
        if (turnScore >= goalScore)
        {
            KillMonster();
            bossHpBar.SetActive(false);
            hpBar.SetActive(false);
            TurnCounting.Instance.turnCount = limitTurn;
        }
        else
        {
            if(aliveMonster == -1)
            {
                SpawnMonsterByLevel();
            }
            bossHpBar.SetActive(true);
            hpBar.SetActive(true);

            bossHpBar.GetComponent<HpBarControll>().SetHp(goalScore - turnScore, goalScore);
            hpBar.GetComponent<HpBarControll>().SetHp(limitTurn - turnCount, limitTurn);
        }
    }
}
