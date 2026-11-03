using System;
using System.Collections;

public interface ITransitionPolicy
{
    public event Action OnTransitionStart;
    public IEnumerator WaitForTransition(TransitionData transitionData);
}
