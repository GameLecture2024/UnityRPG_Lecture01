using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShakeCamera : TutorialBase
{
    bool isCompleted = false;

    public override void Enter()
    {
        ShakeCamera.Instance.OnShakeCamera();
    }

    public override void Execute(TutorialController controller)
    {
        if (isCompleted) controller.SetNextTutorial();              
    }

    public override void Exit()
    {
       
    }

    
}
