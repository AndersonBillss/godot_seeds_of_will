using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Jump(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) : PlayerAnimationHandler(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties)
{
    public override Jump Init(){
        restartTimer = true;
        repeatAnimation = false;
        animationLength = rightLeg.GetFrameCount("jump");
        SetAnimation([rightLeg, leftLeg, rightArm, leftArm, head, body]);
        return this;
    }
    public override void NextFrame(int n){
        base.NextFrame(n);
        if(n == 1){
            MapLocation(rightArm.animatedSprite, [2,0,0]);
            rightLeg.SetRotation(10);
            leftLeg.SetRotation(-2);
        }
        if(n == 2){
            rightArm.SetRotation(-10);
            rightLeg.SetRotation(15);
            leftLeg.SetRotation(-5);
        }
    }
    public override void End(){
        base.End();
        rightLeg.SetRotation(0);
        leftLeg.SetRotation(0);
        MapLocation(rightArm.animatedSprite, [0,0,0]);
    }
}
