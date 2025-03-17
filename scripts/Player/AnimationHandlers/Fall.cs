using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Fall(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) : PlayerAnimationHandler(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties)
{
    public override Fall Init(){
        restartTimer = true;
        repeatAnimation = true;
        animationLength = rightLeg.GetFrameCount("fall");
        SetAnimation([rightLeg, leftLeg, rightArm, leftArm, head, body]);
        return this;
    }

    public override void Start(){
        base.Start();
        rightLeg.SetRotation(20);
        body.SetRotation(5);
        head.SetRotation(-4);
        MapLocation(rightArm.animatedSprite, [5,1,-20]);
        MapLocation(leftLeg.animatedSprite, [1,4,-8]);
    }
    public override void NextFrame(int n){
        base.NextFrame(n);
    }
    public override void End(){
        base.End();
        body.SetRotation(0);
        head.SetRotation(0);
        rightLeg.SetRotation(0);
        rightArm.SetRotation(0);
        MapLocation(rightArm.animatedSprite, [0,0,0]);
        MapLocation(leftLeg.animatedSprite, [0,0,0]);
    }
}
