using UnityEngine;

public class TransitionData : MonoBehaviour
{
  public int[] CellsStartIds;   // Массив с номерами начальных ячеек
  public int[] CellsEndIds;     // Массив с номерами конечных ячеек

  public int GetTransitionResultCellId(int cellId)
  {
    // Проходим в цикле по всем начальным ячейкам
    for (int i = 0; i < CellsStartIds.Length; i++)
    {
      if (CellsStartIds[i] == cellId) { // Если номер начальной ячейки совпадает с переданным
        return CellsEndIds[i];  // Возвращаем номер конечной ячейки для этой змеи или лестницы
      } 
    }
    return -1;    // Если условие не выполнилось, возвращаем -1 — это значит, что перемещения по змее или лестнице нет
  }
}
