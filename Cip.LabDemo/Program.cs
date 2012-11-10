/////////////////////////////////////////////////////////////////////////////////
// Cip.LabDemo                                                       //
// Copyright (C) Andrew [kyzmitch] Ermoshin.                                   //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;
using System.Threading;

namespace Cip.LabDemo
{
    static class Program
    {
        //object that creates only once
        private static Mutex _syncObject;
        private const string _syncObjectName = "436d8981-fe04-4ad8-b229-f1929b6a2b06";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //spy on run another copy of program.
            #region Another copy of program

            bool createdNew;
            _syncObject = new Mutex(true, _syncObjectName, out createdNew);
            if (!createdNew)
            {
                switch(System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "ru-RU":
                        {
                            MessageBox.Show("Программа уже работает!",
                                            "Обработка Цветных Изображений",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Program already working!",
                                            "Colour Image Processing",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                            break;
                        }
                }
                return;
            }

            #endregion Another copy of program

            //this code get information about culture from config file named "Cip.Config.xml".
            #region Config loading

            string path = Application.StartupPath;
            string culture = "ru-RU";
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(path+"\\Cip.Config.xml");
            }
            catch (Exception expn)
            {
                switch(System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "ru-RU":
                        {
                            MessageBox.Show("Приложение не может ЗАГРУЗИТЬ Cip.Config.xml файл, который должен быть в директории приложения!\n" + path,
                                            "Ошибка",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Application can't LOAD Cip.Config.xml file, that should be in the directory of application!\n" + path,
                                            "IO Error",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                            break;
                        }
                }
                throw new System.IO.IOException("Application can't LOAD cipConfig.xml file, that should be in the directory of application!", expn);
                
            }
            XmlNode xmlCulture = xmlDoc.SelectSingleNode("/Cip.LabDemo/Culture");
            if (xmlCulture != null)
            {
                XmlNodeList list = xmlCulture.ChildNodes;
                culture = list[0].Value;
            }

            #endregion Config loading

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(new System.Globalization.CultureInfo(culture, true)));
            
            //Application.Run(new MainForm());
            //Application.Run(new LanguageSelectionForm());
        }
    }
}