using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoState : State
{
  public LevelTwoState(MonoBehaviour mono) : base(mono)
  {

  }

  //do stuffs when the state initialized here
  public override void OnInitialize()
  {
    base.OnInitialize();
  }

  //call all functions here
  public override void Update()
  {
    Example1();
    Example2();
    base.Update();
  }

  //Example Function
  public void Example1()
  {

  }

  //Example Function
  public void Example2()
  {
    StateEnd();
  }

  //State End Example Function : create this to end the current state and call next state.
  public void StateEnd()
  {
    if (GameManager.Instance != null)
    {
      //GameManager.Instance.AddState(new LevelThreeState(monoBehaviour));
    }
  }

  //do state exit stuffs here.
  public override void OnStateExit(CallBack callBack = null)
  {
    base.OnStateExit(callBack);
  }
}
