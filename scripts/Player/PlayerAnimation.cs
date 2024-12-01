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
        if(animationName == "run"){
            // body.SetRotation(15);
            // head.SetRotation(-15);
            // rightArm.SetRotation(-10);
            // leftArm.SetRotation(-10);
            rightLeg.SetAnimation("run");
            leftLeg.SetAnimation("run");
        }
        if(animationName == "default"){
            if(currentAnimation == "walk"){
                easingOutOfWalkLeft = true;
                easingOutOfWalkRight = true;
            } else if (currentAnimation == "run"){
                //transition run into a walk then ease out
                leftLeg.SetAnimation("walk");
                rightLeg.SetAnimation("walk");
                int[] runToWalkLeft = {4,5,5,6,7,8,9,1};
                int[] runToWalkRight = {2,3,4,6,9,0,0,1};
                leftLeg.SetFrame(runToWalkLeft[leftLeg.GetCurrentFrame()]);
                rightLeg.SetFrame(runToWalkRight[rightLeg.GetCurrentFrame()]);
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


    public bool easingOutOfWalkLeft = false;
    public bool easingOutOfWalkRight = false;
    public bool easingOutOfRunLeft = false;
    public bool easingOutOfRunRight = false;
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
            bool breakOut = EaseOut(rightLeg, (4,0), (1,7), new int[]{1,3});
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
            bool breakOut = EaseOut(leftLeg, (0,5), (6,2), new int[]{6,8});
            easingOutOfWalkLeft = !breakOut;
            if(breakOut || currentFrame == 8){
              leftLeg.SetAnimation("default");
            }
        } else {
            leftLeg.NextFrame();
        }
    }

    private static bool EaseOut(AnimationLink animationLink, (int, int) targetFrames, (int, int) endFrames, int[] skippedFrames=null){     
        skippedFrames ??= System.Array.Empty<int>();
        HashSet<int> skippedFramesSet = new(skippedFrames);

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
                if(skippedFramesSet.Contains(animationLink.GetCurrentFrame())){
                    animationLink.PrevFrame();
                }
            } else {
                animationLink.NextFrame();
                if(skippedFramesSet.Contains(animationLink.GetCurrentFrame())){
                    animationLink.NextFrame();
                }
            }
        } else {
            if(closestTargetFrameDistance > 0){
                animationLink.PrevFrame();
                if(skippedFramesSet.Contains(animationLink.GetCurrentFrame())){
                    animationLink.PrevFrame();
                }
            } else {
                animationLink.NextFrame();
                if(skippedFramesSet.Contains(animationLink.GetCurrentFrame())){
                    animationLink.NextFrame();
                }
            }
        }
        return false;
    }
}