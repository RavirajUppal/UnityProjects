using UnityEngine;

public abstract class State
{
  protected delegate void SubState();
  public delegate void CallBack();
  private event CallBack OnCallBack;
  protected bool isInitialized;
  protected MonoBehaviour monoBehaviour;

  public State(MonoBehaviour mono)
  {
    monoBehaviour = mono;
  }

  public virtual void OnInitialize()
  {
    isInitialized = true;
  }

  public virtual void Update()
  {

  }

  public virtual void FixedUpdate()
  {

  }

  public virtual void LateUpdate()
  {

  }

  public virtual void OnStateExit(CallBack callBack = null)
  {
    OnCallBack = callBack;
    Destroy();
  }

  protected virtual void Destroy()
  {
    if (OnCallBack != null)
    {
      OnCallBack();
    }
    OnCallBack = null;
  }
}

