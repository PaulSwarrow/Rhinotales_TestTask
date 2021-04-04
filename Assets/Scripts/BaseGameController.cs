using DI;
using UnityEngine;

public abstract class BaseGameController : MonoBehaviour
{
    [Inject] protected GameManager manager { get;}
}