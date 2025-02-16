using System.Collections.Generic;
using Godot;
using Utils.Animation;
using Player.AnimationHandlers;
using Player;

public partial class PlayerController: CharacterBody2D{

    public AnimationLink head;
    public AnimationLink body;
    public AnimationLink rightArm;
    public AnimationLink leftArm;
    public AnimationLink rightLeg;
    public AnimationLink leftLeg;
    private Timer _animationTimer;
    private Dictionary<string, AnimationHandler> _animationMap;
    private AnimationFactory _playerAnimationFactory;

    public void AnimationInit(){
        _animationTimer = GetNode<Timer>("Timer");

        Node2D headParent = GetNode<Node2D>("CharacterHead");
		Node2D bodyParent = GetNode<Node2D>("CharacterBody");
		Node2D rightArmParent = GetNode<Node2D>("CharacterRightArm");
		Node2D leftArmParent = GetNode<Node2D>("CharacterLeftArm");
		Node2D rightLegParent = GetNode<Node2D>("CharacterRightLeg");
		Node2D leftLegParent = GetNode<Node2D>("CharacterLeftLeg");

        head = new AnimationLink(headParent);
        rightArm = new AnimationLink(rightArmParent);
        leftArm = new AnimationLink(leftArmParent);
        body = new AnimationLink(bodyParent, new AnimationLink[] {
            head, rightArm, leftArm 
        });
        rightLeg = new AnimationLink(rightLegParent);
        leftLeg = new AnimationLink(leftLegParent);

        AnimationCreator animationCreator = new(head, body, rightArm, leftArm, rightLeg, leftLeg);
        _animationMap = new Dictionary<string, AnimationHandler> {
            {"attack_one_hand_1", animationCreator.AddAnimationLinks(new AttackOneHand1("attack_one_hand_1")).AddWeapon(_selectedWeaponSprites, _selectedWeaponSlash).Init()},
            {"default", animationCreator.AddAnimationLinks(new Default("default")).Init()},
            {"fall", animationCreator.AddAnimationLinks(new Fall("fall")).Init()},
            {"jump", animationCreator.AddAnimationLinks(new Jump("jump")).Init()},
            {"run", animationCreator.AddAnimationLinks(new Run("run")).Init()},
            {"walk", animationCreator.AddAnimationLinks(new Walk("walk")).Init()}
        };

        _playerAnimationFactory = new(_animationMap, _animationTimer);
    }

    public void PlayAnimation(string animationName){
        _playerAnimationFactory?.PlayAnimation(animationName);
    }

    // public void PlayAnimation(string animationName){
    //     string prevAnimation = currentAnimation;
    //     if(!allowInterruption){
    //         return;
    //     }
    //     if(animationName == prevAnimation){
    //         return;
    //     }
    //     animationExitFunction?.Invoke();

    //     animationLength = 0;
    //     repeatAnimation = true;
    //     animationFunction = null;
    //     animationExitFunction = null;

    //     if(animationName == "walk"){
    //         rightLeg.SetAnimation("walk");
    //         leftLeg.SetAnimation("walk");
    //         rightArm.SetAnimation("default");
    //         leftArm.SetAnimation("default");
    //         head.SetAnimation("default");
    //         body.SetAnimation("idle");

    //         repeatAnimation = true;
    //     }
    //     if(animationName == "run"){
    //         //start the animation on frame 0
    //         rightLeg.SetAnimation("run");
    //         leftLeg.SetAnimation("run");
    //         rightArm.SetAnimation("run");
    //         leftArm.SetAnimation("run");
    //         head.SetAnimation("run");
    //         body.SetAnimation("run");

    //         repeatAnimation = true;
    //     }
    //     if(animationName == "default"){
    //         rightLeg.SetAnimation("default");
    //         leftLeg.SetAnimation("default");
    //         rightArm.SetAnimation("default");
    //         leftArm.SetAnimation("default");
    //         head.SetAnimation("default");
    //         body.SetAnimation("idle");

    //         repeatAnimation = true;
    //     }
    //     if(animationName == "jump"){
    //         rightLeg.SetAnimation("jump");
    //         leftLeg.SetAnimation("jump");
    //         rightArm.SetAnimation("run");
    //         leftArm.SetAnimation("run");
    //         head.SetAnimation("run");
    //         body.SetAnimation("run");
            
    //         repeatAnimation = false;
    //         animationLength = 1;
    //     }
    //     if(animationName == "fall"){
    //         rightLeg.SetAnimation("fall");
    //         leftLeg.SetAnimation("fall");
    //         rightArm.SetAnimation("run");
    //         leftArm.SetAnimation("run");
    //         head.SetAnimation("run");
    //         body.SetAnimation("run");

    //         repeatAnimation = false;
    //         animationLength = 3;
    //     }
    //     if(animationName == "attack_one_hand_1"){
    //         animationTimer.Stop();
    //         animationTimer.Start();
    //         rightLeg.SetAnimation("attack_one_hand_1");
    //         leftLeg.SetAnimation("attack_one_hand_1");
    //         rightArm.SetAnimation("attack_one_hand_1");
    //         leftArm.SetAnimation("attack_one_hand_1");
    //         head.SetAnimation("attack_one_hand_1");
    //         body.SetAnimation("attack_one_hand_1");
            
    //         _selectedWeaponSprites.Visible = true;
    //         MapLocation(_selectedWeaponSprites, new float[3]{-29, 46, 155});
    //         _selectedWeaponSprites.Frame = 0;

    //         animationFunction = WeaponAnimation;
    //         animationExitFunction = WeaponAnimationExit;

    //         repeatAnimation = false;
    //         allowInterruption = false;
    //         animationLength = 3;
            
    //     }
    //     iterationCount = 0;
    //     currentAnimation = animationName;
    // }

    // private void WeaponAnimation(int n)
    // {
    //     if(n == 0){
    //         _selectedWeaponSlash.Visible = true;
    //         MapLocation(_selectedWeaponSprites, new float[3]{-13, -46, -90});
    //         _selectedWeaponSprites.Frame = 2;
    //     } else if(n == 1){
    //         _selectedWeaponSlash.Visible = false;
    //         MapLocation(_selectedWeaponSprites, new float[3]{19, -41, -38});
    //         _selectedWeaponSprites.Frame = 1;
    //     }
    // }
    // private void WeaponAnimationExit(){
    //     _selectedWeaponSlash.Visible = false;
    //     _selectedWeaponSprites.Visible = false;
    //     _selectedWeaponSprites.Frame = 0;
    // }


    // private int iterationCount = 0;
    // public void AnimateFrame(){
    //     animationFunction?.Invoke(iterationCount);
    //     if (!repeatAnimation && iterationCount < animationLength){
    //         iterationCount++;
    //     }
    //     if(!repeatAnimation  && iterationCount >= animationLength){
    //         allowInterruption = true;
    //         return;
    //     }
    //     head.NextFrame();
    //     rightArm.NextFrame();
    //     leftArm.NextFrame();
    //     body.NextFrame();
    //     rightLeg.NextFrame();
    //     leftLeg.NextFrame();
    // }

    public void AnimateFrame(){
        GD.Print("animate Frame");
        _playerAnimationFactory?.AnimationStep();
    }
}