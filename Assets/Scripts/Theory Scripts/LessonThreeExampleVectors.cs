using UnityEngine;
using System.Collections;

public class LessonThreeExampleVectors : MonoBehaviour
{
    public Vector2 V2D;
    public Vector3 v1, v2;
    float maxDistance = 4;

    // Use this for initialization
    void Start()
    {
        print("Длинна V2D = " + V2D.magnitude); // длина вектора и выведение её на консоль    
        print("Длинна v1 = " + v1.magnitude); // длина вектора и выведение её на консоль 

        float distance = Vector3.Distance(v1, v2); // высчитывание дистанции между двумя векторами (точки)
        print("Дистанция " + distance); // выведение дистанции на консоль

        distance = (v1 - v2).magnitude; // ...
        print(distance);

        distance = Mathf.Sqrt((v1 - v2).sqrMagnitude); // возвращаем квадрат длины вектора и извлекаем из него квадратный корень
        print(distance);

        // Обе операции Vector3.Distance и magnitude дорогостоящие, потому что используют выражения квадратных корней
        // Vector3.Distance(v1, v2); //= Mathf.Sqrt((v1 - v2).sqrMagnitude) 
        // v1.magnitude = Mathf.Sqrt((v1.sqrMagnitude) 

        /* дешёвый аналог, если необходимо только сравнение длинны */
        if (Vector3.Distance(v1, v2) < maxDistance) print("Attack! not optimized");
        if ((v1 - v2).sqrMagnitude < maxDistance * maxDistance) print("Attack! optimized"); //(v1 - v2).sqrMagnitude > maxDistance * maxDistance

        //d_koef = (Ax*Bx)+(Ay*By)+(Az*Bz)
        print("Скалярное произведение " + Vector3.Dot(v1, v2)); // скалярное произведение (независимое от системы координат число)
        
        // вектор, перпендикулярный двум исходным.
        // Компоненты результирующего вектора C определяются следующим образом:
        // Cx = Ay*Bz — Az*By, Cy = Az*Bx — Ax*Bz, Cz = Ax*By — Ay*Bx
        print(Vector3.Cross(v1, v2)); // Векторное произведение    
    }
}

