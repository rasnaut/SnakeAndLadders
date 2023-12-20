using UnityEngine;

public class GameCube : MonoBehaviour
{
  public Vector3[] CubeSidesEulers; // Массив с разными углами для каждой стороны кубика

  public int ThrowCube()
  {
    ShowCube();                                                    // Показываем кубик
    int randomCubeValue = Random.Range(0, CubeSidesEulers.Length); // Задаём случайное значение для выбора стороны кубика
    RotateCube(CubeSidesEulers[randomCubeValue]);                  // Поворачиваем кубик и задаём угол для выбранной стороны
    return randomCubeValue + 1;                     // Возвращаем в методе значение броска + 1
  }

  public void ShowCube() {
    SetActiveCube(true); // Делаем кубик видимым
  }

  public void HideCube() {
    SetActiveCube(false);  // Делаем кубик невидимым
  }

  private void SetActiveCube(bool value) {
    gameObject.SetActive(value);  // Включаем или выключаем отображение кубика в зависимости от переданного значения
  }
  
  private void RotateCube(Vector3 cubeEuler) { 
    transform.eulerAngles = cubeEuler;  // Устанавливаем угол поворота кубика
  }
}
