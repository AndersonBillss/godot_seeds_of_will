using System.Collections.Generic;
using Utils.Animation;

namespace Player.AnimationHandlers;

class Jump : AnimationHandlerBase
{
    public Jump(string animationName) : base(animationName) { }

    public override Jump Init(){
        restartTimer = true;
        repeatAnimation = false;
        animationLength = rightLeg.GetFrameCount("jump");
        SetAnimation(new List<AnimationLink>{rightLeg, leftLeg});
        SetAnimation(new List<AnimationLink>{rightArm, leftArm, head, body}, "run");
        return this;
    }
}
