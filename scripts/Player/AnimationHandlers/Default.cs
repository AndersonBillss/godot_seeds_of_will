using System.Collections.Generic;
using Godot;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Default : PlayerAnimationHandler
{
    private AnimatedSprite2D _selectedWeaponSprites;
    private readonly float[] WeaponPosition = new float[3]{27,-16,-2};
    private readonly float[] WeaponReturnPosition = new float[3]{0,0,0};

    public Default(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) 
        : base(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties) { }

    public override AnimationHandler Init(){
        animationLength = body.GetFrameCount("idle");
        SetAnimation(new List<AnimationLink>(){body}, "idle");
        SetAnimation(new List<AnimationLink>(){head, rightArm, leftArm, rightLeg, leftLeg});
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