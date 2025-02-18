using System.Collections.Generic;
using Godot;
using Utils.Animation;

namespace Utils.Animation;
class AnimationFactory : IAnimationFactory {
    private Dictionary<string, AnimationHandler> _animationMap;
    private AnimationHandler _currentAnimationHandler = null;
    private readonly Timer _animationTimer;

    public AnimationFactory(Dictionary<string, AnimationHandler> animationMap, Timer timer){
        _animationMap = animationMap;
        _animationTimer = timer;
    }

    private int _iterationCount = 0; 
    private string _currentAnimation = "";
    private int _animationLength = 0;
    private bool _repeatAnimation = true;
    private bool _allowInterruption = true;
    public void PlayAnimation(string animationName) {
        if(animationName == _currentAnimation){
                return;
        }
        if(!_allowInterruption){
            return;
        }
        _iterationCount = 0;
        _currentAnimation = animationName;

        _currentAnimationHandler?.End();
        _currentAnimationHandler = _animationMap[animationName];
        if(_currentAnimationHandler == null){
		    throw new System.Exception("This animation is not in the provided Map: " + animationName);
        }
        _allowInterruption = _currentAnimationHandler.allowInterruption;
        _repeatAnimation = _currentAnimationHandler.repeatAnimation;
        _animationLength = _currentAnimationHandler.animationLength;
        if(_currentAnimationHandler.restartTimer){
            _animationTimer.Stop();
            _animationTimer.Start();
        }
        _currentAnimationHandler?.Start();
    }

    public void AnimationStep() {
        if(_currentAnimationHandler.animationLength < 1){
            throw new System.Exception("You must set the animation length to a number above 0 when using animation handlers");
        }

        if(!_repeatAnimation  && _iterationCount >= _animationLength-1){
            _allowInterruption = true;
            return;
        }
        _iterationCount++;
        _iterationCount %= _animationLength;
        
        _currentAnimationHandler.NextFrame(_iterationCount);
    }
}
