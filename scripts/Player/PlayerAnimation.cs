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
        body = new AnimationLink(bodyParent, new AnimationLink[] { head, rightArm, leftArm });
        rightLeg = new AnimationLink(rightLegParent);
        leftLeg = new AnimationLink(leftLegParent);


        AnimationCreator animationCreator = new(head, body, rightArm, leftArm, rightLeg, leftLeg);
        _animationMap = new Dictionary<string, AnimationHandler> {
            {"attack_one_hand_1", animationCreator.AddAnimationLinks(new AttackOneHand1("attack_one_hand_1")).SetWeapon(_selectedWeaponSprites, _selectedWeaponSlash).Init()},
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

    public void AnimateFrame(){
        _playerAnimationFactory?.AnimationStep();
    }
}