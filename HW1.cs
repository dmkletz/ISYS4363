using System;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace ISYS4363HW1
{
    class HW1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning ...");

            IWebDriver driver = new ChromeDriver("C:/temp");
            driver.Url = "https://www.the-numbers.com/market/";

            Console.WriteLine("Title: " + driver.Title);
            Console.WriteLine("URL: " + driver.Url);
            //Console.WriteLine(driver.PageSource);
            int[] year = new int[30];
            long[] tickets = new long[30];
            long[] boxoffice = new long[30];
            long[] inflated = new long[30];
            float[] avgprice = new float[30];
            int r = 0;
            int c = 0;
            string xp = "/ html / body / div / div[5] / div[1] / center[2] / table";
            IWebElement table = driver.FindElement(By.XPath(xp));
            var rows = table.FindElements(By.TagName("tr"));
            string FFrow = "";
            foreach (var row in rows)
            {
                r = r + 1;
                var tds = row.FindElements(By.TagName("td"));
                foreach (var entry in tds)
                {
                    c = c + 1;
                    if (c == 1)
                    {
                        year[r] = Int32.Parse(entry.Text);
                    }
                    if (c == 2)
                    {
                        string comma = Regex.Match(entry.Text, @"^[\d,]+").Value.Replace(",", String.Empty);
                        tickets[r] = long.Parse(comma);
                    }
                    if (c == 3)
                    {
                        string dollar = entry.Text.TrimStart(' ', '$');
                        string comma = Regex.Match(dollar, @"^[\d,]+").Value.Replace(",", String.Empty);
                        boxoffice[r] = long.Parse(comma);
                    }
                    if (c == 4)
                    {
                        string dollar = entry.Text.TrimStart(' ', '$');
                        string comma = Regex.Match(dollar, @"^[\d,]+").Value.Replace(",", String.Empty);
                        inflated[r] = long.Parse(comma);
                    }
                    if (c == 5)
                    {
                        string dollar = entry.Text.TrimStart(' ', '$');
                        avgprice[r] = float.Parse(dollar);
                    }
                    FFrow = FFrow + entry.Text + "  ";
                    //int result = Int32.Parse(entry.Text);
                    //Console.Write(entry.Text + "\t");
                }
                c = 0;
                Console.WriteLine(FFrow);
                FFrow = "";
                Console.WriteLine();
            }
            driver.Quit();

            //Tickets calculation
            double average = tickets.Average();
            double sumOfSquaresOfDifferences = tickets.Select(val => (val - average) * (val - average)).Sum();
            double sdtix = Math.Sqrt(sumOfSquaresOfDifferences / tickets.Length);
            Console.WriteLine(sdtix);

            Console.Write("Press any key to end ...");
            Console.ReadLine();
        }
    }
}
