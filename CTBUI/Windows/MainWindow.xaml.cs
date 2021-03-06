﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CTBUI.JsonClasses;
using Newtonsoft.Json;

namespace CTBUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<BotListItem> m_ListOfBots = new ObservableCollection<BotListItem>();

        private string m_pathToFiles;

        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        /// <summary>
        /// Set the itemsource to our observableCollection so it updates if there are changes to the configs
        /// Create the Folderstructure and add the bots to the list
        /// </summary>
        private void Initialize()
        {
            BotList.ItemsSource = m_ListOfBots;

            CreateFolderStructure();

            PopulateBotList();
        }

        /// <summary>
        /// Create the folderstructure for the project
        /// </summary>
        private void CreateFolderStructure()
        {
            if (!Directory.Exists("Files"))
            {
                Directory.CreateDirectory("Files");
            }

            string path = GetParentPathAtPosition(Environment.CurrentDirectory, 3);

            path += @"\CTB\bin\";

            string releasePath = path + "Release/Files";
            string debugPath = path + "Debug/Files";

            if (Directory.Exists(releasePath) && Directory.GetFiles(releasePath).Length > 0)
            {
                m_pathToFiles = releasePath;
                CreateFolderStructureAndCopyFiles();
            }
            else if (Directory.Exists(debugPath) && Directory.GetFiles(debugPath).Length > 0)
            {
                m_pathToFiles = debugPath;
                CreateFolderStructureAndCopyFiles();
            }
            else
            {
                if (!Directory.Exists("Files/Authfiles"))
                {
                    Directory.CreateDirectory("Files/Authfiles");
                }
                if (!Directory.Exists("Files/2FAFiles"))
                {
                    Directory.CreateDirectory("Files/2FAFiles");
                }
                if (!Directory.Exists("Files/Configs"))
                {
                    Directory.CreateDirectory("Files/Configs");
                }
            }
        }

        /// <summary>
        /// Clear the list so we do not have multiple same entries
        /// Add every config to the list
        /// </summary>
        private void PopulateBotList()
        {
            m_ListOfBots.Clear();

            foreach (string file in Directory.GetFiles("Files/Configs"))
            {
                if (file.Contains(".json"))
                {
                    m_ListOfBots.Add(new BotListItem { m_Name = file.Split('\\').Last().Split('.')[0], m_Selected = false, m_Status = "offline" });
                }
            }
        }

        /// <summary>
        /// TODO rework and just split the path and get the item at the position
        /// </summary>
        /// <param name="_path"></param>
        /// <param name="_index"></param>
        /// <returns></returns>
        private static string GetParentPathAtPosition(string _path, int _index)
        {
            string path = _path;

            for (int i = 0; i < _index; i++)
            {
                path = Directory.GetParent(path).ToString();
            }

            return path;
        }

        /// <summary>
        /// Create the topfolderstructure and copy all files from these into our folderstructure
        /// 
        /// TODO build it recursively?
        /// </summary>
        private void CreateFolderStructureAndCopyFiles()
        {
            string[] directories = Directory.GetDirectories(m_pathToFiles);

            foreach (string directory in directories)
            {
                string directoryLastString = directory.Split('\\').Last();

                DirectoryInfo directoryInfo = null;

                if (!Directory.Exists($"Files/{directoryLastString}"))
                {
                    directoryInfo = Directory.CreateDirectory($"Files/{directoryLastString}");
                }

                if (directoryInfo != null)
                {
                    foreach (string file in Directory.GetFiles(directory))
                    {
                        File.Copy(file, directoryInfo.FullName + "/" + System.IO.Path.GetFileName(file));
                    }
                }
            }
        }

        /// <summary>
        /// open the window for a new config
        /// if the window gets closed update the list again
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void AddClick(object _sender, RoutedEventArgs _e)
        {
            NewConfig config = new NewConfig();
            config.Show();

            config.Closed += (_object, _args) => { PopulateBotList(); };
        }

        /// <summary>
        /// go trough every selected bot and delete the files
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void RemoveClick(object _sender, RoutedEventArgs _e)
        {
            foreach (object bot in BotList.Items)
            {
                if (((BotListItem)bot).m_Selected)
                {
                    File.Delete($"Files/Configs/{((BotListItem)bot).m_Name}.json");
                }
            }

            PopulateBotList();
        }

        /// <summary>
        /// TODO maybe make Start async
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="_e"></param>
        private void StartClick(object _sender, RoutedEventArgs _e)
        {
            foreach (object botName in BotList.Items)
            {
                Task.Run(() =>
                {
                    BotListItem newBot = (BotListItem)botName;

                    if (newBot.m_Selected)
                    {
                        Console.WriteLine(newBot.m_Name);

                        BotInfo botInfo = JsonConvert.DeserializeObject<BotInfo>(File.ReadAllText(m_pathToFiles + "/Configs/" + newBot.m_Name + ".json"));

                        Bot bot = new Bot(botInfo);

                        bot.Start();
                    }
                });
            }
        }

        private void StopClick(object _sender, RoutedEventArgs _e)
        {
            string test = Console.ReadLine();
        }
    }
}
