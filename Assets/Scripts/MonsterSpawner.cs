using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject monster;

    private void Awake()
    {
        monster.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool CheckMonster()
    {
        return monster.activeSelf;
    }

    public void SpawnMonster()
    {
        monster.SetActive(true);
    }

    public void KillMonster()
    {
        monster.SetActive(false);
    }
}
