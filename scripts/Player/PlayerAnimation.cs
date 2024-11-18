using System;
using Godot;
using Utils.Skeleton;

public partial class Player: CharacterBody2D{
    public Skeleton headSkeleton;
    public Skeleton bodySkeleton;
    public Skeleton rightArmSkeleton;
    public Skeleton leftArmSkeleton;
    public Skeleton rightLegSkeleton;
    public Skeleton leftLegSkeleton;

    public void AnimationInit(){
        Node2D head = GetNode<Node2D>("CharacterHead");
		Node2D body = GetNode<Node2D>("CharacterBody");
		Node2D rightArm = GetNode<Node2D>("CharacterRightArm");
		Node2D leftArm = GetNode<Node2D>("CharacterLeftArm");
		Node2D rightLeg = GetNode<Node2D>("CharacterRightLeg");
		Node2D leftLeg = GetNode<Node2D>("CharacterLeftLeg");

        headSkeleton = new Skeleton(head);
        rightArmSkeleton = new Skeleton(rightArm);
        leftArmSkeleton = new Skeleton(leftArm);
        bodySkeleton = new Skeleton(body, new Skeleton[] { headSkeleton, rightArmSkeleton, leftArmSkeleton });
        rightLegSkeleton = new Skeleton(rightLeg);
        leftLegSkeleton = new Skeleton(leftLeg);
    }

    public void PlayAnimation(string animationName){
        if(animationName == "walk"){
            rightLegSkeleton.animatedSprite.Play("walk");
            leftLegSkeleton.animatedSprite.Play("walk");
        }
        if(animationName == "default"){
            rightLegSkeleton.animatedSprite.Play("default");
            leftLegSkeleton.animatedSprite.Play("default");
        }
        if(animationName == "jump"){
            rightLegSkeleton.animatedSprite.Play("jump");
            leftLegSkeleton.animatedSprite.Play("jump");
        }
    }

    public void RotateBody(float degrees){
        bodySkeleton.SetRotation(bodySkeleton.animatedSprite.Rotation + degrees);
	}

}