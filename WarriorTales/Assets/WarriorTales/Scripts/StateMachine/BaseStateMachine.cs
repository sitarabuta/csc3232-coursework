using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    private BaseState currentState;

    void Start()
    {
        currentState = GetInitialState();
        currentState?.Enter();
    }

    void Update()
    {
        currentState?.UpdateState();
    }


    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }
}