using System;
using scripts.Utils.Animation;

namespace scripts.Player;
public class PlayerAnimationHandler : AnimationHandler {
    public AnimationLink head { get; private set; }
    public AnimationLink body { get; private set; }
    public AnimationLink rightArm { get; private set; }
    public AnimationLink leftArm { get; private set; }
    public AnimationLink rightLeg { get; private set; }
    public AnimationLink leftLeg { get; private set; }

    public PlayerAnimationHandler(string animationName, AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg) 
        : base(animationName) {
        this.head = head;
        this.body = body;
        this.rightArm = rightArm;
        this.leftArm = leftArm;
        this.rightLeg = rightLeg;
        this.leftLeg = leftLeg;
    }
}

public class AnimationBuilder {
    private readonly AnimationLink _head;
    private readonly AnimationLink _body;
    private readonly AnimationLink _rightArm;
    private readonly AnimationLink _leftArm;
    private readonly AnimationLink _rightLeg;
    private readonly AnimationLink _leftLeg;

    public AnimationBuilder(AnimationLink head, AnimationLink body, AnimationLink rightArm, AnimationLink leftArm, AnimationLink rightLeg, AnimationLink leftLeg) {
        _head = head;
        _body = body;
        _rightArm = rightArm;
        _leftArm = leftArm;
        _rightLeg = rightLeg;
        _leftLeg = leftLeg;
    }

    public T Build<T>(string animationName) where T : PlayerAnimationHandler {
        return (T)Activator.CreateInstance(typeof(T), animationName, _head, _body, _rightArm, _leftArm, _rightLeg, _leftLeg);
    }
}