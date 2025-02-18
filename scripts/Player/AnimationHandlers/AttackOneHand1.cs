using System.Collections.Generic;
using Godot;
using Utils.Animation;

namespace Player.AnimationHandlers;

class AttackOneHand1: AnimationHandlerBase{
    private AnimatedSprite2D _selectedWeaponSprites;
    private Sprite2D _selectedWeaponSlash;

    public AttackOneHand1(string animationName) : base(animationName){}
    public override AttackOneHand1 Init(){
        repeatAnimation = false;
        allowInterruption = false;
        restartTimer = true;
        animationLength = 3;
        SetAnimation(new List<AnimationLink>(){head, body, rightArm, leftArm, rightLeg, leftLeg});
        return this;
    }

    public override void Start(){
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