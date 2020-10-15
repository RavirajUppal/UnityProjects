using UnityEngine;

public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
{
  private static T instance;
  public static T Instance
  {
    get
    {
      if (instance != null) { return instance; }
      else if (GameObject.FindObjectOfType(typeof(T)) != null) { return instance = (T)GameObject.FindObjectOfType(typeof(T)); }
      else { Debug.Log("Failed to get GameManager"); return null; }
    }
    private set { }
  }

  protected State CurrentState
  {
    get;
    private set;
  }
  private State nextState;

  protected virtual void Awake()
  {
    if (instance == null)
    {
      if (GameObject.FindObjectOfType(typeof(T)) != null)
        instance = (T)GameObject.FindObjectOfType(typeof(T));
    }
  }

  protected virtual void Update()
  {
    if (CurrentState != null)
    {
      CurrentState.Update();
    }
  }

  protected virtual void FixedUpdate()
  {
    if (CurrentState != null)
    {
      CurrentState.FixedUpdate();
    }
  }

  protected virtual void LateUpdate()
  {
    if (CurrentState != null)
    {
      CurrentState.LateUpdate();
    }
  }

  public virtual void AddState(State nextState)
  {
    this.nextState = nextState;
    if (nextState != null)
    {
      if (CurrentState != null)
      {
        CurrentState.OnStateExit(InitializeState);
        return;
      }
      InitializeState();
    }
  }

  private void InitializeState()
  {
    CurrentState = null;
    if (nextState != null)
    {
      CurrentState = nextState;
      CurrentState.OnInitialize();
    }
    nextState = null;
  }

  protected virtual void Destroy()
  {
    if (instance != null)
    {
      if (CurrentState != null)
      {
        CurrentState.OnStateExit();
      }
      instance = null;
    }
  }
}