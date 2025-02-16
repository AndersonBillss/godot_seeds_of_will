using Godot;

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
        return this;
    }

    public override void Start(){
        rightLeg.SetAnimation("attack_one_hand_1");
        leftLeg.SetAnimation("attack_one_hand_1");
        rightArm.SetAnimation("attack_one_hand_1");
        leftArm.SetAnimation("attack_one_hand_1");
        head.SetAnimation("attack_one_hand_1");
        body.SetAnimation("attack_one_hand_1");
        
        _selectedWeaponSprites.Visible = true;
        MapLocation(_selectedWeaponSprites, new float[3]{-29, 46, 155});
        _selectedWeaponSprites.Frame = 0;
    }
    public AttackOneHand1 AddWeapon(AnimatedSprite2D selectedWeaponSprites, Sprite2D selectedWeaponSlash){
        _selectedWeaponSprites = selectedWeaponSprites;
        _selectedWeaponSlash = selectedWeaponSlash;
        return this;
    }
    public override void NextFrame(int n)
    {
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
        _selectedWeaponSlash.Visible = false;
        _selectedWeaponSprites.Visible = false;
        _selectedWeaponSprites.Frame = 0;
    }
}