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
        if(animationName == currentAnimation){
            return;
        }
        //reset the rotation
        head.SetRotation(0);
        body.SetRotation(0);
        leftArm.SetRotation(0);
        rightArm.SetRotation(0);

        if(animationName == "walk"){
            rightLeg.SetAnimation("walk");
            leftLeg.SetAnimation("walk");
        }
        if(animationName == "default"){
            if(currentAnimation == "walk"){
                easingOutOfWalkLeft = true;
                easingOutOfWalkRight = true;
            } else {
                rightLeg.SetAnimation("default");
                leftLeg.SetAnimation("default");
            }
        }
        if(animationName == "jump"){
            rightLeg.SetAnimation("jump");
            leftLeg.SetAnimation("jump");
        }
        currentAnimation = animationName;
    }


    public bool easingOutOfWalkRight = false;
    public bool easingOutOfWalkLeft = false;
    public void AnimateFrame(){
        head.NextFrame();
        rightArm.NextFrame();
        leftArm.NextFrame();
        body.NextFrame();
        AnimateLegs();


    }

    private void AnimateLegs(){
        if(easingOutOfWalkRight && currentAnimation == "default"){
            int currentFrame = rightLeg.animatedSprite.Frame;
            if(currentFrame == 1){
                rightLeg.NextFrame();
            }
            if(currentFrame == 3){
                leftLeg.PrevFrame();
            }
            bool breakOut = EaseOut(rightLeg, (4,0), (1,7));
            easingOutOfWalkRight = !breakOut;
            if(breakOut || currentFrame == 3){
              rightLeg.SetAnimation("default");
            }
        } else {
            //sync the animations up
            rightLeg.animatedSprite.Frame = leftLeg.animatedSprite.Frame;
            rightLeg.NextFrame();
        }
        if(easingOutOfWalkLeft && currentAnimation == "default"){
            int currentFrame = leftLeg.animatedSprite.Frame;
            if(currentFrame == 6){
                leftLeg.NextFrame();
            }
            if(currentFrame == 8){
                leftLeg.PrevFrame();
            }
            bool breakOut = EaseOut(leftLeg, (0,5), (6,2));
            easingOutOfWalkLeft = !breakOut;
            if(breakOut || currentFrame == 8){
              leftLeg.SetAnimation("default");
            }
        } else {
            leftLeg.NextFrame();
        }
    }

    private static bool EaseOut(AnimationLink animationLink, (int, int) targetFrames, (int, int) endFrames){     
        int currentFrame = animationLink.animatedSprite.Frame;
        Array<int> targetFrameArray = new() { targetFrames.Item1, targetFrames.Item2};
        HashSet<int> targetFrameSet = new(targetFrameArray);
        bool finished = targetFrameSet.Contains(currentFrame) || 
            targetFrameSet.Contains(currentFrame + 1) || 
            targetFrameSet.Contains(currentFrame - 1);
        if(finished){
            return true;
        }

        static int LeastAbsValue((int,int) values){
            int val1;
            int val2;
            (val1, val2) = values;
            bool firstValueGreater = Math.Abs(val1) > Math.Abs(val2);
            return firstValueGreater?val1:val2;
        }

        int distanceToEndFrame1 = 
            LeastAbsValue(animationLink.DistanceBetween(currentFrame, endFrames.Item1+1));
        int distanceToEndFrame2 = 
            LeastAbsValue(animationLink.DistanceBetween(currentFrame, endFrames.Item2+1));
        int distanceToTargetFrame1 = 
            LeastAbsValue(animationLink.DistanceBetween(currentFrame, targetFrames.Item1+1));
        int distanceToTargetFrame2 = 
            LeastAbsValue(animationLink.DistanceBetween(currentFrame, targetFrames.Item2+1));
        int closestEndFrameDistance = 
            LeastAbsValue((distanceToEndFrame1,distanceToEndFrame2));
        int closestTargetFrameDistance = 
            LeastAbsValue((distanceToTargetFrame1, distanceToTargetFrame2));

        if(closestEndFrameDistance > closestTargetFrameDistance){
            if(closestEndFrameDistance < 0){
                animationLink.PrevFrame();
            } else {
                animationLink.NextFrame();
            }
        } else {
            if(closestTargetFrameDistance > 0){
                animationLink.PrevFrame();
            } else {
                animationLink.NextFrame();
            }
        }
        return false;
    }
}