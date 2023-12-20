using UnityEngine;
using TMPro;

public class PlayersTurnChanger : MonoBehaviour
{
  public TextMeshProUGUI PlayerText;    // Надпись для отображения текущего игрока
  private PlayerChip[] _playersChips;   // Массив фишек всех игроков
  private int _currentPlayerId;         // Номер текущего игрока

  public void StartGame(PlayerChip[] playersChips)
  {
    _playersChips = playersChips;   // Присваиваем массив фишек игроков
    _currentPlayerId = -1;          // Текущему игроку задаём номер -1
    MovePlayerTurn();               // Вызываем метод для перехода к следующему игроку
  }

  public int GetCurrentPlayerId() {
    return _currentPlayerId;      // Получаем номер текущего игрока
  }

  public void MovePlayerTurn()
  {
    _currentPlayerId++;     // Увеличиваем номер текущего игрока на 1

    // Если номер стал больше или равен количеству фишек игроков
    if (_currentPlayerId >= _playersChips.Length) {
      _currentPlayerId = 0;   // Обнуляем номер текущего игрока
    }
    
    SetPlayerText(_currentPlayerId);    // Вызываем метод для установки надписи с номером игрока
  }

  private void SetPlayerText(int playerId) {
    PlayerText.text = $"Player {playerId + 1}";      // Надпись отображает номер текущего игрока
  }
}
