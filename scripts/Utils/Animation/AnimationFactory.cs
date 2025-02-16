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

    private int iterationCount = 0; 
    public void PlayAnimation(string animationName) {
        if(animationName == _currentAnimation){
                return;
        }
        if(!_allowInterruption){
            return;
        }

        _currentAnimationHandler?.End();
        _currentAnimationHandler = _animationMap[animationName];
        if(_currentAnimationHandler == null){
            GD.PrintErr("This animation is not in the provided Map: " + animationName);
        }
        _allowInterruption = _currentAnimationHandler.allowInterruption;
        _repeatAnimation = _currentAnimationHandler.repeatAnimation;
        if(_currentAnimationHandler.restartTimer){
            // _animationTimer.Stop();
            // _animationTimer.Start();
        }
        _currentAnimationHandler?.Start();
    }

    private readonly string _currentAnimation = "";
    private readonly int _animationLength = 0;
    private bool _repeatAnimation = true;
    private bool _allowInterruption = true;

    public void AnimationStep() {
        iterationCount++;
        if(_currentAnimationHandler.animationLength != 0){
            iterationCount%=_currentAnimationHandler.animationLength;
        }
        
        _currentAnimationHandler.NextFrame(iterationCount);

        if(!_repeatAnimation  && iterationCount >= _animationLength){
            _allowInterruption = true;
            return;
        }
    }
}
