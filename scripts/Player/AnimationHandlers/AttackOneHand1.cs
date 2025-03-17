using Godot;
using scripts.Utils.Animation;

namespace scripts.Player.AnimationHandlers;

class AttackOneHand1(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) : PlayerAnimationHandler(animationName, head, body, rightArm, leftArm, rightLeg, leftLeg, sharedAnimationProperties){
    private AnimatedSprite2D _selectedWeaponSprites;
    private AnimatedSprite2D _selectedWeaponSlashSprites;

    public override AttackOneHand1 Init(){
        SetAnimation([head, body, rightArm, leftArm, rightLeg, leftLeg]);
        repeatAnimation = false;
        allowInterruption = false;
        restartTimer = true;
        syncAnimation = false; // Change this to false so that running animation continues
        animationLength = rightArm.GetFrameCount("attack_one_hand_1");
        return this;
    }

    public override void Start(){
        _selectedWeaponSlashSprites = sharedAnimationProperties.selectedWeaponSlashSprites;
        _selectedWeaponSprites = sharedAnimationProperties.selectedWeaponSprites;
        if (_selectedWeaponSprites == null || _selectedWeaponSlashSprites == null) {
            throw new System.Exception("Weapon sprites not assigned in AttackOneHand1");
        } 
        _selectedWeaponSprites.Visible = true;
        MapLocation(_selectedWeaponSprites, [-28, 44, 155]);
        _selectedWeaponSprites.Frame = 0;
        _selectedWeaponSlashSprites.Frame = 0;
        rightLeg.animatedSprite.ZIndex = 10;
        // If the player is running, keep playing the running animation
        if(sharedAnimationProperties.running){
            ChangeAnimation(rightLeg, "run");
            ChangeAnimation(leftLeg, "run");
        } else {
            ChangeAnimation(rightLeg);
            ChangeAnimation(leftLeg);        
        }
        base.Start();
        if(sharedAnimationProperties.running){
            rightLeg.SetFrame(2);
            leftLeg.SetFrame(2);
        }
    }
    public override void NextFrame(int n){
        base.NextFrame(n);
        if(n == 1){
            _selectedWeaponSlashSprites.Visible = true;
            MapLocation(_selectedWeaponSprites, [26, -38, -40]);
            _selectedWeaponSprites.Frame = 1;
        } else if(n == 2){
            head.animatedSprite.ZIndex = 30;
            body.animatedSprite.ZIndex = 30;
            rightArm.animatedSprite.ZIndex = 30;
            _selectedWeaponSlashSprites.Visible = true;
            MapLocation(_selectedWeaponSprites, [-26, -32, -118]);
            _selectedWeaponSprites.Frame = 2;
            _selectedWeaponSlashSprites.Frame = 1;
        } else if(n == 3){
            _selectedWeaponSlashSprites.Visible = false;
            MapLocation(_selectedWeaponSprites, [-14, -43, -95]);
            _selectedWeaponSprites.Frame = 1;
        } else if(n == 4){
            _selectedWeaponSlashSprites.Visible = false;
            MapLocation(_selectedWeaponSprites, [16, -41, -43]);
            _selectedWeaponSprites.Frame = 1;
        }
    }
    public override void End(){
        base.End();
        head.animatedSprite.ZIndex = 0;
        body.animatedSprite.ZIndex = 0;
        rightArm.animatedSprite.ZIndex = 0;
        _selectedWeaponSlashSprites.Visible = false;
        _selectedWeaponSlashSprites.Frame = 0;
        _selectedWeaponSprites.Visible = false;
        _selectedWeaponSprites.Frame = 0;
        _selectedWeaponSprites.ZIndex = 0;
        rightLeg.animatedSprite.ZIndex = 0;
    }
}