using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Создаем объект Random с именем randomizer
        // для генерации случайных чисел.
        Random randomizer = new Random();

        // Эти целочисленные переменные хранят числа
        // для задачи на сложение.
        int addend1;
        int addend2;

        // Эти целочисленные переменные хранят числа
        // для задачи на вычитание.
        int minuend;
        int subtrahend;

        // Эти целочисленные переменные хранят числа
        // для задачи на умножение.
        int multiplicand;
        int multiplier;

        // Эти целочисленные переменные хранят числа
        // для задачи деления.
        int dividend;
        int divisor;

        // This integer variable keeps track of the 
        // remaining time.
        int timeLeft;
        /// <summary>
        /// Начните викторину, заполнив все задачи
        /// и запускаем таймер.
        /// </summary>
        public void StartTheQuiz()
        {
            // Заполнить задачу на сложение.
            // Генерируем два случайных числа для добавления.
            // Сохраняем значения в переменных addend1 и addend2.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Преобразование двух случайно сгенерированных чисел
            // в строки, чтобы их можно было отобразить
            // в элементах управления меткой.
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // 'sum' - это имя элемента управления NumericUpDown.
            // Этот шаг гарантирует, что его значение равно нулю перед
            // добавление к нему любых значений.
            sum.Value = 0;

            // Заполнить задачу на вычитание.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Заполнить задачу на умножение.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Заполнить задачу на деление.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Начать таймер.
            timeLeft = 30;
            timeLabel.Text = "30 секунды";
            timer1.Start();
        }
        /// <summary>
        /// Проверьте ответы, чтобы убедиться, что пользователь все понял правильно.
        /// </summary>
        /// <returns>True, если ответ правильный, иначе false.</returns>
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            // Чтобы игрок не мог нажать кнопку в процессе теста
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // Если CheckTheAnswer() возвращает true, то пользователь
                // получил правильный ответ. Остановить таймер
                // и показать MessageBox.
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // Если CheckTheAnswer() возвращает false, продолжаем считать
                // вниз. Уменьшите оставшееся время на одну секунду и
                // отображаем новое время, оставшееся после обновления
                // Метка Оставшееся время.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // Если у пользователя закончилось время, останавливаем таймер, показываем
                // MessageBox и введите ответы.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }
    }
}
