using UnityEngine;

public class GameField : MonoBehaviour
{
  public Transform FirstCellPoint;      // Позиция первой ячейки
  public Vector2 CellSize;              // Размер ячейки (по X и Y)
  public int CellsCount = 100;          // Общее количество ячеек на игровом поле
  public int CellsInRow = 10;           // Количество ячеек в одном ряду

  private Vector2[]   _cellsPositions;  // Массив из позиций каждой ячейки
  private Vector2[,] _cellsPositions2; // Массив из позиций каждой ячейки

  public void FillCellsPositions()
  {
    _cellsPositions = new Vector2[CellsCount];// Создаём массив с размером, равным общему количеству ячеек

    float xSign = 1;        // Заводим переменную, которая отслеживает, где создаются новые ячейки (они будут добавляться вправо и влево)
    _cellsPositions[0] = FirstCellPoint.position; // Делаем позицию первой ячейки в массиве равной заданной позиции первой ячейки

    // Проходим по каждой ячейке, начиная со второй
    for (int i = 1; i < _cellsPositions.Length; i++)
    {
      bool needUp = i % CellsInRow == 0;  // Узнаём, нужно ли подниматься на новый ряд ячеек

      if (needUp)  // Если нужно подниматься на новый ряд
      {
        xSign *= -1;  // Меняем направление движения на противоположное
        _cellsPositions[i] = _cellsPositions[i - 1] + Vector2.up * CellSize.y; // Позиция ячейки получается путём смещения на высоту одной ячейки вверх
      }
      else    // Если не нужно подниматься на новый ряд:
      { 
        float deltaX = xSign * CellSize.x;                                     // Смещение по горизонтали равно ширине одной клетки, умноженной на знак смещения
        _cellsPositions[i] = _cellsPositions[i - 1] + Vector2.right * deltaX;  // Позиция ячейки определяется, когда мы смещаем её на указанное значение по горизонтали
      }
    }
  }

  public void FillCellsPositionsMap()
  {
    _cellsPositions2 = new Vector2[CellsCount / 2, CellsCount / 2]; // Создаём массив с размером, равным общему количеству ячеек
    _cellsPositions2[0, 0] = FirstCellPoint.position;            // Делаем позицию первой ячейки в массиве равной заданной позиции первой ячейки
    int xSign = 1;
    /*for (int y = 0; y <= 10; y++)
    {
      _cellsPositions2[x][y] = _cellsPositions2[x][y] + y*Vector2.up * CellSize.y;
      for (int x = 0; x <= 10; x++)
      {
        // Смещение по горизонтали равно ширине одной клетки, умноженной на знак смещения
        float deltaX = xSign * CellSize.x;
        _cellsPositions2[x, y] = _cellsPositions2[x + xSign, y] + Vector2.right * deltaX;
      }
      xSign *= -1;
    }*/
  }

  public Vector2 GetCellPosition(int id)
  {
    // Если номер ячейки некорректный
    if (id < 0 || id >= _cellsPositions.Length) {
      return Vector2.zero;       // Возвращаем нулевые значения (0, 0)
    }
    return _cellsPositions[id];  // Иначе возвращаем позицию ячейки
  }
}
