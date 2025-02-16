using System.Collections.Generic;
using Utils.Animation;

namespace Player.AnimationHandlers;

class Run : AnimationHandlerBase
{
    public Run(string animationName) : base(animationName){
    }
    public override Run Init(){
        animationLength = /* head.GetFrameCount("run"); */8;
        AnimateDefault(new List<AnimationLink>(){head, body, rightArm, leftArm, rightLeg, leftLeg});
        return this;
    }
}