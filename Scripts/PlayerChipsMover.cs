using UnityEngine;

public class PlayerChipsMover : MonoBehaviour
{
  public GameField GameField;                        // Скрипт игрового поля
  public TransitionSettings TransitionSettings;      // Скрипт с настройками переходов
  public PlayerChipsAnimator PlayersChipsAnimator;  // Скрипт анимации движения фишек

  private PlayerChip[] _playersChips;   // Массив фишек игроков
  private int[] _playersChipsCellsIds;  // Массив текущих позиций фишек

  public void StartGame(PlayerChip[] playersChips)
  {
    _playersChips = playersChips;                           // Присваиваем массив фишек игроков
    _playersChipsCellsIds = new int[playersChips.Length];   // Создаём массив для хранения текущих позиций фишек
    RefreshChipsPositions();
  }

  public void RefreshChipsPositions()
  {
    // Проходим в цикле по всем фишкам игроков
    for (int i = 0; i < _playersChips.Length; i++) {
      RefreshChipPosition(i);   // Вызываем метод для обновления позиции фишки с номером i
    }
  }

  public void MoveChip(int playerId, int steps)
  {
    int startCellId = _playersChipsCellsIds[playerId];     // Сохраняем текущую позицию фишки
    _playersChipsCellsIds[playerId] += steps;              // Увеличиваем текущую позицию фишки на заданное число шагов

    // Если текущая позиция фишки превышает количество ячеек на игровом поле
    if (_playersChipsCellsIds[playerId] >= GameField.CellsCount) {
      _playersChipsCellsIds[playerId] = GameField.CellsCount - 1;   // Устанавливаем фишку на последнюю ячейку
    }

    int lastCellId = _playersChipsCellsIds[playerId];            // Сохраняем новую позицию фишки

    TryApplyTransition(playerId);                                // Сохраняем новую позицию фишки
    int afterTransitionCellId = _playersChipsCellsIds[playerId]; // Сохраняем позицию фишки после возможного перехода

    int[] movementCells = GetMovementCells(startCellId, lastCellId, afterTransitionCellId);  // Получаем номера ячеек, по которым пойдёт фишка

    PlayersChipsAnimator.AnimateChipMovement(_playersChips[playerId], movementCells);   // Запускаем анимацию движения фишки
    //RefreshChipPosition(playerId);    // Вызываем метод для обновления позиции фишки
  }

  private int[] GetMovementCells(int startCellId, int lastCellId, int afterTransitionCellId)
  {
    int  cellsCount    = lastCellId - startCellId + 1;            // Вычисляем количество ячеек, которые должна посетить фишка
    bool hasTransition = lastCellId != afterTransitionCellId;     // Проверяем, есть ли переход по змее или лестнице — сравниваем последнюю ячейку и ячейку после перехода

    // Если есть переход по змее или лестнице
    if (hasTransition) { cellsCount++; } // Увеличиваем количество ячеек на 1

    int[] movementCells = new int[cellsCount];      // Создаём массив с указанным количеством ячеек

    // Проходим по всему массиву ячеек
    for (int i = 0; i < movementCells.Length; i++) {
      // Если это последняя клетка и на ней есть переход
      if (i == movementCells.Length - 1 && hasTransition) { movementCells[i] = afterTransitionCellId; } // Записываем номер ячейки после перехода
      else                                                { movementCells[i] = startCellId + i; }       // Записываем номер текущей ячейки
    }
    
    return movementCells;     // Возвращаем массив с номерами ячеек для анимации
  }

  private void RefreshChipPosition(int playerId)
  {
    Vector2 chipPosition = GameField.GetCellPosition(_playersChipsCellsIds[playerId]);    // Получаем позицию ячейки на игровом поле, которая соответствует текущей позиции фишки
    _playersChips[playerId].SetPosition(chipPosition);                                    // Устанавливаем фишку на полученную позицию
  }

  private void TryApplyTransition(int playerId)
  {
    // Получаем новый номер ячейки после хода игрока
    int resultCellId = TransitionSettings.GetTransitionResultCellId(_playersChipsCellsIds[playerId]);

    // Если номер меньше 0
    if (resultCellId < 0) {
      return;  // Переход по змее или лестнице не нужен
    }

    _playersChipsCellsIds[playerId] = resultCellId;  // Устанавливаем новый номер ячейки
  }

  public bool CheckPlayerFinished(int playerId)
  {
    // Возвращаем true, если номер текущей клетки указанного игрока больше или равен количеству клеток на игровом поле - 1
    return _playersChipsCellsIds[playerId] >= GameField.CellsCount - 1;
  }
}
