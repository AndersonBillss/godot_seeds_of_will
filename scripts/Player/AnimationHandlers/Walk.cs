using System.Collections.Generic;
using Utils.Animation;

namespace Player.AnimationHandlers;

class Walk : AnimationHandlerBase
{
    public Walk(string animationName) : base(animationName) { }

    public override Walk Init(){
        syncAnimation = false;
        animationLength = rightLeg.GetFrameCount("walk");
        SetAnimation(new List<AnimationLink>(){rightLeg, leftLeg});
        SetAnimation(new List<AnimationLink>(){head, rightArm, leftArm}, "default");
        SetAnimation(new List<AnimationLink>(){body}, "idle");
        return this;
    }
}
