using System.Collections.Generic;
using Godot;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class Run(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) : PlayerAnimationHandler(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties)
{
    private AnimatedSprite2D _selectedWeaponSprites;
    private readonly float[] _rightArmWeaponPosition1 = [13,3,-25];
    private readonly float[] _rightArmWeaponPosition2 = [13,0,-27];
    private readonly float[] _rightArmReturnPosition = [0,0,0];
    private readonly float[] _weaponPosition1 = [32,-23,-25];
    private readonly float[] _weaponPosition2 = [32,-26,-27];
    private readonly float[] _weaponReturnPosition = [0,0,0];

    public override Run Init(){
        animationLength = head.GetFrameCount("run");
        SetAnimation([head, body, rightArm, leftArm, rightLeg, leftLeg]);
        _selectedWeaponSprites = sharedAnimationProperties.selectedWeaponSprites;
        return this;
    }

    public override void Start(){
        if(sharedAnimationProperties.holdingWeapon){
            ChangeAnimation(rightArm, "holding_one_hand_weapon");
            MapLocation(rightArm.animatedSprite, _rightArmWeaponPosition1);
            MapLocation(_selectedWeaponSprites, _weaponPosition1);
            _selectedWeaponSprites.Frame = 0;
            _selectedWeaponSprites.Visible = true;
        } else {
            ChangeAnimation(rightArm, "run");
        }
        base.Start();
    }

    public override void NextFrame(int n)
    {
        base.NextFrame(n);
        if(!sharedAnimationProperties.holdingWeapon){
            return;
        }
        if(n%4 < 2){
            MapLocation(rightArm.animatedSprite, _rightArmWeaponPosition1);
            MapLocation(_selectedWeaponSprites, _weaponPosition1);
        } else {
            MapLocation(rightArm.animatedSprite, _rightArmWeaponPosition2);
            MapLocation(_selectedWeaponSprites, _weaponPosition2);

        }
    }

    public override void End(){
        base.End();
        MapLocation(rightArm.animatedSprite, _rightArmReturnPosition);
        MapLocation(_selectedWeaponSprites, _weaponReturnPosition);
        _selectedWeaponSprites.Visible = false;
    }
}