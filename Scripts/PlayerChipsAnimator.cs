using UnityEngine;

public class PlayerChipsAnimator : MonoBehaviour
{
  
  public GameStateChanger GameStateChanger;  // Скрипт изменения состояния игры
  public GameField GameField;                // Скрипт игрового поля
  public float CellMoveDuration = 0.3f;      // Длительность перемещения между ячейками
  
  private PlayerChip  _playerChip;           // Префаб фишки текущего игрока
  private bool        isAnimateNow;          // Флаг, который указывает, выполняется ли сейчас анимация
  private int[]       _movementCells;        // Массив ячеек, по которым нужно переместиться
  private int         _currentCellId;        // Индекс текущей ячейки, которую анимируют
  private float       _cellMoveTimer;        // Временной счётчик для анимации
  private Vector2     _startPosition;        // Начальная позиция перемещения
  private Vector2     _endPosition;          // Конечная позиция перемещения
  
  // Update is called once per frame
  void Update()
  {
    Animation();      // Вызываем метод управления анимацией
  }

  public void AnimateChipMovement(PlayerChip playerChip, int[] movementCells)
  {
    _playerChip = playerChip;       // Сохраняем переданную фишку в переменную
    _movementCells = movementCells; // Получаем массив ячеек, через которые фишка должна пройти
    isAnimateNow = true;            // Задаём флаг, который указывает, что анимация идёт сейчас
    _currentCellId = -1;            // Устанавливаем начальное значение текущей ячейки
    ToNextCell();                   // Начинаем движение к следующей ячейке
  }

  private void Animation()
  {
    // Если анимация сейчас не выполняется
    if (!isAnimateNow) {
      return;   // Выходим из метода
    }
    // Если таймер движения фишки достиг значения 1
    if (_cellMoveTimer >= 1) {
      ToNextCell(); // Переходим к следующей ячейке
    }
    
    _playerChip.SetPosition(Vector2.Lerp(_startPosition, _endPosition, _cellMoveTimer));    // Вычисляем промежуточную позицию фишки между начальной и конечной позицией
    _cellMoveTimer += Time.deltaTime / CellMoveDuration;                                    // Увеличиваем таймер на основе прошедшего времени
  }

  private void ToNextCell()
  {
    _currentCellId++;     // Увеличиваем текущий номер ячейки на 1

    // Если текущий номер ячейки больше или равен общему количеству ячеек - 1
    if (_currentCellId >= _movementCells.Length - 1) {
      EndAnimation();     // Завершаем анимацию
      return;             // Выходим из метода
    }
    
    _startPosition = GameField.GetCellPosition(_movementCells[_currentCellId]);   // Получаем начальную позицию для анимации из класса GameField с помощью текущего номера ячейки
    _endPosition = GameField.GetCellPosition(_movementCells[_currentCellId + 1]); // Получаем конечную позицию для анимации из класса GameField с помощью следующего номера ячейки

    _cellMoveTimer = 0;   // Сбрасываем таймер перемещения на 0
  }

  private void EndAnimation()
  {
    isAnimateNow = false;                               // Задаём флагу isAnimateNow значение false, то есть указываем, что анимация завершилась
    GameStateChanger.ContinueGameAfterChipAnimation();  // Продолжаем игру после анимации фишки с помощью функции ContinueGameAfterChipAnimation() из класса GameStateChanger
  }
}
