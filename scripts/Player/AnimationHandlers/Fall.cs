using System.Collections.Generic;
using Godot;
using Utils.Animation;

namespace Player.AnimationHandlers;

class Fall : AnimationHandlerBase
{
    public Fall(string animationName) : base(animationName){ }

    public override Fall Init(){
        restartTimer = true;
        repeatAnimation = false;
        animationLength = rightLeg.GetFrameCount("fall");
        SetAnimation(new List<AnimationLink>{rightLeg, leftLeg});
        SetAnimation(new List<AnimationLink>{rightArm, leftArm, head, body}, "run");
        return this;
    }
}
