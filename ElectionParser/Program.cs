using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ElectionParser
{
    class Program
    {
        private static readonly Uri uri = new Uri(@"https://www.elections.am/votersreg/");
        private static readonly By nameFieldXpath = By.XPath("//*[@id='ctl00_centerHolder_tbFName']");  //   /html/body/div[1]/div/div/div[2]/article/form/div[3]/div/fieldset/div[1]/input
        private static readonly By surnameFieldXpath = By.XPath("//*[@id='ctl00_centerHolder_tbLName']"); //   /html/body/div[1]/div/div/div[2]/article/form/div[3]/div/fieldset/div[2]/input
        private static readonly By searchButtonXpath = By.XPath("//*[@id='ctl00_centerHolder_search']");

        private static readonly By personRow = By.XPath("//*[@id='aspnetForm']/table/tbody/tr");
        private static readonly By personColumn = By.XPath("//*[@id='aspnetForm']/table/tbody/tr[2]/td");
        private static readonly By nextPageButton = By.XPath("//*[@title=' Հաջորդ էջ ']");


        private static readonly string[] alfabetName = { "ա", "բ", "գ", "դ", "ե", "զ", "է", "ը", "թ", "ժ", "ի", "լ", "խ", "ծ", "կ", "հ", "ձ", "ղ", "ճ", "մ", "յ", "ն", "շ", "ո", "չ", "պ", "ջ", "ռ", "ս", "վ", "տ", "ր", "ց", "ու", "փ", "ք", "եւ", "օ", "ֆ" };
        private static readonly string[] alfabetSurname = { "ա", "բ", "գ", "դ", "ե", "զ", "է", "ը", "թ", "ժ", "ի", "լ", "խ", "ծ", "կ", "հ", "ձ", "ղ", "ճ", "մ", "յ", "ն", "շ", "ո", "չ", "պ", "ջ", "ռ", "ս", "վ", "տ", "ր", "ց", "ու", "փ", "ք", "եւ", "օ", "ֆ" };
        private static readonly IWebDriver chromedriver;
        private static readonly Log log;

        static Program()
        {
            log = new Log("Election.txt");
            //Proxy proxy = new Proxy();
            //proxy.Kind = ProxyKind.Manual;
            //proxy.IsAutoDetect = false;
            //proxy.HttpProxy =
            //proxy.SslProxy = "79.170.202.194:37557";

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("ignore-certificate-errors");
            options.AddArguments("disable-infobars");
            options.AddArguments("--incognito");
            //options.Proxy = proxy;

            chromedriver = new ChromeDriver(options);
            chromedriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 10);
            chromedriver.Navigate().GoToUrl(uri);

        }

        static void Main()
        {
            foreach (var name in alfabetName)
            {
                foreach (var surname in alfabetSurname)
                {
                    SearchPerson(name, surname);
                    log.WriteLineWithDateTime($"{name} {surname}");
                }
            }
        }

        private static void SearchPerson(string name, string surname)
        {
            //Անունի դաշտը
            chromedriver.FindElement(nameFieldXpath).Clear();
            chromedriver.FindElement(nameFieldXpath).SendKeys(name);
            //Ազգանունի դաշտը
            chromedriver.FindElement(surnameFieldXpath).Clear();
            chromedriver.FindElement(surnameFieldXpath).SendKeys(surname);
            //ՓՆՏՐԵԼ բատնը
            chromedriver.FindElement(searchButtonXpath).Click(2, 2000);

            TableParsAndMoveNext();
        }

        private static void TableParsAndMoveNext()
        {
            var rows = chromedriver.FindElements(personRow);
            if (rows.Count < 1) return;

            TablePars(rows);

            var nextPB = chromedriver.TryFindElement(nextPageButton);
            if (nextPB != null)
            {
                nextPB.Click(2, 1000);
                TableParsAndMoveNext();
            }
            else return;
        }



        private static void TablePars(ReadOnlyCollection<IWebElement> rows)
        {
            for (int i = 1; i < rows.Count; i++)
            {
                var personInfo = rows[i].FindElements(personColumn);

                string person = string.Empty;
                foreach (var item in personInfo)
                {
                    person += $"{item.Text}|";
                }
                log.WriteLine(person);
            }
        }
    }
}
