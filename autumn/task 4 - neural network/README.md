# spsu-mm-machine-learning
<h3> Задание по курсу "Введение в машинное обучение" </h3> </br>
<b>Датасет:</b> MNIST Dataset.</br>
<b>Задание:</b>

1) Написать свой маленький, но собственный фреймворк для обучения нейросетей, должны быть реализованы следующие слои:
Полносвязный(Dense, FullyConnected, Linear — вариации названия этого слоя), Dropout, несколько видов активаций — Relu, LeakyRelu, SoftPlus(Апроксимация Relu логарифмой log(1+e^x)), логистическая сигмоида, softmax и logsoftmax (в комбинации с кроссэнтропией сильно улучшит сходимость). А также как минимум 2 функции потерь — mse и crossentropy(для многоклассовой классификации). 

2) Применить ваш фреймворк для задачи классификации циферок, датасет MNIST(http://yann.lecun.com/exdb/mnist/), построить график изменения loss (а лучше еще дополнительно и accuracy) в зависимости от эпохи. (вытяните каждую картинку в вектор, а каждую меточку <i><b>y</b></i> сделайте one hot вектором).

<b>Notes:</b> Не забудьте поиграться с архитектурами (однослойная, двухслойная, трехслойная, добавляет ли что-то дропаут!). Eсли пишете на питоне, зафиксировать эти эксперименты в виде jupyter notebook, а если на других языках, то придумать другую форму отчета, например, каждому эксперименту свой модуль, а результаты с пояснениями положить в текстовый файл. 
