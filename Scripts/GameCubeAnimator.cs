using UnityEngine;

public class GameCubeAnimator : MonoBehaviour
{
  public Animation CubeAnimation;         // Ссылка на компонент анимации кубика
  public GameCubeThrower GameCubeThrower; // Скрипт бросков кубика

  // Проигрываем анимацию
  public void PlayAnimation() { CubeAnimation.Play(); }

  // Этот метод мы вызовем позже внутри анимации
  public void OnAnimationEnd() { GameCubeThrower.ContinueAfterCubeAnimation(); }  // Продолжаем действия после анимации
}
