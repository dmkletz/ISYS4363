using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace ISYS4363HW1
{
    class HW1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning ...");

            IWebDriver driver = new ChromeDriver("C:\ChromeDriver");
            driver.Url = "https://www.the-numbers.com/market/";

            Console.WriteLine("Title: " + driver.Title);
            Console.WriteLine("URL: " + driver.Url);
            //Console.WriteLine(driver.PageSource);
            string xp = "/ html / body / div / div[5] / div[1] / center[2] / table";
            IWebElement table = driver.FindElement(By.XPath(xp));
            var rows = table.FindElements(By.TagName("tr"));
            string FFrow = "";
            foreach (var row in rows)
            {

                var tds = row.FindElements(By.TagName("td"));
                foreach (var entry in tds)
                {
                    FFrow = FFrow + entry.Text + "  ";
                    //Console.Write(entry.Text + "\t");
                }

                Console.WriteLine(FFrow);
                FFrow = "";
                Console.WriteLine();
            }
            driver.Quit();

            Console.Write("Press any key to end ...");
            Console.ReadLine();
        }
    }
}
