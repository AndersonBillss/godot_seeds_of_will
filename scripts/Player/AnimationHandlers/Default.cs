using Godot;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Default(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) : PlayerAnimationHandler(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties)
{
    private AnimatedSprite2D _selectedWeaponSprites;
    private readonly float[] WeaponPosition = [27,-16,-2];
    private readonly float[] WeaponReturnPosition = [0,0,0];

    public override AnimationHandler Init(){
        animationLength = body.GetFrameCount("idle");
        SetAnimation([body], "idle");
        SetAnimation([head, rightArm, leftArm, rightLeg, leftLeg]);
        _selectedWeaponSprites = sharedAnimationProperties.selectedWeaponSprites;
        return this;
    }

    public override void Start(){
        if(sharedAnimationProperties.holdingWeapon){
            ChangeAnimation(rightArm, "holding_one_hand_weapon");
            _selectedWeaponSprites.Visible = true;
            MapLocation(_selectedWeaponSprites, WeaponPosition);
            _selectedWeaponSprites.Frame = 0;
        } else {
            ChangeAnimation(rightArm, "default");
        }
        base.Start();
    }
    public override void End(){
        base.End();
        MapLocation(_selectedWeaponSprites, WeaponReturnPosition);
        _selectedWeaponSprites.Visible = false;
    }
}