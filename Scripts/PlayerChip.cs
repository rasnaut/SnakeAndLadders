using UnityEngine;

public class PlayerChip : MonoBehaviour
{
  // Этот метод устанавливает спрайт фишки
  public void SetSprite(Sprite sprite)
  {
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); // Получаем компонент SpriteRenderer, который отображает спрайты на игровой сцене
    spriteRenderer.sprite = sprite;                                 // Устанавливаем спрайт, переданный в метод, в компонент SpriteRenderer
  }

  // Устанавливаем новую позицию фишки
  public void SetPosition(Vector2 position) {
    transform.position = position;   // Изменяем позицию объекта, к которому привязан этот скрипт
  }
}