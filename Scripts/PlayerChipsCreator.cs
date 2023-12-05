using UnityEngine;

public class PlayerChipsCreator : MonoBehaviour
{
  public int PlayersCount = 2;                       // Количество игроков в игре
  public PlayerChip PlayerChipPrefab;                // Префаб фишки игрока
  public Sprite[] PlayerChipSprites = new Sprite[0]; // Массив спрайтов фишек игроков размером 0

  private void Start()
  {
    SpawnPlayersChips(PlayersCount);  // При запуске игры создаём фишки игроков
  }

  private void SpawnPlayersChips(int count)
  {
    // Перебираем количество игроков в цикле
    for (int i = 0; i < count && i <= PlayerChipSprites.Length; i++)
    {
      SpawnPlayerChip(PlayerChipSprites[i]); // Создаём фишку игрока с переданным спрайтом
    }
  }

  private void SpawnPlayerChip(Sprite sprite)
  {
    // Если спрайт отсутствует
    if (!sprite) 
      return;
    
    PlayerChip newPlayerChip = Instantiate(PlayerChipPrefab, transform.position, transform.rotation); // Создаём новую фишку игрока из префаба фишки

    newPlayerChip.SetSprite(sprite);  // Устанавливаем спрайт фишки
  }
}
