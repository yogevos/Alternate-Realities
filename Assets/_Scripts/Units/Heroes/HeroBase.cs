using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : MonoBehaviour
{
    private bool _canMove;
    private void Awake() => GameManager.OnBeforeStateChanged += OnStateChange;

    private void OnDestroy() => GameManager.OnBeforeStateChanged -= OnStateChange;
    private void OnStateChange(GameManager.GameState newState)
    {
        
    }
}
