using System;
using UnityEngine;

public class RealityManager : Singleton<RealityManager> 
{

    public static Action onChangeReality;
    public bool reality = false;

    public float player1Health;
    public float player2Health;

    private void OnEnable()
    {
        TimeManager.OnTimeToChangeReality += ChangeReality;
    }
    private void OnDisable()
    {
        TimeManager.OnTimeToChangeReality -= ChangeReality;
    }
    public void ChangeReality()
    {

        /*
        // if reality = false = Main Reality || if reality = true = Alternate Reality.
        if (!reality)
        {
            reality = true;
        }
        else
        {
            reality = false;
        }
        */

        //switch both player's health with each other

        ///**FixProblem when Passenger dies & other player dead, gives error**
        if(GameManager.Instance.player1Alive && GameManager.Instance.player2Alive) 
        {
            player1Health = GameManager.Instance.player1Health.mainHealth;
            player2Health = GameManager.Instance.player2Health.health;
            GameManager.Instance.player1Health.mainHealth = player2Health;
            GameManager.Instance.player2Health.health = player1Health;
        }
        onChangeReality?.Invoke();
        //Send message to every class that listens to reality changes (e.g player, healthbar UI etc) || (Current listeners: RealityManager, TimeManager, )
        GameManager.Instance.ChangeState(GameManager.GameState.SwitchReality);
    }
}
