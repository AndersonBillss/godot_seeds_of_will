using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Fall(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) : PlayerAnimationHandler(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties)
{
    public override Fall Init(){
        restartTimer = true;
        repeatAnimation = false;
        animationLength = rightLeg.GetFrameCount("fall");
        SetAnimation([rightLeg, leftLeg]);
        SetAnimation([rightArm, leftArm, head, body], "run");
        return this;
    }
}
