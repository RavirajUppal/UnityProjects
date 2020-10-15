using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this to a gameobject
public class GameManager : StateMachine<GameManager>
{

  //Initialize the scene
  protected override void Awake()
  {
    Example1();
    base.Awake();
  }

  //Example Function
  public void Example1()
  {
    AddState(new LevelOneState(this));
  }

  //Do scene related changes
  protected override void Update()
  {
    base.Update();
  }

  //use this to change the state
  public override void AddState(State nextState)
  {
    base.AddState(nextState);
  }

  //call this on quitting the game.
  protected override void Destroy()
  {
    base.Destroy();
  }

}
