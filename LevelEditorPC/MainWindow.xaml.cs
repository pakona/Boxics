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
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using System.Xml;
using System.IO;
using System.Threading;
using BoxicsDataTypes;

namespace LevelEditorPC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game1 game;

        public MainWindow()
        {
            InitializeComponent();
        }

        string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
        Thread reloadThread;

        private void ReloadGameData(object text)
        {
            try
            {
                StringBuilder sb = new StringBuilder("<XnaContent><Asset Type=\"BoxicsDataTypes.LevelData\">");
                sb.Append((string)text);
                sb.Append("</Asset></XnaContent>");
                StringReader sr = new StringReader(sb.ToString());
                XmlReader reader = new XmlTextReader(sr);
                LevelData data = IntermediateSerializer.Deserialize<LevelData>(reader, basePath);
                game.Reload(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnLevelDataChanged(object sender, TextChangedEventArgs e)
        {
            if (reloadThread != null && reloadThread.IsAlive)
            {
                reloadThread.Abort();
            }

            reloadThread = new Thread(new ParameterizedThreadStart(ReloadGameData));
            reloadThread.Start(levelDataTextbox.Text);
        }

        private void OnTextBoxLoaded(object sender, RoutedEventArgs e)
        {
            TextReader reader = new StreamReader(basePath + "LevelTemplate.txt");
            levelDataTextbox.Text = reader.ReadToEnd();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            game = new Game1();
            windowsFormsHost.Child = game;
        }
    }
}
