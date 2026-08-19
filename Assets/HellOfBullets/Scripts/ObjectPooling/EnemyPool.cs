using System;
using System.Collections.Generic;
using HellOfBullets.Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private List<GameObject> enemyList;
    private int _poolSize = 1000;
    private Transform parent;

    //TODO: Replace with DI injection
    private static EnemyPool instance;
    public static EnemyPool Instance {get{return instance;}}
    private InputSystemActions systemActions;

    private System.Random rand;

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        parent =  new GameObject("[EnemyPool]").transform;
        enemyList =  new();
        systemActions = new();
        systemActions.Player.Attack.Enable();
        systemActions.Player.Attack.performed+=OnPlayerClick;
        rand=new();
    }

    private void OnPlayerClick(InputAction.CallbackContext context)
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject enemy = RequestEnemy();
            enemy.transform.position = new Vector3(rand.Next(-10,10),rand.Next(-10,10),0);
        }
    }

    private void Start()
    {
        AddEnemiesToPool(_poolSize);
    }

    private void AddEnemiesToPool(int poolsize)
    {
        for(int i=0; i<poolsize; i++)
        {
            GameObject enemy = Instantiate(prefab,parent);
            enemy.SetActive(false);
            enemyList.Add(enemy);
        }
        
    }

    public GameObject RequestEnemy()
    {
        for(int i=0; i<enemyList.Count; i++)
        {
            if(!enemyList[i].activeSelf)
            {
                enemyList[i].SetActive(true);
                return enemyList[i];
            }
        }
        return null;
    }
}
