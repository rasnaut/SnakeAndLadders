using UnityEngine;

public class PlayerChipsCreator : MonoBehaviour
{
  public PlayerChip PlayerChipPrefab;                // Префаб фишки игрока
  public Sprite[] PlayerChipSprites = new Sprite[0]; // Массив спрайтов фишек игроков размером 0
  private PlayerChip[] playersChips = null;

  public PlayerChip[] SpawnPlayersChips(int count)
  {
    playersChips = new PlayerChip[count];     // Создаём массив нужной длины для фишек игроков
    
    // Проходим по циклу длиной в количество игроков
    for (int i = 0; i < count && i < PlayerChipSprites.Length; i++) {
      playersChips[i] = SpawnPlayerChip(PlayerChipSprites[i]);  // Создаём фишку для текущего игрока из массива спрайтов
    }

    return playersChips;  // Возвращаем массив созданных фишек игроков
  }

  private PlayerChip SpawnPlayerChip(Sprite sprite)
  {
    // Если спрайт отсутствует
    if (!sprite) 
      return null;
    
    PlayerChip newPlayerChip = Instantiate(PlayerChipPrefab, transform.position, transform.rotation); // Создаём новую фишку игрока из префаба фишки

    newPlayerChip.SetSprite(sprite);  // Устанавливаем спрайт фишки

    return newPlayerChip;
  }

  public void Clear()
  {
    DestroyPlayersChips();    // Удаляем все фишки игроков
  }

  private void DestroyPlayersChips()
  {
    // Проходим по каждой фишке игрока
    for (int i = 0; i < playersChips.Length; i++) {
      Destroy(playersChips[i].gameObject);    // Уничтожаем их игровые объекты
    }
  }
}
