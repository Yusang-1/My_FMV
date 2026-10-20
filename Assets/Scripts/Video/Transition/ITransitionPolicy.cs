using System.Collections;

public interface ITransitionPolicy
{
    public IEnumerator WaitForTransition(TransitionData transitionData);
}
