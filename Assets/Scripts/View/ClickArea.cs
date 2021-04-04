using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace View
{
    public class ClickArea : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Vector3> ClickEvent; 
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            ClickEvent?.Invoke(eventData.pointerCurrentRaycast.worldPosition);
        }
    }
}