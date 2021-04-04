using DI;
using UnityEngine;

public abstract class BaseGameController : MonoBehaviour
{
    [Inject] protected GameManager manager { get;}

    public virtual void Subscribe()
    {
        
    }

    public virtual void Unsubscribe()
    {
        
    }
}