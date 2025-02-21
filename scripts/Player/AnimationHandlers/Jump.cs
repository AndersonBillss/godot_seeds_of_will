using System.Collections.Generic;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Jump : PlayerAnimationHandler
{
    public Jump(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) 
        : base(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties) { }

    public override Jump Init(){
        restartTimer = true;
        repeatAnimation = false;
        animationLength = rightLeg.GetFrameCount("jump");
        SetAnimation(new List<AnimationLink>{rightLeg, leftLeg});
        SetAnimation(new List<AnimationLink>{rightArm, leftArm, head, body}, "run");
        return this;
    }
}
