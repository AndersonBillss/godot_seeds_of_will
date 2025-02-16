interface IAnimationHandler{
    public void Start();
    public void NextFrame(int n);
    public void End();
}

interface IAnimationFactory{
    public void PlayAnimation(string animationName);
    public void AnimationStep();    
}