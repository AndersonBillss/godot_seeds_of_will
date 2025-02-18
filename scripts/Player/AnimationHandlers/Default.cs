using System.Collections.Generic;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Default : PlayerAnimationHandler
{
    public Default(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg) 
        : base(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg) { }

    public override AnimationHandler Init(){
        animationLength = body.GetFrameCount("idle");
        SetAnimation(new List<AnimationLink>(){body}, "idle");
        SetAnimation(new List<AnimationLink>(){head, rightArm, leftArm, rightLeg, leftLeg});
        return this;
    }
}