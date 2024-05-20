using UnityEngine ;
using GG.Infrastructure.Utils.Swipe ;

public class TouchListener : MonoBehaviour {
   [SerializeField] private SwipeListener swipeListener ;
   
   private void OnEnable () {
      swipeListener.OnSwipe.AddListener (OnSwipe) ;
   }

   private void OnSwipe (string swipe) {
      Debug.Log(swipe);
   }

   private void OnDisable () {
      swipeListener.OnSwipe.RemoveListener (OnSwipe) ;
   }
}
