using System;
using Godot;
using scripts.Utils.Animation;

namespace scripts.Player;
public class PlayerAnimationHandler : AnimationHandler {
    public SharedAnimationProperties sharedAnimationProperties;
    public AnimationLink head;
    public AnimationLink body;
    public AnimationLink rightArm;
    public AnimationLink leftArm;
    public AnimationLink rightLeg;
    public AnimationLink leftLeg;
    public PlayerAnimationHandler(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) 
        : base(animationName) {
        this.head = head;
        this.body = body;
        this.rightArm = rightArm;
        this.leftArm = leftArm;
        this.rightLeg = rightLeg;
        this.leftLeg = leftLeg;
        this.sharedAnimationProperties = sharedAnimationProperties;
    }
}

public class AnimationBuilder {
    private readonly SharedAnimationProperties _sharedAnimationProperties;
    private readonly AnimationLink _head;
    private readonly AnimationLink _body;
    private readonly AnimationLink _rightArm;
    private readonly AnimationLink _leftArm;
    private readonly AnimationLink _rightLeg;
    private readonly AnimationLink _leftLeg;

    public AnimationBuilder(AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg, SharedAnimationProperties sharedAnimationProperties) {
        _head = head;
        _body = body;
        _rightArm = rightArm;
        _leftArm = leftArm;
        _rightLeg = rightLeg;
        _leftLeg = leftLeg;
        _sharedAnimationProperties = sharedAnimationProperties;
    }

    public T Build<T>(string animationName) where T : PlayerAnimationHandler {
        return (T)Activator.CreateInstance(typeof(T), animationName, _head, _body, _rightArm, _leftArm, _rightLeg, _leftLeg, _sharedAnimationProperties);
    }
}

public class SharedAnimationProperties{
    public bool holdingWeapon = false;
	public AnimatedSprite2D selectedWeaponSprites;
	public Sprite2D selectedWeaponSlash;
}