using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStateChanger : MonoBehaviour
{
  public int PlayersCount = 2;                     // Количество игроков
  
  public PlayerChipsCreator PlayersChipsCreator;   // Скрипт создания фишек
  public PlayersTurnChanger PlayersTurnChanger;    // Скрипт смены хода игроков
  public PlayerChipsMover PlayersChipsMover;       // Скрипт перемещения фишек
  public GameField GameField;                      // Скрипт игрового поля
  
  public GameObject      GameScreenGO;             // Экран игры
  public Button          ThrowButton;              // Кнопка бросков кубика
  public GameObject      GameEndScreenGO;          // Экран конца игры
  public TextMeshProUGUI WinText;                  // Надпись о победе

  private void Start() {
    FirstStartGame();   // Делаем первичную настройку игры
  }
  // Метод для первого запуска игры 
  private void FirstStartGame()
  {
    GameField.FillCellsPositions(); // Заполняем позиции клеток на игровом поле
    StartGame();                    // Начинаем новую игру 
  }

  private void StartGame()
  {
    PlayerChip[] playersChips = PlayersChipsCreator.SpawnPlayersChips(PlayersCount);  // Создаём фишки для заданного числа игроков

    PlayersTurnChanger.StartGame(playersChips);     // Готовим игроков к началу игры
    PlayersChipsMover.StartGame(playersChips);      // Задаём начальную позицию фишек игроков
    SetScreens(true);                               // Показываем экран игры
    SetThrowButtonInteractable(true);
  }

  // Метод для завершения игры
  private void EndGame() {
    SetScreens(false);    // Показываем экран конца игры
  }

  // Метод для перезапуска по кнопке
  public void RestartGame()
  {
    PlayersChipsCreator.Clear();      // Удаляем фишки игроков
    StartGame();                      // Начинаем новую игру
  }

  // Метод для установки видимости игровых экранов
  private void SetScreens(bool inGame)
  {
    GameScreenGO.SetActive(inGame);     // Если игра в процессе, показываем экран игры и скрываем экран конца игры
    GameEndScreenGO.SetActive(!inGame); // Иначе скрываем экран игры и показываем экран конца игры 
  }

  // Метод для установки надписи о победе
  private void SetWinText(int playerId)
  {
    WinText.text = $"Player {playerId + 1} WIN!"; // Отображаем текст с номером победившего игрока
  }

  public void DoPlayerTurn(int steps)
  {
    int currentPlayerId = PlayersTurnChanger.GetCurrentPlayerId();      // Получаем номер текущего игрока
    PlayersChipsMover.MoveChip(currentPlayerId, steps);                 // Двигаем фишку текущего игрока на заданное число шагов

    
    SetThrowButtonInteractable(false);                                  // Блокируем возможность бросить кубик

    /*// Проверяем, достиг ли игрок финиша
    bool isPlayerFinished = PlayersChipsMover.CheckPlayerFinished(currentPlayerId);
    if (isPlayerFinished)
    {                 // Если игрок на финише
      SetWinText(currentPlayerId);          // Устанавливаем надпись о победе
      EndGame();                            // Переходим к экрану конца игры
    }
    else
    {
      PlayersTurnChanger.MovePlayerTurn();  // Передаём ход следующему игроку
    }*/
  }

  private void SetThrowButtonInteractable(bool value) {
    ThrowButton.interactable = value;     // Блокируем или активируем кнопку в зависимости от value
  }

  public void ContinueGameAfterChipAnimation()
  {
    int  currentPlayerId  = PlayersTurnChanger.GetCurrentPlayerId();                // Получаем номер текущего игрока
    bool isPlayerFinished = PlayersChipsMover.CheckPlayerFinished(currentPlayerId); // Определяем, достиг ли игрок финиша

    // Если игрок на финише
    if (isPlayerFinished) {
      SetWinText(currentPlayerId);          // Устанавливаем надпись о победе
      EndGame();                            // Переходим к экрану конца игры
    } else {
      PlayersTurnChanger.MovePlayerTurn();  // Передаём ход следующему игроку
      SetThrowButtonInteractable(true);     // Разрешаем ему бросить кубик
    }
  }
}
