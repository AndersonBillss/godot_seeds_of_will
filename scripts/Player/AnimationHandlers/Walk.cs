using System.Collections.Generic;
using Utils.Animation;

namespace Player.AnimationHandlers;

class Walk : PlayerAnimationHandler
{
  public Walk(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg) 
        : base(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg) { }

    public override Walk Init(){
        syncAnimation = false;
        animationLength = rightLeg.GetFrameCount("walk");
        SetAnimation(new List<AnimationLink>(){rightLeg, leftLeg});
        SetAnimation(new List<AnimationLink>(){head, rightArm, leftArm}, "default");
        SetAnimation(new List<AnimationLink>(){body}, "idle");
        return this;
    }
}
