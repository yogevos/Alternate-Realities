using System;
using System.Collections;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    public GameObject player1;
    public GameObject player2;
    [HideInInspector] public bool player1Alive;
    [HideInInspector] public bool player2Alive;
    [HideInInspector] public Health player1Health;
    [HideInInspector] public RegularEnemy player2Health;
    [SerializeField] private GameObject player1Start;
    [SerializeField] private GameObject player2Start;
    //[SerializeField] private GameObject playerStart2;
    //[SerializeField] private Slider mainSlider;
    //[SerializeField] private Slider alternateSlider;
    private GameObject playerCharacter;
    private bool firstTimeBelowHalfHealth = true;

    public GameState State { get; private set; }

    private void Start() => ChangeState(GameState.Init);

    public void ChangeState (GameState newState)
    {
        //if(State == newState) return;

        OnBeforeStateChanged?.Invoke (newState);

        State = newState;

        switch (newState)
        {
            case GameState.Init:
                HandleInit();
                break;

            case GameState.Starting:

                HandleStarting();
                break;

            case GameState.Counting:
                HandleCounting();
                break;

            case GameState.GameRunning:
                HandleGameRunning();
                break;

            case GameState.SwitchReality: 
                HandleSwitchReality();
                break;

            case GameState.buff:
                HandleBuffs();
                break;

            case GameState.Win:
                HandleWin();
                break;

            case GameState.Lose:
                HandleLose();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke (newState);
    }

    
    public void HandleInit()

    {
        player1 = Instantiate(player1, player1Start.transform.position, Quaternion.identity) as GameObject;
        player1Health = player1.GetComponent<Health>();
        player2 = Instantiate(player2, player2Start.transform.position, Quaternion.identity);
        player2Health = player2.GetComponent<RegularEnemy>();

        //player1.GetComponent<playermovement>().canMove = false;
        ChangeState(GameState.Starting);
    }
    public void HandleStarting()
    {
        
        ChangeState(GameState.Counting);
    }
    public void HandleCounting()
    {
        StartCoroutine(CountDown());
    }
    public void HandleGameRunning()
    {

    }
    public void HandleSwitchReality()
    {
        ChangeState(GameState.GameRunning);
    }

    //once 1 player gets under 50% health, sentinel can spawn.
    public void UnderHalfHealth()
    {
        if (firstTimeBelowHalfHealth)
        {
            Debug.Log("One player is under 50% health");
            PawnManager.Instance.SentinelCanSpawn();
            firstTimeBelowHalfHealth = false;
        }
    }
    public void HandleBuffs()
    {
        //Buff lowest hp player. if both at the same hp, none gets a buff.
        if (player1Health.mainHealth > player2Health.health)
        {
            //buff player 2 - player2.Buff(buffType);
            //debuff player 1 - player1.DeBuff(deBuffType);
        }
        else if (player1Health.mainHealth < player2Health.health)
        {
            //buff player 1 - player1.Buff(buffType);
            //debuff player 2 - player2.DeBuff(deBuffType);
        }
        else
        {
            Debug.Log("Neither are buffed since both are at the same HP");
        }
        ChangeState(GameState.GameRunning);
    }
    public void HandleWin()
    {
        //Win
        //Stop Time Manager's coroutines
    }
    public void HandleLose()
    {
        //Lose
        //Stop Time Manager's coroutines
    }


    IEnumerator CountDown()
    {
        Debug.Log("3");
        yield return new WaitForSeconds(1);
        Debug.Log("2");
        yield return new WaitForSeconds(1);
        Debug.Log("1");
        yield return new WaitForSeconds(1);
        //ClearLog();
        //playerCharacter.GetComponent<playermovement>().canMove = true;
        ChangeState(GameState.GameRunning);
    }

    [Serializable]
    public enum GameState
    {
        Init = 0,
        Starting = 1,
        Counting = 2,
        GameRunning = 3,
        SwitchReality = 4, 
        buff = 5,
        Win = 6,
        Lose = 7,

    }

    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

}
