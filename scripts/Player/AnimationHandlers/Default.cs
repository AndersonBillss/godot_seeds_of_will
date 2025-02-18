using System.Collections.Generic;
using Godot;
using Utils.Animation;
namespace Player.AnimationHandlers;

class Default : AnimationHandlerBase
{
    public Default(string animationName) : base(animationName){ }

    public override AnimationHandler Init(){
        animationLength = body.GetFrameCount("idle");
        SetAnimation(new List<AnimationLink>(){body}, "idle");
        SetAnimation(new List<AnimationLink>(){head, rightArm, leftArm, rightLeg, leftLeg});
        return this;
    }
}