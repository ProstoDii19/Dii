using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        // firstClicked указывает на первый элемент управления Label
        // что игрок нажимает, но это будет нуль
        // если игрок еще не нажал на метку
        Label firstClicked = null;

        // secondClicked указывает на второй элемент управления Label
        // что игрок нажимает
        Label secondClicked = null;

        // Используйте этот объект Random для выбора случайных значков для квадратов
        Random random = new Random();

        // Каждая из этих букв представляет собой интересную иконку
        // в шрифте Webdings,
        // и каждый значок появляется дважды в этом списке
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        /// <summary>
        /// Назначьте каждый значок из списка значков случайному квадрату
        /// </summary>
        private void AssignIconsToSquares()
        {
            // TableLayoutPanel имеет 16 меток,
            // а в списке значков 16 значков,
            // чтобы иконка вытягивалась случайным образом из списка
            // и добавляем к каждой метке
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        /// <summary>
        /// Событие Click каждой метки обрабатывается этим обработчиком событий.
        /// </summary>
        /// <param name="sender">Ярлык, на который нажали</param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            // Таймер включается только после двух несовпадающих
            // игроку показаны иконки,
            // поэтому игнорируйте любые клики, если таймер запущен
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Если метка, по которой щелкнули, черная, значит, игрок щелкнул
                // уже открытая иконка --
                // игнорировать щелчок
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Если firstClicked равно null, это первая иконка
                // в паре, которую кликнул игрок,
                // поэтому установите firstClicked на метку, которую игрок
                // щелчок, изменение цвета на черный и возврат
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // Если игрок зайдет так далеко, таймер не
                // работает и firstClicked не равно null,
                // так что это должна быть вторая иконка, на которую нажал игрок
                // Устанавливаем его цвет на черный
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Проверяем, выиграл ли игрок
                CheckForWinner();

                // Если игрок нажал два одинаковых значка, оставить их
                // черный и сбросить firstClicked и secondClicked
                // чтобы игрок мог щелкнуть другую иконку
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // Если игрок зайдет так далеко, игрок
                // щелкнули два разных значка, поэтому запустите
                // таймер (который будет ждать три четверти
                // секунду, а затем скрыть иконки)
                timer1.Start();
            }
        }
        /// <summary>
        /// Этот таймер запускается, когда игрок нажимает
        /// две не совпадающие иконки,
        /// так что он считает три четверти секунды
        /// а затем выключается и скрывает оба значка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Остановить таймер
            timer1.Stop();

            // Скрываем оба значка
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Сбросить firstClicked и secondClicked
            // поэтому в следующий раз, когда метка будет
            // щелчок, программа знает, что это первый щелчок
            firstClicked = null;
            secondClicked = null;
        }

        /// <summary>
        /// Проверяем каждую иконку на совпадение с помощью
        /// сравнение цвета переднего плана с цветом фона.
        /// Если все значки совпадают, игрок выигрывает
        /// </summary>
        private void CheckForWinner()
        {
            // Проходим по всем меткам в TableLayoutPanel,
            // проверка каждого, чтобы увидеть, соответствует ли его значок
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // Если цикл не вернулся, он не нашел
            // любые неподходящие иконки
            // Это означает, что пользователь выиграл. Показать сообщение и закрыть форму
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }

        private void FirstClicked(object sender, EventArgs e)
        {

        }
    }
}


