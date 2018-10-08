# spsu-mm-machine-learning
<h3> Задание по курсу "Введение в машинное обучение" </h3> </br>
<b>Датасет:</b> Facebook Comment Volume Dataset.</br>
<b>Цель:</b>
Предсказать, сколько комментариев наберёт пост. Задача предполагает реализацию градиентного спуска и подсчёт метрик оценки качества модели. Можно использовать линейную алгебру, всякую другую математику готовую в либах. </br>
<b>Этапы решения:</b></br>
— нормировка значений фичей;</br>
— кросс-валидация по пяти фолдам и обучение линейной регрессии;</br>
— подсчёт R^2 (коэффициента детерминации) и RMSE.</br>
</br>
<b>Результаты можно оформить в виде следующей таблицы.</b> </br>
T1,..T5 — фолды, E — среднее, STD — дисперсия, R^2/RMSE-test — значение соответствующей метрики на тестовой выборке, -train — на обучающей выборке, f0,..fn — значимость признаков (они же переменные, они же фичи).
<table class="tg">
  <tr>
    <th class="tg-031e"><br></th>
    <th class="tg-031e">T1</th>
    <th class="tg-031e">T2</th>
    <th class="tg-031e">...</th>
    <th class="tg-031e">T5</th>
    <th class="tg-031e">E</th>
    <th class="tg-031e">STD</th>
  </tr>
  <tr>
    <td class="tg-031e">R^2_train</td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
  </tr>
  <tr>
    <td class="tg-031e">R^2_test</td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
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
   <tr>
    <td class="tg-031e">feature1</td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
   </tr>
   <tr>
    <td class="tg-031e">...</td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
   </tr>
   <tr>
    <td class="tg-031e">featureM</td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
    <td class="tg-031e"></td>
   </tr>
</table>



