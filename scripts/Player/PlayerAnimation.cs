using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;
using Utils.AnimationLink;

public partial class Player: CharacterBody2D{
    public AnimationLink head;
    public AnimationLink body;
    public AnimationLink rightArm;
    public AnimationLink leftArm;
    public AnimationLink rightLeg;
    public AnimationLink leftLeg;

    public string currentAnimation = "";
    private int animationLength = 0;
    private bool repeatAnimation = true;
    private bool allowInterruption = true;

    public void AnimationInit(){
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
    }


    public void PlayAnimation(string animationName){
        string prevAnimation = currentAnimation;
        if(!allowInterruption){
            return;
        }
        if(animationName == prevAnimation){
            return;
        }
        animationLength = 0;
        repeatAnimation = true;

        if(animationName == "walk"){
            rightLeg.SetAnimation("walk");
            leftLeg.SetAnimation("walk");
            rightArm.SetAnimation("default");
            leftArm.SetAnimation("default");
            head.SetAnimation("default");
            body.SetAnimation("idle");

            repeatAnimation = true;
        }
        if(animationName == "run"){
            //start the animation on frame 0
            rightLeg.SetAnimation("run");
            leftLeg.SetAnimation("run");
            rightArm.SetAnimation("run");
            leftArm.SetAnimation("run");
            head.SetAnimation("run");
            body.SetAnimation("run");

            repeatAnimation = true;
        }
        if(animationName == "default"){
            rightLeg.SetAnimation("default");
            leftLeg.SetAnimation("default");
            rightArm.SetAnimation("default");
            leftArm.SetAnimation("default");
            head.SetAnimation("default");
            body.SetAnimation("idle");

            repeatAnimation = true;
        }
        if(animationName == "jump"){
            rightLeg.SetAnimation("jump");
            leftLeg.SetAnimation("jump");
            rightArm.SetAnimation("run");
            leftArm.SetAnimation("run");
            head.SetAnimation("run");
            body.SetAnimation("run");
            repeatAnimation = false;
            animationLength = 1;
        }
        if(animationName == "fall"){
            rightLeg.SetAnimation("fall");
            leftLeg.SetAnimation("fall");
            rightArm.SetAnimation("run");
            leftArm.SetAnimation("run");
            head.SetAnimation("run");
            body.SetAnimation("run");

            repeatAnimation = false;
            animationLength = 3;
        }
        if(animationName == "attack_one_hand_1"){
            rightLeg.SetAnimation("attack_one_hand_1");
            leftLeg.SetAnimation("attack_one_hand_1");
            rightArm.SetAnimation("attack_one_hand_1");
            leftArm.SetAnimation("attack_one_hand_1");
            head.SetAnimation("attack_one_hand_1");
            body.SetAnimation("attack_one_hand_1");
            
            repeatAnimation = false;
            allowInterruption = false;
            animationLength = 3;
            
        }
        iterationCount = 0;
        currentAnimation = animationName;
    }


    public bool easingOutOfWalkLeft = false;
    public bool easingOutOfWalkRight = false;
    public bool easingOutOfRunLeft = false;
    public bool easingOutOfRunRight = false;

    private int iterationCount = 0;
    public void AnimateFrame(){
        if(!repeatAnimation && iterationCount < animationLength){
            iterationCount++;
        }
        if(!repeatAnimation  && iterationCount >= animationLength){
            allowInterruption = true;
            return;
        }
        head.NextFrame();
        rightArm.NextFrame();
        leftArm.NextFrame();
        body.NextFrame();
        rightLeg.NextFrame();
        leftLeg.NextFrame();
    }
}