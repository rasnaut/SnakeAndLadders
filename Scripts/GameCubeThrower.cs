using UnityEngine;

public class GameCubeThrower : MonoBehaviour
{
  public GameStateChanger  GameStateChanger;  // Скрипт изменения состояния игры
  public GameCube          GameCubePrefab;    // Префаб для создания кубика
  public Transform         GameCubePoint;     // Точка, где будет появляться кубик
  public GameCubeAnimator  CubeThrowAnimator; // Скрипт анимации кубика

  private int      _cubeValue;    // Значение, которое выпало на кубике
  private GameCube _gameCube;     // Созданный объект кубика
 
  void Start()
  {
    CreateGameCube();   // Создаём новый кубик при запуске игры
  }

  private void CreateGameCube()
  {
    _gameCube = Instantiate(GameCubePrefab, GameCubePoint.position, GameCubePoint.rotation, GameCubePoint);    // Создаём новый кубик в указанной позиции и с указанным углом вращения
    _gameCube.HideCube();                                                                                      // Скрываем кубик, чтобы его не было видно в начале игры
  }

  public void ThrowCube()
  {
    _cubeValue = _gameCube.ThrowCube();    // Получаем случайное значение броска кубика
    CubeThrowAnimator.PlayAnimation();     // Проигрываем анимацию броска
  }

  public void ContinueAfterCubeAnimation() {
    GameStateChanger.DoPlayerTurn(_cubeValue);      // Передаём значение, которое выпало на кубике, в скрипт изменения состояния игры
  }
}
