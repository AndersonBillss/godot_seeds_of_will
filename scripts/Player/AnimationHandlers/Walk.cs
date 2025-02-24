using System.Collections.Generic;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Walk : PlayerAnimationHandler
{
  public Walk(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) 
        : base(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties) { }

    public override Walk Init(){
        syncAnimation = false;
        animationLength = rightLeg.GetFrameCount("walk");
        SetAnimation(new List<AnimationLink>(){rightLeg, leftLeg});
        SetAnimation(new List<AnimationLink>(){head, rightArm, leftArm}, "default");
        SetAnimation(new List<AnimationLink>(){body}, "idle");
        return this;
    }
}
