using System.Collections.Generic;
using Godot;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class AttackOneHand1: PlayerAnimationHandler{
    private AnimatedSprite2D _selectedWeaponSprites;
    private Sprite2D _selectedWeaponSlash;

    public AttackOneHand1(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) 
        : base(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties) { }

    public override AttackOneHand1 Init(){
        SetAnimation(new List<AnimationLink>(){head, body, rightArm, leftArm, rightLeg, leftLeg});
        repeatAnimation = false;
        allowInterruption = false;
        restartTimer = true;
        animationLength = rightArm.GetFrameCount("attack_one_hand_1");
        return this;
    }

    public override void Start(){
        _selectedWeaponSlash = sharedAnimationProperties.selectedWeaponSlash;
        _selectedWeaponSprites = sharedAnimationProperties.selectedWeaponSprites;
        if (_selectedWeaponSprites == null || _selectedWeaponSlash == null) {
            throw new System.Exception("Weapon sprites not assigned in AttackOneHand1");
        } 
        base.Start();
        _selectedWeaponSprites.Visible = true;
        MapLocation(_selectedWeaponSprites, new float[3]{-28, 44, 155});
        _selectedWeaponSprites.Frame = 0;
    }
    public override void NextFrame(int n){
        base.NextFrame(n);
        if(n == 1){
            _selectedWeaponSlash.Visible = true;
            MapLocation(_selectedWeaponSprites, new float[3]{26, -38, -40});
            _selectedWeaponSprites.Frame = 1;
        } else if(n == 2){
            head.animatedSprite.ZIndex = 30;
            body.animatedSprite.ZIndex = 30;
            rightArm.animatedSprite.ZIndex = 30;
            _selectedWeaponSlash.Visible = false;
            MapLocation(_selectedWeaponSprites, new float[3]{-26, -32, -118});
            _selectedWeaponSprites.Frame = 2;
        } else if(n == 3){
            _selectedWeaponSlash.Visible = false;
            MapLocation(_selectedWeaponSprites, new float[3]{-14, -43, -95});
            _selectedWeaponSprites.Frame = 1;
        } else if(n == 4){
            _selectedWeaponSlash.Visible = false;
            MapLocation(_selectedWeaponSprites, new float[3]{16, -41, -43});
            _selectedWeaponSprites.Frame = 1;
        }
    }
    public override void End(){
        base.End();
        head.animatedSprite.ZIndex = 0;
        body.animatedSprite.ZIndex = 0;
        rightArm.animatedSprite.ZIndex = 0;
        _selectedWeaponSlash.Visible = false;
        _selectedWeaponSprites.Visible = false;
        _selectedWeaponSprites.Frame = 0;
        _selectedWeaponSprites.ZIndex = 0;
    }
}