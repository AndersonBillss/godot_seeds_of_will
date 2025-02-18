using System.Collections.Generic;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Fall : PlayerAnimationHandler
{
    public Fall(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg) 
        : base(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg) { }

    public override Fall Init(){
        restartTimer = true;
        repeatAnimation = false;
        animationLength = rightLeg.GetFrameCount("fall");
        SetAnimation(new List<AnimationLink>{rightLeg, leftLeg});
        SetAnimation(new List<AnimationLink>{rightArm, leftArm, head, body}, "run");
        return this;
    }
}
