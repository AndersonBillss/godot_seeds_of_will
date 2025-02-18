using System.Collections.Generic;
using Godot;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class AttackOneHand1: PlayerAnimationHandler{
    private AnimatedSprite2D _selectedWeaponSprites;
    private Sprite2D _selectedWeaponSlash;

    public AttackOneHand1(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg) 
        : base(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg) { }

    public override AttackOneHand1 Init(){
        SetAnimation(new List<AnimationLink>(){head, body, rightArm, leftArm, rightLeg, leftLeg});
        repeatAnimation = false;
        allowInterruption = false;
        restartTimer = true;
        animationLength = 3;
        return this;
    }

    public override void Start(){
        if (_selectedWeaponSprites == null || _selectedWeaponSlash == null) {
            throw new System.Exception("Weapon sprites not assigned in AttackOneHand1");
        } 
        base.Start();
        _selectedWeaponSprites.Visible = true;
        MapLocation(_selectedWeaponSprites, new float[3]{-29, 46, 155});
        _selectedWeaponSprites.Frame = 0;
    }
    public override void NextFrame(int n){
        base.NextFrame(n);
        if(n == 1){
            _selectedWeaponSlash.Visible = true;
            MapLocation(_selectedWeaponSprites, new float[3]{-13, -46, -90});
            _selectedWeaponSprites.Frame = 2;
        } else if(n == 2){
            _selectedWeaponSlash.Visible = false;
            MapLocation(_selectedWeaponSprites, new float[3]{19, -41, -38});
            _selectedWeaponSprites.Frame = 1;
        }
    }
    public override void End(){
        base.End();
        _selectedWeaponSlash.Visible = false;
        _selectedWeaponSprites.Visible = false;
        _selectedWeaponSprites.Frame = 0;
    }

    public AttackOneHand1 SetWeapon(AnimatedSprite2D selectedWeaponSprites, Sprite2D selectedWeaponSlash){
        _selectedWeaponSprites = selectedWeaponSprites;
        _selectedWeaponSlash = selectedWeaponSlash;
        return this;
    }
}