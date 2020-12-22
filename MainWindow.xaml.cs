using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TestProject
{
    public partial class MainWindow : Window
    {
        List<string> operand = new List<string>();
        List<string> operation = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            // Добавляем обработчик для всех кнопок на гриде
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текст кнопки
            var s = new StringBuilder();
            s.Append((string)((Button)e.OriginalSource).Content);
            textBlock.Text += (string)((Button)e.OriginalSource).Content;
            // Если текст - это число
            if (Int32.TryParse(s.ToString(), out int num))
            {
                operand.Add(s.ToString());
            }
            // Если было введено не число
            else
            {
                if (s.ToString() == "=")
                {
                    textBlock.Text += Res();
                    operation.Clear();
                    operand.Clear();
                }
                // Очищаем поле и переменные
                else if (s.ToString() == "CLEAR")
                {
                    operand.Clear();
                    operation.Clear();
                    textBlock.Text = "";
                }
                //получаем операцию
                else
                {
                    operation.Add(s.ToString());
                }
            }
        }
        private string Res()
        {
            for (int j = 0; j < operation.Count;)
            {
                if (operation[j] == "*")
                {
                    operand[j] = (int.Parse(operand[j]) * int.Parse(operand[j + 1])).ToString();
                    operand.RemoveAt(j + 1);
                    operation.RemoveAt(j);
                }
                else if (operation[j] == "/")
                {
                    try
                    {
                        operand[j] = (int.Parse(operand[j]) / int.Parse(operand[j + 1])).ToString();
                        operand.RemoveAt(j + 1);
                        operation.RemoveAt(j);
                    }
                    catch
                    {
                        return "Деление на ноль!";
                    }
                }
                else
                {
                    j++;
                }
            }
            for (int j = 0; j < operation.Count;)
            {
                if (operation[j] == "+")
                {
                    operand[j] = (int.Parse(operand[j]) + int.Parse(operand[j + 1])).ToString();
                    operand.RemoveAt(j + 1);
                    operation.RemoveAt(j);
                }
                else if (operation[j] == "-")
                {
                    operand[j] = (int.Parse(operand[j]) - int.Parse(operand[j + 1])).ToString();
                    operand.RemoveAt(j + 1);
                    operation.RemoveAt(j);
                }
            }
            return operand[0];
        }
    }
}
