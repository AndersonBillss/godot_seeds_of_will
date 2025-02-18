using System.Collections.Generic;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Run : PlayerAnimationHandler
{
    public Run(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg) 
        : base(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg) { }
    public override Run Init(){
        animationLength = head.GetFrameCount("run");
        SetAnimation(new List<AnimationLink>(){head, body, rightArm, leftArm, rightLeg, leftLeg});
        return this;
    }
}