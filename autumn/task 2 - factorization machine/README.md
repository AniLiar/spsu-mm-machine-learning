# spsu-mm-machine-learning
<h3> Задание по курсу "Введение в машинное обучение" </h3> </br>
<b>Датасет:</b> Netflix Prize data.</br>
<b>Цель:</b>
Есть матрица рейтингов User-Item, по кросс-валидации бьем её на фолды, затем пытаемся предсказать скрытые рейтинги. Качество проверяем по RMSE, только для тех точек в которых прогноз есть. 

Используем факторизационную машину 2-го порядка с квадратичной функцией потерь (аналогично линейной регрессии). </br>

<b>Результаты можно оформить в виде следующей таблицы.</b> </br>
T1,..Tn — фолды, E — среднее, STD — дисперсия, RMSE-test — значение соответствующей метрики на тестовой выборке, -train — на обучающей выборке.
<table class="tg">
  <tr>
    <th class="tg-031e"><br></th>
    <th class="tg-031e">T1</th>
    <th class="tg-031e">T2</th>
    <th class="tg-031e">...</th>
    <th class="tg-031e">Tn</th>
    <th class="tg-031e">E</th>
    <th class="tg-031e">STD</th>
  </tr>
  <tr>
    <td class="tg-031e">RMSE_train</td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
  </tr>
  <tr>
    <td class="tg-031e">RMSE_test</td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
  </tr>
</table>



