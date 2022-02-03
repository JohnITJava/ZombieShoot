using UnityEngine;


public class MyMath : MonoBehaviour
{
    public float deg = 30.0F;
    private float _speed = 1;

    private void Start()
    {
        // Перевод градусов в радианы
        float rad = deg * Mathf.Deg2Rad;
        //deg = rad * Mathf.Rad2Deg;
        print(deg + " градусов = " + rad + " радиан");

        // Значение числа ПИ
        print("Число ПИ = " + Mathf.PI);

        // Возвращение модуля числа
        print("Модуль числа -10 = " + Mathf.Abs(-10));

        // Возвращение косинуса угла, заданного в радианах
        print("Косинус угла 60 градусов = " + Mathf.Cos(Mathf.Deg2Rad * 60));
       
        //Возведение в степень
        print(Mathf.Pow(5, 2)); 

        //Линейная интерполяция, функция возвращает промежуточное значение между двух величин, на основании значения третьего параметра
        print(Mathf.Lerp(10f, 30f, 0.5f)); // Интерполя́ция - это способ нахождения промежуточного значения
        
        // Возвращение максимального среди заданного набора чисел        
        print(Mathf.Max(1, 4, 6, 2, 0, 9, 5));
        
        // А так же немного случайных чисел

        // Возвращает случайное чисел между двумя заданными (границы включаются)
        print(Random.Range(4.6f, 12.3f)); // float (включительно) min 4.6  max 12.3 (включительно)  

        print(Random.Range(1, 5)); // int (включительно) min 1 max 5 (исключительно)    

        // Возвращает случайное число от 0.0 до 1.0
        print(Random.value);

        // Возвращает случайное число внутри единичной сферы
        print(Random.insideUnitSphere);

        // Возвращает случайное число внутри единичной окружности
        print(Random.insideUnitCircle);
    }

    //Ограничение возможного значения числа
    private void Update()
    {        
        _speed += 1 * Time.deltaTime;
        // Ограничение переменной с помощью min, max
        float fNum = Mathf.Clamp(_speed, 0, 5);

        print(fNum);
        transform.position += new Vector3(fNum, 0, 0) * Time.deltaTime;        
    }
}
