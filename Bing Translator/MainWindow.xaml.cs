using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bing_Translator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {




            //Helloooooooooooooo
            InitializeComponent();

            Translator.SetAuthorization("POSTranslator", "WAQCD/pJKigQ7GcS1fdzFIihTDrHIOkAWf+b8+fT0VY=", true);


            Translator.Translate("Hello World", Languages.en, Languages.ar);
            Translator.Translate("Hello World", Languages.en, Languages.ar);
            Translator.Translate("Hello World", Languages.en, Languages.ar);
            Translator.Translate("Hello World", Languages.en, Languages.ar);
            Translator.Translate("Hello World", Languages.en, Languages.ar);
            Translator.Translate("Hello World", Languages.en, Languages.ar);

            string ss = Translator.Translate("Hello", Languages.en, Languages.ar);

            string sss = Translator.Translate("Hello World", Languages.en, Languages.ar);
        }
    }
}
