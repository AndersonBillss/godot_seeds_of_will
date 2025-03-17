using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Land(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) : PlayerAnimationHandler(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties)
{
    public override Land Init(){
        restartTimer = true;
        repeatAnimation = false;
        allowInterruption = false;
        animationLength = rightLeg.GetFrameCount("land");
        SetAnimation([rightLeg, leftLeg, rightArm, leftArm, head, body]);
        return this;
    }
}