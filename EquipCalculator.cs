using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{
    public partial class EquipCalculator : Window, IComponentConnector
    {
        private enum enuma : byte
        {
            a = 14,
            b = 11,
            c = 10,
            d = 8,
            e = 5,
            f = 5,
            g = 4
        }

        private enum enumb : byte
        {
            a = 14,
            b = 13,
            c = 12,
            d = 10,
            e = 9,
            f = 7,
            g = 7
        }

        private enum enumc : byte
        {
            a = 2,
            b = 0,
            c = 0,
            d = 0,
            e = 0,
            f = 5,
            g = 5
        }

        private enum enumd : byte
        {
            a = 9,
            b = 8,
            c = 7,
            d = 5,
            e = 4,
            f = 1,
            g = 0
        }

        private enum enume : byte
        {
            a = 0,
            b = 0,
            c = 0,
            d = 0,
            e = 0,
            f = 5,
            g = 5
        }

        private enum enumf : byte
        {
            a = 9,
            b = 8,
            c = 7,
            d = 5,
            e = 4,
            f = 1,
            g = 0
        }

        private bool m_a;

        internal RadioButton radioButtonB;

        internal RadioButton radioButtonC/*c*/;

        internal RadioButton radioButtonD/*d*/;

        internal RadioButton radioButtonE/*e*/;

        internal RadioButton radioButtonF/*f*/;

        internal Label label/*g*/;

        internal RadioButton radioButtonH;/*h;*/

        internal RadioButton radioButtonI;/*i;*/

        internal RadioButton radioButtonJ;/*j;*/

        internal CheckBox CheckBoxK;/*k;*/

        internal Label labelL/*l*/;

        internal TextBox textBoxM/*m*/;

        internal TextBox TextBoxN/*n*/;

        internal Label labelO;/*o;*/

        internal Label labelP;/*p;*/

        internal TextBox textBoxQ;/*q;*/

        internal TextBox textBoxR;/*r;*/

        internal Label labelS;/*s;*/

        internal TextBox textBoxT/*t*/;

        internal Label labelU;/*u;*/

        internal TextBox textBoxV/*v*/;

        internal TextBox TextBoxW/*w*/;

        internal TextBox TextBoxX/*x*/;

        internal TextBox TextBoxY/*y*/;

        internal TextBox TextBoxZ/*z*/;

        internal TextBox TextBoxAA/*aa*/;

        internal TextBox TextBoxAB/*ab*/;

        internal TextBox TextBoxAC/*ac*/;

        internal TextBox TextBoxAD/*ad*/;

        private bool ae;

        public EquipCalculator()
        {
            InitializeComponent();
            this.m_a = true;
        }

        private void a(object A_0, MouseButtonEventArgs A_1)
        {
            DragMove();
        }

        private void q(object A_0, RoutedEventArgs A_1)
        {
            if (this.m_a)
            {
                this.CheckBoxK.IsChecked = false;
                this.CheckBoxK.Visibility = ((!this.radioButtonH.IsChecked.Value || (!this.radioButtonC.IsChecked.Value && !this.radioButtonD.IsChecked.Value)) ? Visibility.Collapsed : Visibility.Visible);
                this.label.Visibility = ((!this.radioButtonF.IsChecked.Value) ? Visibility.Collapsed : Visibility.Visible);
                a();
            }
        }

        private void p(object A_0, RoutedEventArgs A_1)
        {
            if (this.m_a)
            {
                if (!this.radioButtonH.IsChecked.Value || (!this.radioButtonC.IsChecked.Value && !this.radioButtonD.IsChecked.Value))
                {
                    this.CheckBoxK.IsChecked = false;
                    this.CheckBoxK.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.CheckBoxK.Visibility = Visibility.Visible;
                }
                a();
            }
        }

        private void o(object A_0, RoutedEventArgs A_1)
        {
            if (!this.m_a)
            {
                return;
            }
            enumu.Content = (this.CheckBoxK.IsChecked.Value ? "15" : "25");
            if (this.CheckBoxK.IsChecked.Value)
            {
                this.radioButtonI.Visibility = Visibility.Collapsed;
                this.radioButtonJ.Visibility = Visibility.Collapsed;
                if (!this.radioButtonH.IsChecked.Value)
                {
                    this.radioButtonH.IsChecked = true;
                    return;
                }
            }
            else
            {
                this.radioButtonI.Visibility = Visibility.Visible;
                this.radioButtonJ.Visibility = Visibility.Visible;
            }
            a();
        }

        private void a(object A_0, TextChangedEventArgs A_1)
        {
            a();
        }

        private void n(object A_0, RoutedEventArgs A_1)
        {
            this.textBoxM.Text = "";
        }

        private void m(object A_0, RoutedEventArgs A_1)
        {
            this.TextBoxN.Text = "";
        }

        private void l(object A_0, RoutedEventArgs A_1)
        {
            this.textBoxQ.Text = "";
        }

        private void k(object A_0, RoutedEventArgs A_1)
        {
            enumr.Text = "";
        }

        private void j(object A_0, RoutedEventArgs A_1)
        {
            enumt.Text = "";
        }

        private void i(object A_0, RoutedEventArgs A_1)
        {
            enumv.Text = "";
        }

        private void h(object A_0, RoutedEventArgs A_1)
        {
            enumw.Text = "";
        }

        private void g(object A_0, RoutedEventArgs A_1)
        {
            enumx.Text = "";
        }

        private void f(object A_0, RoutedEventArgs A_1)
        {
            enumy.Text = "";
        }

        private void e(object A_0, RoutedEventArgs A_1)
        {
            enumz.Text = "";
        }

        private void d(object A_0, RoutedEventArgs A_1)
        {
            enumaa.Text = "";
        }

        private void c(object A_0, RoutedEventArgs A_1)
        {
            enumab.Text = "";
        }

        private void b(object A_0, RoutedEventArgs A_1)
        {
            enumac.Text = "";
        }

        private void a(object A_0, RoutedEventArgs A_1)
        {
            enumad.Text = "";
        }

        private void a()
        {
            if (this.m_a)
            {
                byte b = (byte)((!this.radioButtonB.IsChecked.Value) ? (this.radioButtonC.IsChecked.Value ? 1u : (this.radioButtonD.IsChecked.Value ? 2u : (this.radioButtonE.IsChecked.Value ? 3u : 4u))) : 0u);
                short a_ = (short)(this.radioButtonJ.IsChecked.Value ? 200 : (this.radioButtonI.IsChecked.Value ? 160 : 150));
                bool a_2 = this.CheckBoxK.IsChecked.Value && this.CheckBoxK.Visibility == Visibility.Visible;
                int num;
                try
                {
                    num = int.Parse(this.textBoxM.Text);
                }
                catch
                {
                    num = 0;
                }
                byte b2;
                try
                {
                    b2 = byte.Parse(this.TextBoxN.Text);
                }
                catch
                {
                    b2 = 0;
                }
                int num2;
                try
                {
                    num2 = int.Parse(this.textBoxQ.Text);
                }
                catch
                {
                    num2 = 0;
                }
                byte b3;
                try
                {
                    b3 = byte.Parse(enumr.Text);
                }
                catch
                {
                    b3 = 0;
                }
                byte b4;
                try
                {
                    b4 = byte.Parse(enumt.Text);
                }
                catch
                {
                    b4 = 0;
                }
                byte b5;
                try
                {
                    b5 = byte.Parse(enumv.Text);
                }
                catch
                {
                    b5 = 0;
                }
                byte b6;
                try
                {
                    b6 = byte.Parse(enumw.Text);
                }
                catch
                {
                    b6 = 0;
                }
                byte b7;
                try
                {
                    b7 = byte.Parse(enumx.Text);
                }
                catch
                {
                    b7 = 0;
                }
                byte b8;
                try
                {
                    b8 = byte.Parse(enumy.Text);
                }
                catch
                {
                    b8 = 0;
                }
                byte b9;
                try
                {
                    b9 = byte.Parse(enumz.Text);
                }
                catch
                {
                    b9 = 0;
                }
                byte b10;
                try
                {
                    b10 = byte.Parse(enumaa.Text);
                }
                catch
                {
                    b10 = 0;
                }
                byte b11;
                try
                {
                    b11 = byte.Parse(enumab.Text);
                }
                catch
                {
                    b11 = 0;
                }
                int num3;
                try
                {
                    num3 = int.Parse(enumac.Text);
                }
                catch
                {
                    num3 = 0;
                }
                int num4;
                try
                {
                    num4 = int.Parse(enumad.Text);
                }
                catch
                {
                    num4 = 0;
                }
                int num5 = num2;
                byte num6 = b5;
                int num7;
                switch (b)
                {
                    default:
                        num7 = 9;
                        break;
                    case 3:
                        num7 = 9;
                        break;
                    case 0:
                    case 4:
                        num7 = 14;
                        break;
                }
                int num8 = num5 + num6 * num7;
                byte num9 = b6;
                int num10;
                switch (b)
                {
                    default:
                        num10 = 8;
                        break;
                    case 3:
                        num10 = 8;
                        break;
                    case 0:
                    case 4:
                        num10 = 13;
                        break;
                }
                int num11 = num8 + num9 * num10;
                byte num12 = b7;
                int num13;
                switch (b)
                {
                    default:
                        num13 = 7;
                        break;
                    case 3:
                        num13 = 7;
                        break;
                    case 0:
                    case 4:
                        num13 = 12;
                        break;
                }
                int num14 = num11 + num12 * num13;
                byte num15 = b8;
                int num16;
                switch (b)
                {
                    default:
                        num16 = 5;
                        break;
                    case 3:
                        num16 = 5;
                        break;
                    case 0:
                    case 4:
                        num16 = 10;
                        break;
                }
                int num17 = num14 + num15 * num16;
                byte num18 = b9;
                int num19;
                switch (b)
                {
                    default:
                        num19 = 4;
                        break;
                    case 3:
                        num19 = 4;
                        break;
                    case 0:
                    case 4:
                        num19 = 9;
                        break;
                }
                int num20 = num17 + num18 * num19;
                byte num21 = b10;
                int num22;
                switch (b)
                {
                    default:
                        num22 = 1;
                        break;
                    case 3:
                        num22 = 1;
                        break;
                    case 0:
                    case 4:
                        num22 = 7;
                        break;
                }
                int num23 = num20 + num21 * num22;
                byte num24 = b11;
                int num25;
                switch (b)
                {
                    default:
                        num25 = 0;
                        break;
                    case 3:
                        num25 = 0;
                        break;
                    case 0:
                    case 4:
                        num25 = 7;
                        break;
                }
                int num26 = num23 + num24 * num25 + num4;
                int num27 = num;
                byte num28 = b5;
                int num29;
                switch (b)
                {
                    default:
                        num29 = 2;
                        break;
                    case 3:
                        num29 = 0;
                        break;
                    case 0:
                    case 4:
                        num29 = 14;
                        break;
                }
                int num30 = num27 + num28 * num29;
                byte num31 = b6;
                int num32;
                switch (b)
                {
                    default:
                        num32 = 0;
                        break;
                    case 3:
                        num32 = 0;
                        break;
                    case 0:
                    case 4:
                        num32 = 11;
                        break;
                }
                int num33 = num30 + num31 * num32;
                byte num34 = b7;
                int num35;
                switch (b)
                {
                    default:
                        num35 = 0;
                        break;
                    case 3:
                        num35 = 0;
                        break;
                    case 0:
                    case 4:
                        num35 = 10;
                        break;
                }
                int num36 = num33 + num34 * num35;
                byte num37 = b8;
                int num38;
                switch (b)
                {
                    default:
                        num38 = 0;
                        break;
                    case 3:
                        num38 = 0;
                        break;
                    case 0:
                    case 4:
                        num38 = 8;
                        break;
                }
                int num39 = num36 + num37 * num38;
                byte num40 = b9;
                int num41;
                switch (b)
                {
                    default:
                        num41 = 0;
                        break;
                    case 3:
                        num41 = 0;
                        break;
                    case 0:
                    case 4:
                        num41 = 5;
                        break;
                }
                int num42 = num39 + num40 * num41;
                byte num43 = b10;
                int num44;
                switch (b)
                {
                    default:
                        num44 = 5;
                        break;
                    case 3:
                        num44 = 5;
                        break;
                    case 0:
                    case 4:
                        num44 = 5;
                        break;
                }
                int num45 = num42 + num43 * num44;
                byte num46 = b11;
                int num47;
                switch (b)
                {
                    default:
                        num47 = 5;
                        break;
                    case 3:
                        num47 = 5;
                        break;
                    case 0:
                    case 4:
                        num47 = 4;
                        break;
                }
                int num48 = num45 + num46 * num47 + num3;
                for (byte b12 = 0; b12 < b4; b12 = (byte)(b12 + 1))
                {
                    Dictionary<int, int> dictionary = a(a_2, b, b12, num26, a_);
                    num48 += dictionary[1];
                    num26 += dictionary[2];
                }
                this.labelO.Content = num48 - num;
                this.labelL.Content = num48 + b2;
                enums.Content = num26 - num2;
                this.labelP.Content = num26 + b3;
            }
        }

        private Dictionary<int, int> a(bool A_0, byte A_1, byte A_2, int A_3, short A_4)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            dictionary.Add(1, 0);
            dictionary.Add(2, 0);
            if (A_0)
            {
                switch (A_2)
                {
                    case 0:
                        dictionary.Remove(1);
                        dictionary.Add(1, 19);
                        break;
                    case 1:
                        dictionary.Remove(1);
                        dictionary.Add(1, 20);
                        break;
                    case 2:
                        dictionary.Remove(1);
                        dictionary.Add(1, 22);
                        break;
                    case 3:
                        dictionary.Remove(1);
                        dictionary.Add(1, 25);
                        break;
                    case 4:
                        dictionary.Remove(1);
                        dictionary.Add(1, 29);
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        dictionary.Remove(2);
                        dictionary.Add(2, A_2 + 4);
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                        dictionary.Remove(2);
                        dictionary.Add(2, 15 + 2 * (A_2 - 10));
                        break;
                }
            }
            else if (A_1 == 0)
            {
                int value = ((A_2 >= 0 && A_2 < 5) ? 2 : ((A_2 >= 5 && A_2 < 15) ? 3 : ((A_4 >= 200) ? 15 : ((A_4 < 160) ? 11 : 13))));
                dictionary.Remove(1);
                dictionary.Add(1, value);
                if (A_2 < 15)
                {
                    dictionary.Remove(2);
                    dictionary.Add(2, (int)Math.Floor((double)A_3 / 50.0) + 1);
                }
                else
                {
                    int value2 = 0;
                    switch (A_2)
                    {
                        case 15:
                            value2 = ((A_4 < 200) ? ((A_4 < 160) ? 8 : 9) : 13);
                            break;
                        case 16:
                            value2 = ((A_4 < 200) ? ((A_4 < 160) ? 9 : 9) : 13);
                            break;
                        case 17:
                            value2 = ((A_4 < 200) ? ((A_4 < 160) ? 9 : 10) : 14);
                            break;
                        case 18:
                            value2 = ((A_4 < 200) ? ((A_4 < 160) ? 10 : 11) : 14);
                            break;
                        case 19:
                            value2 = ((A_4 < 200) ? ((A_4 < 160) ? 11 : 12) : 15);
                            break;
                        case 20:
                            value2 = ((A_4 < 200) ? ((A_4 < 160) ? 12 : 13) : 16);
                            break;
                        case 21:
                            value2 = ((A_4 < 200) ? ((A_4 < 160) ? 13 : 14) : 17);
                            break;
                        case 22:
                            value2 = ((A_4 < 200) ? ((A_4 < 160) ? 31 : 32) : 34);
                            break;
                    }
                    dictionary.Remove(2);
                    dictionary.Add(2, value2);
                }
            }
            else
            {
                int value3 = ((A_2 >= 0 && A_2 < 5) ? 2 : ((A_2 >= 5 && A_2 < 15) ? 3 : ((A_4 >= 200) ? 15 : ((A_4 < 160) ? 11 : 13))));
                dictionary.Remove(1);
                dictionary.Add(1, value3);
                if (A_2 >= 15)
                {
                    int value4 = 0;
                    switch (A_2)
                    {
                        case 15:
                            value4 = ((A_4 < 200) ? ((A_4 < 160) ? 9 : 10) : 12);
                            break;
                        case 16:
                            value4 = ((A_4 < 200) ? ((A_4 < 160) ? 10 : 11) : 13);
                            break;
                        case 17:
                            value4 = ((A_4 < 200) ? ((A_4 < 160) ? 11 : 12) : 14);
                            break;
                        case 18:
                            value4 = ((A_4 < 200) ? ((A_4 < 160) ? 12 : 13) : 15);
                            break;
                        case 19:
                            value4 = ((A_4 < 200) ? ((A_4 < 160) ? 13 : 14) : 16);
                            break;
                        case 20:
                            value4 = ((A_4 < 200) ? ((A_4 < 160) ? 14 : 15) : 17);
                            break;
                        case 21:
                            value4 = ((A_4 < 200) ? ((A_4 < 160) ? 16 : 17) : 19);
                            break;
                        case 22:
                            value4 = ((A_4 < 200) ? ((A_4 < 160) ? 18 : 19) : 21);
                            break;
                        case 23:
                            value4 = ((A_4 < 200) ? ((A_4 < 160) ? 20 : 21) : 23);
                            break;
                        case 24:
                            value4 = ((A_4 < 200) ? ((A_4 < 160) ? 22 : 23) : 25);
                            break;
                    }
                    dictionary.Remove(2);
                    dictionary.Add(2, value4);
                }
                else if (A_1 == 1)
                {
                    int value5 = 0;
                    switch (A_2)
                    {
                        case 4:
                        case 6:
                        case 8:
                        case 10:
                        case 12:
                            value5 = 1;
                            break;
                        case 13:
                            if (A_4 >= 200)
                            {
                                value5 = 1;
                            }
                            break;
                        case 14:
                            value5 = ((A_4 >= 200) ? 1 : 2);
                            break;
                    }
                    dictionary.Remove(2);
                    dictionary.Add(2, value5);
                }
            }
            return dictionary;
        }

       
    }
}
